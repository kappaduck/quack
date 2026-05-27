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
Linux_Runtime=artifacts/runtimes/linux-x64/native
mkdir -p ${Linux_Runtime}

# Build SDL3
git clone --branch ${SDL3} --depth 1 https://github.com/libsdl-org/SDL.git

cmake -S SDL -B SDL/build-linux \
      -DCMAKE_BUILD_TYPE=Release \
      && cmake --build SDL/build-linux --config Release \
      && cmake --install SDL/build-linux --config Release --prefix SDL/bin-linux \
      && strip SDL/bin-linux/lib/libSDL3.so.* \
      && cp -r SDL/bin-linux/lib/*.so* ${Linux_Runtime}

# Build SDL3_image
git clone --branch ${SDL3_Image} --depth 1 https://github.com/libsdl-org/SDL_image.git && SDL_image/external/download.sh

cmake -S SDL_image -B SDL_image/build-linux \
      -DSDLIMAGE_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DSDL3_DIR=../SDL/bin-linux/lib/cmake/SDL3 \
      && cmake --build SDL_image/build-linux --config Release \
      && cmake --install SDL_image/build-linux --config Release --prefix SDL_image/bin-linux \
      && strip SDL_image/bin-linux/lib/libSDL3_image.so.* \
      && cp -r SDL_image/bin-linux/lib/*.so* ${Linux_Runtime}

# Build SDL3_ttf
git clone --branch ${SDL3_ttf} --depth 1 https://github.com/libsdl-org/SDL_ttf.git && SDL_ttf/external/download.sh

cmake -S SDL_ttf -B SDL_ttf/build-linux \
      -DSDLTTF_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DSDL3_DIR=../SDL/bin-linux/lib/cmake/SDL3 \
      && cmake --build SDL_ttf/build-linux --config Release \
      && cmake --install SDL_ttf/build-linux --config Release --prefix SDL_ttf/bin-linux \
      && strip SDL_ttf/bin-linux/lib/libSDL3_ttf.so.* \
      && cp -r SDL_ttf/bin-linux/lib/*.so* ${Linux_Runtime}

# Build SDL3_mixer
git clone --branch ${SDL3_mixer} --depth 1 https://github.com/libsdl-org/SDL_mixer.git && SDL_mixer/external/download.sh

cmake -S SDL_mixer -B SDL_mixer/build-linux \
      -DSMIXER_VENDORED=ON \
      -DCMAKE_BUILD_TYPE=Release \
      -DSDL3_DIR=../SDL/bin-linux/lib/cmake/SDL3 \
      && cmake --build SDL_mixer/build-linux --config Release \
      && cmake --install SDL_mixer/build-linux --config Release --prefix SDL_mixer/bin-linux \
      && strip SDL_mixer/bin-linux/lib/libSDL3_mixer.so.* \
      && cp -r SDL_mixer/bin-linux/lib/*.so* ${Linux_Runtime}

# Cleanup
rm -rf SDL SDL_image SDL_ttf SDL_mixer

echo "SDL3-$SDL3 build complete."
