using System;
using System.Text;
using Accord.Math;

namespace Accord.Music
{
	
	/// <summary>
	///   Simple diatonic interval names.
	/// </summary>
	/// <remarks>
	///   The interval name is given by the difference between
	///   two noter names, without considering the distance in
	///   semitones.
	/// </remarks>
	public enum IntervalName
	{
		Unison = 0,
		Second = 1,
		Third = 2,
		Fourth = 3,
		Fifth = 4,
		Sixth = 5,
		Seventh = 6,
		Octave = 7,
	}
	
	/// <summary>
	///   Interval qualifiers.
	/// </summary>
	public enum IntervalQuality
	{
		DoubleAugmented = 3,
		Augmented = 2,
		Major = 1,
		Perfect = 0,
		Minor = -1,
		Diminished = -2,
		DoubleDiminished = -3,
	}
	
	/// <summary>
	///   Description of Interval.
	/// </summary>
	/// <remarks>
	///   http://www.knowledgerush.com/kr/encyclopedia/Interval_(music)/
	/// </remarks>
	public struct Interval : IComparable<Interval>, IEquatable<Interval>
	{
		private Pitch lower;
		private Pitch upper;

		
		/// <summary>
		///   Gets the simple diatonic interval name.
		/// </summary>
		public IntervalName Name
		{
			get { return (IntervalName)lower.Note.Name.Distance(upper.Note.Name); }
		}
		
		/// <summary>
		///   Gets the interval quality.
		/// </summary>
		public IntervalQuality Quality
		{
			get { 
				int noteDistance = lower.Note.Name.Distance(upper.Note.Name);;
				int quality = noteDistance - (Semitones+1);
				return (IntervalQuality)quality;
			}
		}
				
		public int Octaves
		{
			get { return Semitones/12; }
		}
		public bool Compound
		{
			get { return Semitones > 12; }
		}

		public int Semitones
		{
			get { return upper.Number - lower.Number; }
		}
		
		public int Tones
		{
			get { return Semitones/2;}
		}
		
		public IntervalClass Class
		{
			get
			{
				return new IntervalClass(Distance.Modular(upper,lower,12));
			}
		}
		
		public Interval(Pitch a, Pitch b)
		{
			upper = new Pitch(System.Math.Max(a.Number, b.Number));
			lower = new Pitch(System.Math.Min(a.Number, b.Number));
		}
		
		public Interval(Pitch p, int semitones)
		{
			this.lower = p;
			this.upper = lower + semitones;
		}
		
		public Interval(int semitones)
		{
			this.lower = Pitch.MiddleC;
			this.upper = lower + semitones;
		}
		
		/// <summary>
		///   Intervals are often abbreviated with a P for perfect,
		///   m for minor, M for major, d for diminished, A for
		///   augmented, followed by the diatonic interval number.
		///   The indication M and P are often omitted. The octave
		///   is P8, and a unison is usually referred to simply as
		///   "a unison" but can be labeled P1. The tritone, an
		///   augmented fourth or diminished fifth is often π or TT.
		/// </summary>
		/// <remarks>
		///   Examples: m1, m2, M3, TT, P8, A4, T, S, H, W, 1, 2, 3
		/// </remarks>
		/// <param name="name"></param>
		public Interval(String name)
		{
			this.lower = Pitch.MiddleC;
			this.upper = Pitch.MiddleC;
			
			switch (name.ToUpper())
			{
				case "S":
				case "H":
					this.upper = lower + 1; break;
				case "T":
				case "W":
					this.upper = lower + 2; break;
				case "A":
					this.upper = lower + 3; break;
				default:
					throw new ArgumentException();
			}
			
		}
		
		public override string ToString()
		{
			return Enum.GetName(typeof(IntervalQuality),this.Quality) +
				Enum.GetName(typeof(IntervalName),this.Name);
		}
		
		
		/*
		 * An interval may be inverted, by raising the lower pitch an octave,
		 * or lowering the upper pitch an octave (though it is less usual to
		 * speak of inverting unisons or octaves). For example, the fourth
		 * between a lower C and a higher F may be inverted to make a fifth,
		 * with a lower F and a higher C. Here are the ways to identify interval
		 * inversions:
		 */
		public Interval Inverse()
		{
			return new Interval(lower+8,upper);
		}
		
		public static Interval operator+(Interval a, int i)
		{
			return new Interval(a.lower, a.upper	+i);
		}
		
		public static Interval operator-(Interval a, int i)
		{
			return a + (-i);
		}
		
		public static implicit operator int(Interval a)
		{
			return a.Semitones;
		}
		
		public bool EnharmonicEqual(Interval a)
		{
			return this.Semitones == a.Semitones;
		}
		
		public int CompareTo(Interval other)
		{
			throw new NotImplementedException();
		}
		
		public bool Equals(Interval other)
		{
			throw new NotImplementedException();
		}
		
		public void Play(Duration duration)
		{
			throw new NotImplementedException();
		}
	}

    public static class IntervalNameEx
    {
        public static int Semitones(this IntervalName i)
        {
            switch (i)
            {
                case IntervalName.Unison: return 0;
                case IntervalName.Second: return 2;
                case IntervalName.Third: return 4;
                case IntervalName.Fourth: return 5;
                case IntervalName.Fifth: return 7;
                case IntervalName.Sixth: return 9;
                case IntervalName.Seventh: return 11;
                case IntervalName.Octave: return 12;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
