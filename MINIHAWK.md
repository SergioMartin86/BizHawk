# miniHawk — Project Charter

A minimal, core-agnostic TAS frontend derived from BizHawk. Emulation cores are removed
from the tree and loaded on-the-fly as external, self-contained packages just before ROM load.

## Objectives (testable)

1. **Zero core code in the repo.** miniHawk builds and runs with no emulation core in the
   solution. Cores arrive as single-file packages (`.zip`: manifest + managed adapter DLL +
   native DLL(s)), discovered from a `Cores/` directory and loaded at ROM-load time.
2. **Published core contract.** The core-facing API (essentially `BizHawk.Emulation.Common`
   + `BizHawk.BizInvoke`) becomes a versioned, published contract. A core package must be
   buildable outside the miniHawk repo against that contract alone.
3. **Determinism witness.** The QuickerNES regression suite (34 test movies in
   `quicknes/core/tests/`, from github.com/SergioMartin86/quickerNES) must resync to
   success at every phase boundary, including after the core is evicted to an external
   package. Running the full suite green at every step is paramount to conserving
   correctness. Test ROMs live at `C:\Users\sergiom\Documents\TAS\roms\nes` (file names
   match the `.test` files' `Rom File` entries; SHA1-verified).
4. **TAS-only frontend.** Kept: TAStudio, movie record/playback, savestates, rewind, frame
   advance, virtual pads, RAM Watch/Search, Hex Editor, Cheats, Lua console (core-agnostic
   APIs only), A/V dumping + screenshots, core-agnostic debug tools (trace logger, CDL,
   generic disassembler). Everything else — core-specific config dialogs and viewers,
   movie importers, RetroAchievements, per-system Lua libs, etc. — is removed.

## Agreed decisions

- **Core package interface:** managed .NET adapter DLL implementing `IEmulator` (+ service
  interfaces) against the published contract, bundled with its native core DLL(s).
  Not a C ABI, not libretro.
- **Repo strategy:** trim this BizHawk checkout in place on a branch. Keep git history and
  upstream diffability. Rename to miniHawk once stable.
- **Reload model:** load-once per process (net48 cannot unload assemblies). Packages load
  lazily at ROM-load time; swapping a loaded core's version requires app restart.
- **Witness core:** QuickNes (backed by the TASEmulators/quickerNES submodule) — single
  native DLL, no waterbox, no discs, no firmware. It stays in-tree until Phase 3.

## Procedure

Each phase ends with: build green → witness movie syncs → commit. Never more than one
phase of unverified change.

- **Phase 0 — Baseline & harness.** Build as-is. Stand up the two-level witness harness
  (below), generate golden RAM dumps from unmodified BizHawk, and confirm they agree with
  the native quickerNES tester's ground truth. No product code changes.
- **Phase 1 — Shrink to one core, statically.** Remove all cores except QuickNes from the
  build; delete frontend features that referenced them (~26 files in Client.Common,
  ~70 in Client.EmuHawk reach into concrete core types). No architecture changes.
- **Phase 2 — Publish the contract.** Extract the core API layer; invert `CoreInventory`
  (src/BizHawk.Emulation.Cores/CoreInventory.cs) to accept externally loaded assemblies;
  harden settings/sync-settings (de)serialization against types from external assemblies
  (movie sync-settings JSON embeds type info — known sharp edge).
- **Phase 3 — Evict the witness.** Move the QuickNes adapter + native DLL out of the
  solution into a standalone package built against the published contract; load from
  `.zip` at runtime. Phase 0 movie must still sync bit-identically.
- **Phase 4 — Harden & prove generality.** Manifest/versioning, package validation,
  error UX, core-author docs. Port a second core to prove the contract isn't
  QuickerNES-shaped.

## Witness harness (two levels, both must pass at every phase boundary)

The quickerNES test format: each `.test` is JSON naming a ROM (+ expected SHA1, optional
initial `.state`, controller types) and a `.sol` input sequence (one line per frame,
jaffar format, e.g. `|..|........|`). The native tester (`quicknes/core/source/tester.cpp`)
replays the sequence and emits a MetroHash of NES low RAM (2KB) as the verdict; cycle
types `Rerecord`/`Full` additionally do a savestate save+load around every frame, which
is exactly the TAS-critical property.

- **Level A — core payload guard.** Build and run the native quickerNES tester over all
  34 tests. Validates that the native DLL we package never drifts. Runs everything,
  including the two tests with initial `.state` files.
- **Level B — full-stack witness (the real one).** Drive EmuHawk itself: load ROM, feed
  the `.sol` inputs through the frontend input pipeline (Lua harness or generated `.bk2`
  movies), dump final 2KB RAM domain, byte-compare against golden dumps recorded from
  unmodified BizHawk in Phase 0. Also run a per-frame savestate save/load variant
  (mirroring `Rerecord` cycle type) to exercise the frontend statable path.
  `bizinterface.cpp` already supports the Arkanoid paddle types the arkanoid tests need.

Level B sharp edges, to resolve in Phase 0: input-string → BizHawk controller mapping
must be validated button-by-button; the two initial-`.state` tests (microMachines,
saiyuukiWorld.lastHalf) use quickerNES-native state format and may remain Level-A-only;
BizHawk power-on state must be confirmed identical to the bare core's.

## Architecture facts informing the plan

- `CoreInventory` already discovers cores via reflection (`[Core]` + `[CoreConstructor]`
  attributes) and its constructor already accepts arbitrary assembly type lists — the
  plugin inversion is small.
- Only `BizHawk.Client.Common` references `Emulation.Cores` at the project level; the real
  coupling is ~96 frontend files using concrete core types, mostly deletable for miniHawk.
- `GenericCoreConfig` (reflection-based settings UI) already exists; per-core config
  dialogs are not needed.
- EmuHawk targets `net48`: no assembly unload, hence the load-once model.
- Determinism is sacred: anything touching emulation, input, or timing must preserve
  frame-exact reproducibility or movies desync.
