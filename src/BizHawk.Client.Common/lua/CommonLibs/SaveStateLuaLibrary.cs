namespace BizHawk.Client.Common
{
	public sealed class SaveStateLuaLibrary : LuaLibraryBase
	{
		public SaveStateLuaLibrary(ILuaLibraries luaLibsImpl, ApiContainer apiContainer, Action<string> logOutputCallback)
			: base(luaLibsImpl, apiContainer, logOutputCallback) {}

		public override string Name => "savestate";

		[LuaMethodExample("savestate.load( \"C:\\state.bin\" );")]
		[LuaMethod("load", "Loads a savestate with the given path. Returns true iff succeeded. If EmuHawk is deferring quicksaves, to TAStudio for example, that form will do what it likes (and the path is ignored).")]
		public bool Load(string path, bool suppressOSD = false)
		{
			if (_luaLibsImpl.IsInInputOrMemoryCallback)
			{
				throw new InvalidOperationException("savestate.load() is not allowed during input/memory callbacks");
			}

			_luaLibsImpl.IsUpdateSupressed = true;
			try
			{
				return APIs.SaveState.Load(path, suppressOSD);
			}
			catch
			{
				return false;
			}
			finally
			{
				_luaLibsImpl.IsUpdateSupressed = false;
			}
		}

		[LuaMethodExample("savestate.loadslot( 7 );")]
		[LuaMethod("loadslot", "Loads the savestate at the given slot number (must be an integer between 1 and 10). Returns true iff succeeded. If EmuHawk is deferring quicksaves, to TAStudio for example, that form will do what it likes with the slot number.")]
		public bool LoadSlot(int slotNum, bool suppressOSD = false)
		{
			if (_luaLibsImpl.IsInInputOrMemoryCallback)
			{
				throw new InvalidOperationException("savestate.loadslot() is not allowed during input/memory callbacks");
			}

			_luaLibsImpl.IsUpdateSupressed = true;
			try
			{
				return APIs.SaveState.LoadSlot(slotNum, suppressOSD: suppressOSD);
			}
			catch
			{
				return false;
			}
			finally
			{
				_luaLibsImpl.IsUpdateSupressed = false;
			}
		}

		[LuaMethodExample("savestate.save( \"C:\\state.bin\" );")]
		[LuaMethod("save", "Saves a state at the given path. If EmuHawk is deferring quicksaves, to TAStudio for example, that form will do what it likes (and the path is ignored).")]
		public bool Save(string path, bool suppressOSD = false)
		{
			if (_luaLibsImpl.IsInInputOrMemoryCallback)
			{
				throw new InvalidOperationException("savestate.save() is not allowed during input/memory callbacks");
			}
			try
			{
				APIs.SaveState.Save(path, suppressOSD);
				return true;
			}
			catch
			{
				return false;
			}
		}

		[LuaMethodExample("savestate.saveslot( 7 );")]
		[LuaMethod("saveslot", "Saves a state at the given save slot (must be an integer between 1 and 10). If EmuHawk is deferring quicksaves, to TAStudio for example, that form will do what it likes with the slot number.")]
		public bool SaveSlot(int slotNum, bool suppressOSD = false)
		{
			if (_luaLibsImpl.IsInInputOrMemoryCallback)
			{
				throw new InvalidOperationException("savestate.saveslot() is not allowed during input/memory callbacks");
			}
			try
			{
				APIs.SaveState.SaveSlot(slotNum, suppressOSD);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
