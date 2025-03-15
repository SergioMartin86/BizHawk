#ifndef __BIZHAWK_INTERFACE__
#define __BIZHAWK_INTERFACE__

#include "emulibc.h"
#include "d_player.h"
#include "doomstat.h"
#include "g_game.h"
#include "p_mobj.h"
#include "w_wad.h"
#include "dsda/args.h"

extern int headlessMain(int argc, char **argv);
extern void headlessRunSingleTick();
extern void headlessClearTickCommand();
extern void headlessSetTickCommand(int playerId, int forwardSpeed, int strafingSpeed, int turningSpeed, int fire, int action, int weapon, int automap, int lookfly, int artifact, int jump, int endPlayer);
extern void headlessGetMapName(char *outString);
extern void headlessSetSaveStatePointer(void *savePtr, int saveStateSize);
extern size_t headlessGetEffectiveSaveSize();
extern void dsda_ArchiveAll(void);
extern void dsda_UnArchiveAll(void);
extern void D_AddFile (const char *file, wad_source_t source, void *const buffer, const size_t size);
extern void AddIWAD(const char *iwad, void *const buffer, const size_t size);

// Video
extern void headlessUpdateVideo(void);
extern void* headlessGetVideoBuffer();
extern int headlessGetVideoPitch();
extern int headlessGetVideoWidth();
extern int headlessGetVideoHeight();
extern void headlessEnableVideoRendering();
extern void headlessDisableVideoRendering();
extern uint32_t* headlessGetPallette();

// Audio
extern void headlessUpdateSounds(void);
extern void headlessEnableAudioRendering();
extern void headlessDisableAudioRendering();
extern void I_InitSound(void);
extern void I_SetSoundCap (void);
extern uint8_t *I_CaptureAudio (int *nsamples);

// Players information
extern int enableOutput;
extern int preventLevelExit;
extern int preventGameEnd;
extern int reachedLevelExit;
extern int reachedGameEnd;
extern int numthings;
extern mobj_t **mobj_ptrs;
extern dsda_arg_t arg_value[dsda_arg_count];

#define PALETTE_SIZE 256
uint32_t _convertedPaletteBuffer[PALETTE_SIZE];

enum MemoryArrayType
{
  ARRAY_THINGS = 0,
  ARRAY_LINES = 1,
  ARRAY_SECTORS = 2
};

struct InitSettings
{
  int Player1Present;
  int Player2Present;
  int Player3Present;
  int Player4Present;
  int Player1Class;
  int Player2Class;
  int Player3Class;
  int Player4Class;
  int PreventLevelExit;
  int PreventGameEnd;
} __attribute__((packed));

struct PackedPlayerInput
{
  int RunSpeed;
  int StrafingSpeed;
  int TurningSpeed;
  int WeaponSelect;
  int Fire;
  int Action;
  int Automap;
  int FlyLook;
  int ArtifactUse;
  int Jump;
  int EndPlayer;
} __attribute__((packed));

struct PackedRenderInfo
{
  int RenderVideo;
  int RenderAudio;
  int PlayerPointOfView;
} __attribute__((packed));

#endif