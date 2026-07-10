// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace KappaDuck.Quack.SourceGenerator.Events;

[Generator]
internal class EventGenerator : IIncrementalGenerator
{
    private const string AttributeMetadataName = "KappaDuck.Quack.Events.QuackEventAttribute";
    private const string EventInterfaceMetadataName = "KappaDuck.Quack.Events.IEvent";

    private static readonly DiagnosticDescriptor _missingInterface = new("QUACK001", "Event type must implement IEvent", "'{0}' is decorated with [QuackEvent] but does not implement IEvent", "Events", DiagnosticSeverity.Error, true);
    private static readonly DiagnosticDescriptor _invalidConstructor = new("QUACK002", "Invalid event constructor", "'{0}' must declare zero constructors (marker event), or exactly one constructor with a single native payload parameter", "Events", DiagnosticSeverity.Error, true);
    private static readonly DiagnosticDescriptor _missingNativeField = new("QUACK003", "Payload event must specify a native field", "'{0}' has a payload constructor, so [QuackEvent] must set NativeField to the SDL_Event field to read", "Events", DiagnosticSeverity.Error, true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EventInfo> events = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                AttributeMetadataName,
                predicate: static (node, _) => node is StructDeclarationSyntax,
                transform: static (ctx, _) => Extract(ctx))
            .Where(static c => c is not null)
            .Select(static (c, _) => c!)
            .WithTrackingName("events");

        IncrementalValueProvider<EquatableArray<EventInfo>> collected = events.Collect()
            .Select(static (array, _) => new EquatableArray<EventInfo>(array))
            .WithTrackingName("combined");

