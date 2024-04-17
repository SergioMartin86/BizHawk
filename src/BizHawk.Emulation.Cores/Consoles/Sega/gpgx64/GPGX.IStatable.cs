using System.IO;
using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.Consoles.Sega.gpgx
{
	public partial class GPGX : IStatable
	{
		public bool AvoidRewind => false;

		private byte[] _saveStateBuff;

		private void InitSaveStateBuff()
		{
			int size = Core.gpgx_state_size();
			_saveStateBuff = new byte[size];

			// any managed pointers that we sent to the core need to be resent now!
			Core.gpgx_set_input_callback(_inputCallback);
			RefreshMemCallbacks();
			Core.gpgx_set_cdd_callback(cd_callback_handle);
			Core.gpgx_invalidate_pattern_cache();
			Core.gpgx_set_draw_mask(_settings.GetDrawMask());
			Core.gpgx_set_sprite_limit_enabled(!_settings.NoSpriteLimit);
			UpdateVideo();
		}

		public void SaveStateBinary(BinaryWriter writer)
		{
			Core.gpgx_state_save(_saveStateBuff);
			writer.Write(_saveStateBuff);
		}

		public void LoadStateBinary(BinaryReader reader)
		{
			reader.Read(_saveStateBuff, 0, _saveStateBuff.Length);
			Core.gpgx_state_load(_saveStateBuff);
		}
	}
}
