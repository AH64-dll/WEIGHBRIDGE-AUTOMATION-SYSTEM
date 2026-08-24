#!/bin/bash
DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

PREFIX="/home/amr/.local/opt/mono/usr"
export PATH="$PREFIX/bin:$PATH"
export LD_LIBRARY_PATH="$PREFIX/lib:$LD_LIBRARY_PATH"
export MONO_CFG_DIR="/home/amr/.local/opt/mono/etc"
export MONO_CONFIG="/home/amr/.local/opt/mono/etc/mono/config"
export MONO_PATH="$PREFIX/lib/mono/4.5:$MONO_PATH"

EXE="$DIR/WeighBridge/bin/Debug/Weighbridge.exe"

if [ ! -f "$EXE" ]; then
    echo "Building WeighbridgeApp..."
    cd "$DIR"
    xbuild /p:Configuration=Debug WeighBridge.sln
fi

echo "Starting WeighbridgeApp..."
cd "$DIR/WeighBridge/bin/Debug"
exec mono Weighbridge.exe "$@"