        context.RegisterSourceOutput(collected, static (spc, source) => Execute(source, spc));
    }

    private static EventInfo? Extract(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol @event)
            return null;

        if (context.Attributes is not [AttributeData attribute, ..])
            return null;

        string? nativeEventType = GetNativeEventType(attribute);

        if (nativeEventType is null)
            return null;

        IMethodSymbol[] ctors = [.. @event.InstanceConstructors.Where(static c => !c.IsImplicitlyDeclared)];

        return new EventInfo(@event.Name, nativeEventType)
        {
            NativeField = attribute.NamedArguments.FirstOrDefault(static kv => kv.Key == "NativeField").Value.Value as string,
            Location = LocationInfo.CreateFrom(@event),
            ImplementsInterface = @event.AllInterfaces.Any(static i => i.ToDisplayString() == EventInterfaceMetadataName),
            Valid = ctors is { Length: 0 } or ([{ Parameters.Length: 1 }]),
            HasConstructor = ctors is [{ Parameters.Length: 1 }]
        };
    }

    private static string? GetNativeEventType(AttributeData attribute)
    {
        if (attribute.ApplicationSyntaxReference?.GetSyntax() is AttributeSyntax { ArgumentList.Arguments: [{ Expression: MemberAccessExpressionSyntax memberAccess }, ..] })
            return memberAccess.Name.Identifier.ValueText;

        if (attribute.ConstructorArguments is not [{ Type: INamedTypeSymbol type, Value: { } value }, ..])
            return null;

        foreach (ISymbol member in type.GetMembers())
        {
            if (member is IFieldSymbol { HasConstantValue: true, ConstantValue: { } memberValue } field && EnumValuesEqual(memberValue, value))
                return field.Name;
        }

        return null;
    }

    private static bool EnumValuesEqual(object left, object right)
    {
        try
        {
            return Convert.ToInt64(left) == Convert.ToInt64(right);
        }
        catch (OverflowException)
        {
            return left.Equals(right);
        }
    }

    private static void Execute(EquatableArray<EventInfo> events, SourceProductionContext context)
    {
        if (events.Count == 0)
            return;

        List<Event> resolved = [];

        foreach (EventInfo ev in events)
        {
            if (!ev.ImplementsInterface)
            {
                context.ReportDiagnostic(Diagnostic.Create(_missingInterface, ev.Location?.ToLocation(), ev.Name));
                continue;
            }

            if (!ev.Valid)
            {
                context.ReportDiagnostic(Diagnostic.Create(_invalidConstructor, ev.Location?.ToLocation(), ev.Name));
                continue;
            }

            if (ev.HasConstructor && ev.NativeField is null)
            {
                context.ReportDiagnostic(Diagnostic.Create(_missingNativeField, ev.Location?.ToLocation(), ev.Name));
                continue;
            }

            resolved.Add(new Event(ev.Name, ev.NativeEventType, ev.NativeField));
        }

        if (resolved.Count == 0)
            return;

        resolved.Sort(static (a, b) =>
        {
            int byType = string.CompareOrdinal(a.NativeEventType, b.NativeEventType);
            return byType != 0 ? byType : string.CompareOrdinal(a.Name, b.NativeEventType);
        });

        context.AddSource("Event.g.cs", BuildEvent([.. resolved]));
        context.AddSource("EventMapper.g.cs", BuildEventMapper([.. resolved]));
    }

    private static SourceText BuildEvent(Event[] events)
    {
        StringBuilder sb = new();

        sb.AppendLine("// <auto-generated/>")
          .AppendLine("#nullable enable")
          .AppendLine()
          .AppendLine("using KappaDuck.Quack.Interop.SDL.Primitives.Events;")
          .AppendLine()
          .AppendLine("namespace KappaDuck.Quack.Events;")
          .AppendLine()
          .AppendLine("public readonly partial struct Event")
          .AppendLine("{");

        foreach (string name in events.Select(e => e.Name))
            sb.AppendLine($"    private readonly {name} {FieldName(name)};");

        sb.AppendLine()
          .AppendLine("    private readonly SDL_EventType _type;")
          .AppendLine();

        foreach (Event e in events)
        {
            sb.AppendLine("    /// <summary>")
              .AppendLine($"    /// Initializes a new instance holding a <see cref=\"{e.Name}\"/>.")
              .AppendLine("    /// </summary>")
              .AppendLine("    /// <param name=\"e\">The event value.</param>")
              .AppendLine($"    public Event({e.Name} e)")
              .AppendLine("    {")
              .AppendLine($"        {FieldName(e.Name)} = e;")
              .AppendLine($"        _type = SDL_EventType.{e.NativeEventType};")
              .AppendLine("    }")
              .AppendLine();
        }

        sb.AppendLine("    /// <summary>")
          .AppendLine("    /// Gets a value indicating whether this event holds a value or not.")
          .AppendLine("    /// </summary>")
          .AppendLine("    public bool HasValue => _type == EventMapper.None;")
          .AppendLine()
          .AppendLine("    /// <summary>")
          .AppendLine("    /// Gets the underlying value by boxing, or <see langword=\"null\"/> if this event holds none.")
          .AppendLine("    /// </summary>")
          .AppendLine("    public object? Value => _type switch")
          .AppendLine("    {");

        foreach (Event e in events)
            sb.AppendLine($"        SDL_EventType.{e.NativeEventType} => {FieldName(e.Name)},");

        sb.AppendLine("        _ => null")
          .AppendLine("    };")
          .AppendLine();

        foreach (Event e in events)
        {
            sb.AppendLine("    /// <summary>")
              .AppendLine($"    /// Attempts to retrieve this event as a <see cref=\"{e.Name}\"/>.")
              .AppendLine("    /// </summary>")
              .AppendLine("    /// <param name=\"e\">The event value when this method returns <see langword=\"true\"/>.</param>")
              .AppendLine($"    /// <returns><see langword=\"true\"/> if this event holds a <see cref=\"{e.Name}\"/>; otherwise <see langword=\"false\"/>.</returns>")
              .AppendLine($"    public bool TryGetValue(out {e.Name} e)")
              .AppendLine("    {")
              .AppendLine($"        if (_type != SDL_EventType.{e.NativeEventType})")
              .AppendLine("        {")
              .AppendLine("            e = default;")
              .AppendLine("            return false;")
              .AppendLine("        }")
              .AppendLine()
              .AppendLine($"        e = {FieldName(e.Name)};")
              .AppendLine("        return true;")
              .AppendLine("    }")
              .AppendLine();
        }

        sb.AppendLine("}");

        return SourceText.From(sb.ToString(), Encoding.UTF8);
    }

    private static SourceText BuildEventMapper(Event[] events)
    {
        StringBuilder sb = new();

        sb.AppendLine("// <auto-generated/>")
          .AppendLine("#nullable enable")
          .AppendLine()
          .AppendLine("using System;")
          .AppendLine("using System.Collections.Frozen;")
          .AppendLine("using System.Collections.Generic;")
          .AppendLine("using KappaDuck.Quack.Interop.SDL.Primitives.Events;")
          .AppendLine()
          .AppendLine("namespace KappaDuck.Quack.Events;")
          .AppendLine()
          .AppendLine("internal static partial class EventMapper")
          .AppendLine("{")
          .AppendLine("    private static readonly FrozenDictionary<Type, SDL_EventType> _map = new Dictionary<Type, SDL_EventType>")
          .AppendLine("    {");

        foreach (Event e in events)
            sb.AppendLine($"        [typeof({e.Name})] = SDL_EventType.{e.NativeEventType},");

        sb.AppendLine("    }.ToFrozenDictionary();")
          .AppendLine()
          .AppendLine("    internal static SDL_EventType Of<TEvent>() where TEvent : IEvent")
          .AppendLine("        => _map.TryGetValue(typeof(TEvent), out SDL_EventType type) ? type : None;")
          .AppendLine()
          .AppendLine("    internal static Event Convert(in SDL_Event e) => e.Type switch")
          .AppendLine("    {");

        foreach (Event e in events)
        {
            string arg = e.Field is null ? string.Empty : $"e.{e.Field}";
            sb.AppendLine($"        SDL_EventType.{e.NativeEventType} => new {e.Name}({arg}),");
        }

        sb.AppendLine("        _ => default")
          .AppendLine("    };")
          .AppendLine("}");

        return SourceText.From(sb.ToString(), Encoding.UTF8);
    }

    private static string FieldName(string name) => $"_{char.ToLowerInvariant(name[0])}{name[1..]}";
}
