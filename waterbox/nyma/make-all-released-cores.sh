#!/bin/sh
set -e

make -f turbo.mak $1 -j$(nproc)
make -f hyper.mak $1 -j$(nproc)
make -f ngp.mak $1 -j$(nproc)
make -f faust.mak $1 -j$(nproc)
make -f pcfx.mak $1 -j$(nproc)
make -f ss.mak $1 -j$(nproc)
make -f shock.mak $1 -j$(nproc)
# make -f lynx.mak $1 -j
make -f vb.mak $1 -j$(nproc)
# make -f wswan.mak $1 -j
