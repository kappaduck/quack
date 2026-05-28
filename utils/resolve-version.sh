#!/usr/bin/env bash
set -euo pipefail

PACKAGE_ID="${1:?Usage: resolve-version.sh <package-id> <version> <branch>}"
BASE_VERSION="${2:?Usage: resolve-version.sh <package-id> <version> <branch>}"
BRANCH="${3:?Usage: resolve-version.sh <package-id> <version> <branch>}"

if [[ "$BRANCH" != "develop" ]]; then
    echo "Resolved version $BASE_VERSION"
    echo "Version=$BASE_VERSION" >> "$GITHUB_OUTPUT"

    exit 0
fi

PACKAGE_ID_LOWER=$(echo "$PACKAGE_ID" | tr '[:upper:]' '[:lower:]')
URL="https://api.nuget.org/v3-flatcontainer/${PACKAGE_ID_LOWER}/index.json"

echo "Querying NuGet: $URL"
HTTP_CODE=$(curl -s -o /tmp/nuget.json -w "%{http_code}" "$URL" || true)

cat /tmp/nuget.json
echo

if [[ "$HTTP_CODE" == "404" ]]; then
    echo "Package not found on Nuget, starting at beta.1"
    LATEST_BETA=""
elif [[ "$HTTP_CODE" != "200" ]]; then
    echo "Error: unexpected HTTP $HTTP_CODE from NuGet. Aborting."
    exit 1
else
    LATEST_BETA=$(jq -r --arg base "$BASE_VERSION" '.versions[] | select(test("^" + $base + "-beta\\.[0-9]+$"))' /tmp/nuget.json | sed "s/^${BASE_VERSION}-beta\.//" | sort -n | tail -n1 || true)
fi

if [[ -z "$LATEST_BETA" ]]; then
    NEXT_BETA=1
else
    NEXT_BETA=$((LATEST_BETA + 1))
fi

RESOLVED_VERSION="${BASE_VERSION}-beta.${NEXT_BETA}"

echo "Resolved version: $RESOLVED_VERSION"
echo "Version=$RESOLVED_VERSION" >> "$GITHUB_OUTPUT"
