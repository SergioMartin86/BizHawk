using System.Runtime.InteropServices;
using BizHawk.BizInvoke;
using BizHawk.Emulation.Cores.Waterbox;

namespace BizHawk.Emulation.Consoles._3DO
{
	public abstract class LibOpera : LibWaterboxCore
	{
		// NTSC Specifications
		public const int NTSC_WIDTH = 320;
		public const int NTSC_HEIGHT = 240;
		public const int NTSC_VIDEO_NUMERATOR = 60;
		public const int NTSC_VIDEO_DENOMINATOR = 1;

		// PAL1 Specifications
		public const int PAL1_WIDTH = 320;
		public const int PAL1_HEIGHT = 288;
		public const int PAL1_VIDEO_NUMERATOR = 50;
		public const int PAL1_VIDEO_DENOMINATOR = 1;

		// PAL2 Specifications
		public const int PAL2_WIDTH = 384;
		public const int PAL2_HEIGHT = 288;
		public const int PAL2_VIDEO_NUMERATOR = 50;
		public const int PAL2_VIDEO_DENOMINATOR = 1;

		[UnmanagedFunctionPointer(CC)]
		public delegate void CDReadCallback(int lba, IntPtr dst);

		[UnmanagedFunctionPointer(CC)]
		public delegate int CDSectorCountCallback();

		[BizImport(CC)]
		public abstract void SetCdCallbacks(CDReadCallback cdrc, CDSectorCountCallback cdscc);

		[BizImport(CC)]
		public abstract void ejectCD();

		[BizImport(CC)]
		public abstract void insertCD();

		[BizImport(CC, Compatibility = true)]
		public abstract bool Init(string gameFile, string biosFile, string fontFile, int port1Type, int port2Type, int videoStandard);

		[BizImport(CC, Compatibility = true)]
		public abstract bool sram_changed();

		[BizImport(CC, Compatibility = true)]
		public abstract int get_sram_size();

		[BizImport(CC, Compatibility = true)]
		public abstract void get_sram(IntPtr sramBuffer);

		[BizImport(CC, Compatibility = true)]
		public abstract void set_sram(IntPtr sramBuffer);

		[StructLayout(LayoutKind.Sequential)]
		public struct GamepadInputs
		{
			public bool up;
			public bool down;
			public bool left;
			public bool right;
			public bool start;
			public bool select;
			public bool buttonA;
			public bool buttonB;
			public bool buttonX;
			public bool buttonY;
			public bool buttonL;
			public bool buttonR;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MouseInputs
		{
			public int dX;
			public int dY;
			public bool leftButton;
			public bool middleButton;
			public bool rightButton;
			public bool fourthButton;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FlightStickInputs
		{
			public bool up;
			public bool down;
			public bool left;
			public bool right;
			public bool fire;
			public bool buttonA;
			public bool buttonB;
			public bool buttonC;
			public bool buttonX;
			public bool buttonP;
			public bool leftTrigger;
			public bool rightTrigger;
			public int horizontalAxis;
			public int verticalAxis;
			public int altitudeAxis;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LightGunInputs
		{
			public bool trigger;
			public bool select;
			public bool reload;
			public bool isOffScreen;
			public int screenX;
			public int screenY;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ArcadeLightGunInputs
		{
			public bool trigger;
			public bool select;
			public bool start;
			public bool reload;
			public bool auxA;
			public bool isOffScreen;
			public int screenX;
			public int screenY;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct OrbatakTrackballInputs
		{
			public int dX;
			public int dY;
			public bool startP1;
			public bool startP2;
			public bool coinP1;
			public bool coinP2;
			public bool service;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct GameInput
		{
			public GamepadInputs gamepad;
			public MouseInputs mouse;
			public FlightStickInputs flightStick;
			public LightGunInputs lightGun;
			public ArcadeLightGunInputs arcadeLightGun;
			public OrbatakTrackballInputs orbatakTrackball;
		}

		[StructLayout(LayoutKind.Sequential)]
		public new class FrameInfo : LibWaterboxCore.FrameInfo
		{
			public GameInput port1;
			public GameInput port2;
			public bool isReset = false;
		}
	}
}