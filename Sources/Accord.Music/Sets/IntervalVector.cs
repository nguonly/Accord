using System;
using System.Text;

namespace Accord.Music.Sets
{
	/// <summary>
	///   An interval vector is an array that expresses the
	///   intervallic content of a pitch-class set.
	/// </summary>
	/// <remarks>
	///   In 12 equal temperament the interval vector has six
	///   digits, with each digit standing for the number of
	///   times an interval class appears in the set. (Interval
	///   classes, not regular intervals, must be used, in order
	///   that the interval vector remains the same, regardless
	///   of the set's permutation or vertical arrangement.)
	/// 
	///   The interval classes represented by each digit ascend
	///   from left to right. That is:
	/// 
	///     1) minor seconds/major sevenths (1 or 11 semitones)
	///     2) major seconds/minor sevenths (2 or 10 semitones)
	///     3) minor thirds/major sixths (3 or 9 semitones)
	///     4) major thirds/minor sixths (4 or 8 semitones)
	///     5) perfect fourths/perfect fifths (5 or 7 semitones)
	///     6) tritones (6 semitones) (The tritone is inversionally related to itself.)
	/// 
	///   Interval class 0 (representing unisons and octaves) is omitted.
	/// </remarks>
	public class IntervalVector
	{
		private int[] intervals;
		
		public int this[int intervalClass]
		{
			get { return intervals[intervalClass-1]; }
		}
		
		public int Length
		{
			get { return intervals.Length; }
		}
		
		public IntervalVector(IntervalSet set)
		{
			// 6 digits for 12 equal temperament
			intervals = new int[6];
		}

		
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(intervals.Length+5);
			sb.Append("<");
			for (int i = 0; i < intervals.Length-1; i++)
				sb.Append(intervals[i] + " ");
			sb.Append(intervals[Length-1]+">");
			return sb.ToString();
		}
	}
}
