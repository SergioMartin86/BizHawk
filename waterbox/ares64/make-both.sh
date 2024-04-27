#!/bin/sh
set -e

make -f interpreter.mak $1 -j$(nproc)
make -f recompiler.mak $1 -j$(nproc)
