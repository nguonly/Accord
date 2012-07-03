using System;

namespace Accord.Music
{
	/// <summary>
	///   Interval Class
	/// </summary>
	/// <remarks>
	///   In musical set theory, an interval class is the shortest distance
	///   in pitch class space between two unordered pitch classes. 
	/// </remarks>
	public struct IntervalClass
	{
		private int label;
		

		public IntervalClass(int label)
		{
			this.label = label;
		}
		
		
		public static readonly Interval[][] Classes =
		{
			new Interval[] { new Interval(0) },
			new Interval[] { new Interval(1), new Interval(11) },
			new Interval[] { new Interval(2), new Interval(10) },
			new Interval[] { new Interval(3), new Interval(9) },
			new Interval[] { new Interval(4), new Interval(8) },
			new Interval[] { new Interval(5), new Interval(7) },
			new Interval[] { new Interval(6)  },
		};
	}
}
