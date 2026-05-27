#!/usr/bin/env bash
set -euo pipefail

: "${SDL3:?SDL3 version not set}"
: "${SDL3_IMAGE:?SDL3_Image version not set}"
: "${SDL3_TTF:?SDL3_ttf version not set}"
: "${SDL3_MIXER:?SDL3_mixer version not set}"

echo "Building SDL3-$SDL3..."

# Set SDL3 versions
SDL3=release-$SDL3
SDL3_Image=release-$SDL3_IMAGE
SDL3_ttf=release-$SDL3_TTF
SDL3_mixer=release-$SDL3_MIXER

# Set runtime directories
Windows_Runtime=artifacts/runtimes/win-x64/native
mkdir -p ${Windows_Runtime}

# Build SDL3
git clone --branch ${SDL3} --depth 1 https://github.com/libsdl-org/SDL.git

cmake -S SDL -B SDL/build-windows -G Ninja \
      -DCMAKE_BUILD_TYPE=Release \
      -DCMAKE_TOOLCHAIN_FILE=../utils/mingw.cmake \
      && cmake --build SDL/build-windows --config Release \
      && cmake --install SDL/build-windows --config Release --prefix SDL/bin-windows \
      && x86_64-w64-mingw32-strip SDL/bin-windows/bin/SDL3.dll \
      && cp -r SDL/bin-windows/bin/SDL3.dll ${Windows_Runtime}

# Build SDL3_image
git clone --branch ${SDL3_Image} --depth 1 https://github.com/libsdl-org/SDL_image.git && SDL_image/external/download.sh

cmake -S SDL_image -B SDL_image/build-windows -G Ninja \
      -DSDLIMAGE_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DCMAKE_TOOLCHAIN_FILE=../utils/mingw.cmake \
      -DSDL3_DIR=../SDL/bin-windows/lib/cmake/SDL3 \
      && cmake --build SDL_image/build-windows --config Release \
      && cmake --install SDL_image/build-windows --config Release --prefix SDL_image/bin-windows \
      && x86_64-w64-mingw32-strip SDL_image/bin-windows/bin/SDL3_image.dll \
      && cp -r SDL_image/bin-windows/bin/*.dll ${Windows_Runtime}

# Build SDL3_ttf
git clone --branch ${SDL3_ttf} --depth 1 https://github.com/libsdl-org/SDL_ttf.git && SDL_ttf/external/download.sh

cmake -S SDL_ttf -B SDL_ttf/build-windows -G Ninja \
      -DSDLTTF_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DCMAKE_TOOLCHAIN_FILE=../utils/mingw.cmake \
      -DSDL3_DIR=../SDL/bin-windows/lib/cmake/SDL3 \
      && cmake --build SDL_ttf/build-windows --config Release \
      && cmake --install SDL_ttf/build-windows --config Release --prefix SDL_ttf/bin-windows \
      && x86_64-w64-mingw32-strip SDL_ttf/bin-windows/bin/SDL3_ttf.dll \
      && cp -r SDL_ttf/bin-windows/bin/*.dll ${Windows_Runtime}

# Build SDL3_mixer
git clone --branch ${SDL3_mixer} --depth 1 https://github.com/libsdl-org/SDL_mixer.git && SDL_mixer/external/download.sh

cmake -S SDL_mixer -B SDL_mixer/build-windows -G Ninja \
      -DSDLMIXER_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DCMAKE_TOOLCHAIN_FILE=../utils/mingw.cmake \
      -DSDL3_DIR=../SDL/bin-windows/lib/cmake/SDL3 \
      && cmake --build SDL_mixer/build-windows --config Release \
      && cmake --install SDL_mixer/build-windows --config Release --prefix SDL_mixer/bin-windows \
      && x86_64-w64-mingw32-strip SDL_mixer/bin-windows/bin/SDL3_mixer.dll \
      && cp -r SDL_mixer/bin-windows/bin/*.dll ${Windows_Runtime}

# Cleanup
rm -rf SDL SDL_image SDL_ttf SDL_mixer

echo "SDL3-$SDL3 build complete."
