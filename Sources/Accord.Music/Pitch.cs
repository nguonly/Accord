using System;
using System.Linq;


namespace Accord.Music
{

	
	/// <summary>
	///   Represents a pitch in Scientific Pitch Notation.
	/// </summary>
	/// <remarks>
	///   Scientific pitch notation is one of several methods that name
	///   the notes of the standard Western chromatic scale by combining
	///   a letter-name, accidentals, and a number identifying the pitch's
	///   octave.
	/// </remarks>
	public struct Pitch : IComparable<Pitch>
	{
		// A pitch is a note plus an octave to give spatial reference.
		private int octave;
		private Note note;


        #region Constructors
        /// <summary>
        ///   Constructs a new pitch.
        /// </summary>
        /// <param name="name">The letter-name for the pitch.</param>
        /// <param name="accidental">The accidental for the pitch.</param>
        /// <param name="octave">The octave for the pitch.</param>
        public Pitch(NoteName name, Accidental accidental, int octave)
        {
            this.note = new Note(name, accidental);
            this.octave = octave;
        }

        /// <summary>
        ///   Constructs a new pitch. Default accidental is none.
        /// </summary>
        /// <param name="name">The letter-name for the pitch.</param>
        /// <param name="octave">The octave for the pitch.</param>
        public Pitch(NoteName name, int octave) :
            this(name, Accidental.Natural, octave)
        {
        }

        public Pitch(Note note, int octave)
        {
            this.note = note;
            this.octave = octave;
        }

        public Pitch(Note note)
        {
            this.note = note;
            this.octave = MiddleC.Octave;
        }

        /// <summary>
        ///   Constructs a new pitch.
        /// </summary>
        /// <param name="pitchClass">The pitch class for the pitch.</param>
        /// <param name="octave">The octave for the pitch.</param>
        public Pitch(PitchClass pitchClass, int octave)
        {
            this.note = pitchClass.Enharmonics[0];
            this.octave = octave;
        }

        /// <summary>
        ///   Constructs a new pitch.
        /// </summary>
        /// <param name="noteNumber">
        ///   The number of semitones starting from C(-1) that identifies
        /// the pitch in the MIDI system.
        /// </param>
        public Pitch(int noteNumber)
            : this()
        {
            Number = noteNumber;
        }

        /// <summary>
        ///   Constructs a new pitch.
        /// </summary>
        /// <param name="frequency">The frequency for the pitch.</param>
        public Pitch(double frequency)
            : this()
        {
            Frequency = frequency;
        }

        /// <summary>
        ///   Constructs a new pitch.
        /// </summary>
        /// <param name="name">
        ///   The text symbol for the pitch.
        ///   i.e.: C#1, Db3, G5, A##97, Ab-73
        /// </param>
        public Pitch(string name)
            : this()
        {
            int i = name.Length - 1;
            while (char.IsNumber(name[i]) ||
                   name[i] == '+' || name[i] == '-') i--;

            this.note = new Note(name.Substring(0, i));
            this.octave = int.Parse(name.Substring(i));
        }
        #endregion

		
		#region Properties
		/// <summary>
		///   Gets or sets the octave for the pitch.
		/// </summary>
		public int Octave
		{
			get { return octave; }
			set { octave = value; }
		}
		
        /*
		/// <summary>
		///   Gets or sets the note name for the pitch.
		/// </summary>
		public NoteName Name {
			get { return note.Name; }
			set { note.Name = value;}
		}
         */ 
		
		/// <summary>
		///   Gets the note for this pitch.
		/// </summary>
		public Note Note
		{
			get { return note; }
		}

		/*
		/// <summary>
		///   Gets or sets the accidental for the pitch.
		/// </summary>
		public Accidental Accidental
		{
			get { return note.Accidental; }
			set { note.Accidental = value; }
		}*/
		
		/// <summary>
		///   Gets the pitch's class.
		/// </summary>
		public PitchClass Class
		{
			get { return new PitchClass(note); }
		}
		
