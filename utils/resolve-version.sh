#!/usr/bin/env bash
set -euo pipefail

PACKAGE_ID="${1:?Usage: resolve-version.sh <package-id> <version> <branch>}"
BASE_VERSION="${2:?Usage: resolve-version.sh <package-id> <version> <branch>}"
BRANCH="${3:?Usage: resolve-version.sh <package-id> <version> <branch>}"


if [[ "$BRANCH" == "develop" ]]; then
    VERSIONS=$(curl -sf "https://api.nuget.org/v3-flatcontainer/${PACKAGE_ID}/index.json" | jq -r '.versions[]')

    LATEST_BETA=$(echo "$VERSIONS" | grep -E "^${BASE_VERSION}-beta\.[0-9]+$" | sort -t. -k4 -V | tail -n1)

    if [[ -z "$LATEST_BETA" ]]; then
        NEXT_BETA=1
    else
        CURRENT_X=$(echo "$LATEST_BETA" | grep -oE '[0-9]+$')
        NEXT_BETA=$((CURRENT_X + 1))
    fi

    RESOLVED_VERSION="${BASE_VERSION}-beta.${NEXT_BETA}"
else
    RESOLVED_VERSION="${BASE_VERSION}"
fi

echo "Resolved version: $RESOLVED_VERSION"
echo "Version=$RESOLVED_VERSION" >> "$GITHUB_OUTPUT"
