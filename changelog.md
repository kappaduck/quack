# Quack! changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased] &#8212; YYYY-MM-DD

<!-- Unreleased -->

## 0.3.0 &#8212; 2026-01-01

Major overhaul of the public API with breaking changes ([#272], [#273], [#275])

### Added

- Linux support ([#15])

### Changed

- Native window handle now returns `X11` or `Wayland` handle on linux depending on the backend used. ([#15])

<!-- 0.3.0 -->
[#15]: https://github.com/kappaduck/quack/issues/15
[#272]: https://github.com/kappaduck/quack/issues/272
[#273]: https://github.com/kappaduck/quack/issues/273
[#275]: https://github.com/kappaduck/quack/issues/275

## 0.2.0 &#8212; 2025-12-03

### Added

- SDL_Image support ([#8], [#53])
- SDL_TTF support ([#10], [#49])
- Added Image to load and manipulate images ([#138])
- Added Texture to handle GPU textures ([#111])
- Added Sprite to draw textures on the screen ([#121])
- System menu support ([#237])
- Added Surface to handle pixel data in CPU memory ([#131])
- Palette support for surfaces ([#140])
- Cursor support ([#64])
- Render can now render debug text ([#241], [#225])
- Window can now have an icon ([#238])
- Added Transform ([#78])

### Changed

- Updated SDL to version 3.2.28
- Replace custom vector2 implementation with System.Numerics.Vector2 ([#257])

### Fixed

- Window now use the native handle correctly depending on the platform ([#207])
- Fixed issue with RenderWindow will fail to render after closing the window ([#250])

<!-- 0.2.0 -->
[#8]: https://github.com/kappaduck/quack/issues/8
[#10]: https://github.com/kappaduck/quack/issues/10
[#49]: https://github.com/kappaduck/quack/issues/49
[#53]: https://github.com/kappaduck/quack/issues/53
[#64]: https://github.com/kappaduck/quack/issues/64
[#78]: https://github.com/kappaduck/quack/issues/78
[#111]: https://github.com/kappaduck/quack/issues/111
[#121]: https://github.com/kappaduck/quack/issues/121
[#131]: https://github.com/kappaduck/quack/issues/131
[#138]: https://github.com/kappaduck/quack/issues/138
[#140]: https://github.com/kappaduck/quack/issues/140
[#207]: https://github.com/kappaduck/quack/issues/207
[#225]: https://github.com/kappaduck/quack/issues/225
[#237]: https://github.com/kappaduck/quack/issues/237
[#238]: https://github.com/kappaduck/quack/issues/238
[#241]: https://github.com/kappaduck/quack/issues/241
[#250]: https://github.com/kappaduck/quack/issues/250
[#257]: https://github.com/kappaduck/quack/issues/257

## 0.1.0 &#8212; 2025-08-24

First release of Quack!

Focus on bare essentials enough to make a Snake or Pong game and use input like keyboard/mouse.

### Added

- Input handling for keyboard and mouse events ([#86], [#89])
- Event handling ([#83])
- Window which is a native OS window with no graphics context. ([#105])
- RenderWindow which is a window that can be used to render 2D graphics using SDL renderer API. ([#113], [#116], [#125], [#130])
- IDrawable interface for objects that can be drawn on the screen. ([#114])
- Pixel format with details and extensions ([#126])
- Color management ([#119])
- Vertex ([#112])
- Blend modes ([#124])
- Vector2 and Vector2Int ([#73], [#74])
- Rect and RectInt ([#79], [#80])
- Angle ([#81])
- Displays ([#68])
- Video drivers ([#69])
- Renderer drivers ([#109])
- Quack engine ([#43], [#47])
- Power management ([#60])
- System preference management ([#61])
- Launch any URL from the system ([#62])

<!-- 0.1.0 -->
[#43]: https://github.com/kappaduck/quack/issues/43
[#47]: https://github.com/kappaduck/quack/issues/47
[#60]: https://github.com/kappaduck/quack/issues/60
[#61]: https://github.com/kappaduck/quack/issues/61
[#62]: https://github.com/kappaduck/quack/issues/62
[#68]: https://github.com/kappaduck/quack/issues/68
[#69]: https://github.com/kappaduck/quack/issues/69
[#73]: https://github.com/kappaduck/quack/issues/73
[#74]: https://github.com/kappaduck/quack/issues/74
[#79]: https://github.com/kappaduck/quack/issues/79
[#80]: https://github.com/kappaduck/quack/issues/80
[#81]: https://github.com/kappaduck/quack/issues/81
[#83]: https://github.com/kappaduck/quack/issues/83
[#86]: https://github.com/kappaduck/quack/issues/86
[#89]: https://github.com/kappaduck/quack/issues/89
[#105]: https://github.com/kappaduck/quack/issues/105
[#109]: https://github.com/kappaduck/quack/issues/109
[#112]: https://github.com/kappaduck/quack/issues/112
[#113]: https://github.com/kappaduck/quack/issues/113
[#114]: https://github.com/kappaduck/quack/issues/114
[#116]: https://github.com/kappaduck/quack/issues/116
[#119]: https://github.com/kappaduck/quack/issues/119
[#124]: https://github.com/kappaduck/quack/issues/124
[#125]: https://github.com/kappaduck/quack/issues/125
[#126]: https://github.com/kappaduck/quack/issues/126
[#130]: https://github.com/kappaduck/quack/issues/130
