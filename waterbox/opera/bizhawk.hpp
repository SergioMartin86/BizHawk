#pragma once

// system
#include <limits.h>
#include <stddef.h>
#include <stdarg.h>
#include <stdio.h>

// waterbox
#include "emulibc.h"
#include "waterboxcore.h"

struct gamePad_t
{
	bool up;
	bool down;
	bool left;
	bool right;
	bool start;
	bool select;
	bool buttonA;
	bool buttonB;
	bool buttonX;
	bool buttonY;
	bool buttonL;
	bool buttonR;
};

struct mouse_t
{
	int dX;
	int dY;
	bool leftButton;
	bool middleButton;
	bool rightButton;
	bool fourthButton;
};

struct flightStick_t
{
	bool up;
	bool down;
	bool left;
	bool right;
	bool fire;
	bool buttonA;
	bool buttonB;
	bool buttonC;
	bool buttonX;
	bool buttonP;
	bool leftTrigger;
	bool rightTrigger;
	int horizontalAxis;
	int verticalAxis;
	int altitudeAxis;
};

struct lightGun_t
{
 bool trigger;
 bool select;
 bool reload;
 bool isOffScreen;
 int screenX;
 int screenY;
};

struct arcadeLightGun_t
{
 bool trigger;
 bool select;
	bool start;
 bool reload;
	bool auxA;
 bool isOffScreen;
 int screenX;
 int screenY;
};

struct OrbatakTrackball_t
{
	int dX;
	int dY;
	bool startP1;
	bool startP2;
	bool coinP1;
	bool coinP2;
	bool service;
};

struct controllerData_t
{
	gamePad_t gamePad;
	mouse_t mouse;
	flightStick_t flightStick;
	lightGun_t lightGun;
	arcadeLightGun_t arcadeLightGun;
	OrbatakTrackball_t orbatakTrackball;
};

typedef struct
{
	FrameInfo base;
	controllerData_t port1;
	controllerData_t port2;
	bool isReset;
} MyFrameInfo;

struct MemoryAreas 
{
  uint8_t* systemRAM;
  uint8_t* videoRAM;
  uint8_t* nonVolatileRAM;
};

struct MemorySizes
{
  size_t systemRAM;
  size_t videoRAM;
  size_t nonVolatileRAM;
};