		/// <summary>
		///   Gets or sets a pitch frequency. Octave, letter-name,
		///   and accidentals will change automatically to reflect
		///   the new frequency.
		/// </summary>
		/// <remarks>
		///   The basic formula for the frequencies of the
		///   notes of the equal tempered scale is given by
		///
		///     fn = f0 * (a)^n			where:
		/// 
		///	    f0 = the frequency of one fixed note which must be
		///          defined. A common choice is setting the A above
		///          middle C (A4) at f0 = 440 Hz.
		///      n = the number of half steps away from the fixed note
		///          you are. If you are at a higher note, n is positive.
		///          If you are on a lower note, n is negative.
		///     fn = the frequency of the note n half steps away.
		///      a = (2)^1/12 = the twelth root of 2 = the number which
		///                 when multiplied by itself 12 times equals 2 = 1.059463094359...
		/// </remarks>
		public double Frequency
		{
			get
			{
				double n = Number - 9;
				return 439.96 * System.Math.Pow(2,n/12);
			}
			set
			{
				Number = (int)(69 + 12*System.Math.Log((value / 439.96), 2));
			}
		}
		
		/// <summary>
		///   The negative or positive distance, in semitones,
		///   from middle C (C4).
		/// </summary>
		public int Distance
		{
			get
			{
				return Number - MiddleC.Number;
			}
		}
		
		/// <summary>
		///   The MIDI number for the pitch.
		/// </summary>
		/// <remarks>
		///   The MIDI number is the distance from C(-1)
		///   and identifies pitches without considering
		///   any information about enharmonics equivalents.
		/// </remarks>
		public int Number
		{
			get
			{
				return (octave+1) * 12 + note.Class;
			}
			set
			{
				int classNumber = System.Math.DivRem(value, 12, out octave);
				note = new Note(classNumber);
			}
		}
		#endregion
		
		
		
		
		public override string ToString()
		{
			return note.ToString()+octave.ToString();
		}
		
		public void Play(int duration)
		{
			Console.Beep((int)Frequency, duration);
		}
		
		
		
		public readonly static Pitch MiddleC = new Pitch(NoteName.C,4);



        #region Operators
        public static Pitch operator+(Pitch pitch, int a)
		{
			return new Pitch(pitch.Number + a);
		}
				
		public static Pitch operator-(Pitch pitch, int a)
		{
			return new Pitch(pitch.Number - a);
		}
		
		public static Interval operator-(Pitch a, Pitch b)
		{
			return new Interval(a, b);
		}
		
		public static Pitch operator++(Pitch pitch)
		{
			return pitch + 1;
		}
		
		public static Pitch operator--(Pitch pitch)
		{
			return pitch - 1;
		}
		
		public static bool operator==(Pitch a, Pitch b)
		{
			return a.Equals(b);
		}

		public static bool operator!=(Pitch a, Pitch b)
		{
			return !(a == b);
		}
		
		public static bool operator>(Pitch a, Pitch b)
		{
			return a.Number > b.Number;
		}
		
		public static bool operator<(Pitch a, Pitch b)
		{
			return b.Number < a.Number;
		}
		
		public static implicit operator int(Pitch a)
		{
			return a.Number;
		}
		
		public static implicit operator double(Pitch a)
		{
			return a.Frequency;
		}
		
		public override int GetHashCode()
		{
			return note.GetHashCode() ^ octave;
		}
		
		/// <summary>
		///   Compares if the pitch has the same letter-name,
		///   accidental and octave, disregarding enharmonic
		///   equivalence information, as another object.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is Pitch)
				return this == (Pitch)obj;
			return false;
		}
		
		/// <summary>
		///   Compares if the pitch is enharmonically equivalent
		///   to another pitch.
		/// </summary>
		/// <param name="pitch">The pitch to be compared.</param>
		/// <returns>Returns true if the two are equivalent, false otherwise.</returns>
		public bool EnharmonicEquals(Pitch pitch)
		{
			// enharmonic notes share the same (midi) pitch number.
			return note.EnharmonicEquals(pitch.note);
		}
		
		/// <summary>
		///   Compares if the pitch has the same letter-name,
		///   accidental and octave, disregarding enharmonic
		///   equivalence information, as another object.
		/// </summary>
		/// <param name="pitch">The pitch to be compared.</param>
		/// <returns>Returns true if the two are equivalent, false otherwise.</returns>
		public bool Equals(Pitch pitch)
		{
			return (this.note == pitch.note) &&
				(this.octave == pitch.octave);
		}
		
		/// <summary>
		///   Compares if the pitch is enharmonically equivalent
		///   to another pitch.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int EnharmonicCompareTo(Pitch other)
		{
			return this.Number.CompareTo(other.Number);
		}
		
		public int CompareTo(Pitch other)
		{
			return this.note.CompareTo(other.note);
        }
        #endregion


        public static Pitch Parse(String s)
		{
			return new Pitch(s);
		}
	}
}
