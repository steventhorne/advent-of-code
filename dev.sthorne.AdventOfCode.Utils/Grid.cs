using System.Collections.Generic;

namespace dev.sthorne.AdventOfCode.Utils
{
	public class Grid<TValue> : Dictionary<(int, int), TValue>
	{
		public int? LeftBound { get; private set; }
		public int? RightBound { get; private set; }
		public int? TopBound { get; private set; }
		public int? BottomBound { get; private set; }

		public int Width
		{
			get
			{
				if (!LeftBound.HasValue || !RightBound.HasValue)
					return 0;

				return RightBound.Value - LeftBound.Value + 1;
			}
		}

		public int Height
		{
			get
			{
				if (!TopBound.HasValue || !BottomBound.HasValue)
					return 0;

				return TopBound.Value - BottomBound.Value + 1;
			}
		}

		public Grid() : base() { }
		public Grid(int capacity) : base(capacity) { }

		public new TValue this[(int, int) key]
		{
			get
			{
				if (!ContainsKey(key))
					return default;

				return base[key];
			}
			set => SetValue(key, value);
		}

		public TValue this[int x, int y]
		{
			get
			{
				if (!ContainsKey((x, y)))
					return default;

				return base[(x, y)];
			}
			set => SetValue((x, y), value);
		}

		public void Add(int x, int y, TValue value)
		{
			SetValue((x, y), value);
		}

		private void SetValue((int X, int Y) key, TValue value)
		{
			if (!RightBound.HasValue
				|| !LeftBound.HasValue
				|| !TopBound.HasValue
				|| !BottomBound.HasValue)
			{
				RightBound = key.X;
				LeftBound = key.X;
				TopBound = key.Y;
				BottomBound = key.Y;
			}
			else
			{
				if (key.X > RightBound)
					RightBound = key.X;
				else if (key.X < LeftBound)
					LeftBound = key.X;

				if (key.Y > TopBound)
					TopBound = key.Y;
				else if (key.Y < BottomBound)
					BottomBound = key.Y;
			}

			base[key] = value;
		}
	}
}
