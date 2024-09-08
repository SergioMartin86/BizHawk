using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using BizHawk.Common;
using BizHawk.Common.CollectionExtensions;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	internal class Bk2Controller : IMovieController
	{
		private readonly Dictionary<string, int> _myAxisControls = new();

		private readonly Dictionary<string, bool> _myBoolButtons = new();

		private readonly Bk2ControllerDefinition _type;

		private IReadOnlyList<ControlMap> _controlsOrdered;

		private IReadOnlyList<ControlMap> ControlsOrdered
			=> _controlsOrdered
				??= _type.OrderedControlsFlat.Select(name => new ControlMap(name, _type)).ToArray();

		public IInputDisplayGenerator InputDisplayGenerator { get; set; } = null;

		public Bk2Controller(string key, ControllerDefinition definition) : this(definition)
		{
			if (!string.IsNullOrEmpty(key))
			{
				var groups = key.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

				_type.ControlsFromLog = groups
					.Select(group => group.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList())
					.ToList();
			}
		}

		public Bk2Controller(ControllerDefinition definition)
		{
			_type = new Bk2ControllerDefinition(definition);
			foreach ((string axisName, AxisSpec range) in definition.Axes)
			{
				_myAxisControls[axisName] = range.Neutral;
			}
		}

		public ControllerDefinition Definition => _type;

		public int AxisValue(string name)
			=> _myAxisControls.GetValueOrDefault(name);

		public bool IsPressed(string button)
			=> _myBoolButtons.GetValueOrDefault(button);

		public IReadOnlyCollection<(string Name, int Strength)> GetHapticsSnapshot() => Array.Empty<(string, int)>();

		public void SetHapticChannelStrength(string name, int strength) {}

		public void SetFrom(IController source)
		{
			for (int index = 0; index < Definition.BoolButtons.Count; index++)
			{
				string button = Definition.BoolButtons[index];
				_myBoolButtons[button] = source.IsPressed(button);
			}

			foreach (var name in Definition.Axes.Keys)
			{
				_myAxisControls[name] = source.AxisValue(name);
			}
		}

		public void SetFromSticky(IStickyAdapter controller)
		{
			foreach (var button in Definition.BoolButtons)
			{
				_myBoolButtons[button] = controller.IsSticky(button);
			}

			// axes don't have sticky logic, so latch default value
			foreach (var (k, v) in Definition.Axes) _myAxisControls[k] = v.Neutral;
		}

		public void SetFromMnemonic(string mnemonic)
		{
			if (string.IsNullOrWhiteSpace(mnemonic)) return;
			var iterator = 0;

			foreach (var key in ControlsOrdered)
			{
				while (mnemonic[iterator] == '|') iterator++;

				if (key.IsBool)
				{
					_myBoolButtons[key.Name] = mnemonic[iterator] != '.';
					iterator++;
				}
				else if (key.IsAxis)
				{
					var commaIndex = mnemonic.IndexOf(',', iterator);
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
					var val = int.Parse(mnemonic.AsSpan(start: iterator, length: commaIndex - iterator));
#else
					var axisValueString = mnemonic.Substring(startIndex: iterator, length: commaIndex - iterator);
					var val = int.Parse(axisValueString);
#endif
					_myAxisControls[key.Name] = val;

					iterator = commaIndex + 1;
				}
			}
		}

		public void SetBool(string buttonName, bool value)
		{
			_myBoolButtons[buttonName] = value;
		}

		public void SetAxis(string buttonName, int value)
		{
			_myAxisControls[buttonName] = value;
		}

		private readonly struct ControlMap
		{
			public readonly bool IsAxis;

			public readonly bool IsBool;

			public readonly string Name;

			public ControlMap(string name, bool isButton, bool isAxis)
			{
				Debug.Assert(isButton ^ isAxis, "axis conflicts with button of the same name?");
				Name = name;
				IsBool = isButton;
				IsAxis = isAxis;
			}

			public ControlMap(string name, ControllerDefinition def)
				: this(
					name: name,
					isButton: def.BoolButtons.Contains(name),
					isAxis: def.Axes.ContainsKey(name)) {}
		}

		private class Bk2ControllerDefinition : ControllerDefinition
		{
			public IReadOnlyList<IReadOnlyList<string>> ControlsFromLog = null;

			public Bk2ControllerDefinition(ControllerDefinition source)
				: base(source)
			{
			}

			protected override IReadOnlyList<IReadOnlyList<string>> GenOrderedControls()
				=> ControlsFromLog is not null && ControlsFromLog.Count is not 0 ? ControlsFromLog : base.GenOrderedControls();
		}
	}
}
