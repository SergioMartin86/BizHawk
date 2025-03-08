#pragma once

// system
#include <limits.h>
#include <stddef.h>
#include <stdarg.h>
#include <stdio.h>

// waterbox
#include "emulibc.h"
#include "waterboxcore.h"

const int KEY_COUNT = 0x65;

typedef struct
{
		int insertFloppyDisk;
		int insertCDROM;
} DriveActions;

typedef struct
{
		int up;
		int down;
		int left;
		int right;
		int button1;
		int button2;
} JoystickButtons;

typedef struct
{
	int posX;
	int posY;
	int dX;
	int dY;
	int leftButtonPressed;
	int middleButtonPressed;
	int rightButtonPressed;
	int leftButtonReleased;
	int middleButtonReleased;
	int rightButtonReleased;
	float sensitivity;
} MouseInput;

typedef struct
{
	FrameInfo base;
	char Keys[KEY_COUNT];
	DriveActions driveActions;
	JoystickButtons joy1;
	JoystickButtons joy2;
	MouseInput mouse;
} MyFrameInfo;
