using System;
using System.Linq;
using Accord.Math;

namespace Accord.Music
{
    /// <summary>
    ///   A pitch class is a set of all pitches that are a whole
    ///   number of octaves apart, e.g., the pitch class C consists
    ///   of the Cs in all octaves.
    /// </summary>
    /// <remarks>
    ///   In standard Western equal temperament, distinct spellings can
    ///   refer to the same sounding object: B#3, C4, and D4 all refer to
    ///   the same pitch, hence share the same chroma, and therefore belong
    ///   to the same pitch class; a phenomenon called enharmonic equivalence.
    /// </remarks>
    public struct PitchClass
    {

        private int label;


        /// <summary>
        ///   Constructs a new pitch class.
        /// </summary>
        /// <param name="note">The base note for the class.</param>
        public PitchClass(Note note)
        {
            this.label = note.Class;
        }

        /// <summary>
        ///   Constructs a new pitch class
        /// </summary>
        /// <param name="label">The class label.</param>
        public PitchClass(int label)
        {
            this.label = label;
        }


        /// <summary>
        ///   Gets the integer label for the pitch class.
        /// </summary>
        public int Label
        {
            get { return label; }
        }

        /// <summary>
        ///   The base note for the pitch class.
        /// </summary>
        public Note[] Enharmonics
        {
            get { return Classes[label]; }
        }

        public bool Contains(Pitch pitch)
        {
            return Contains(pitch.Note);
        }

        public bool Contains(Note note)
        {
            return note.Class == label;
        }


        public static int Interval(PitchClass a, PitchClass b)
        {
            return Distance.Modular(a.Label, b.Label, 12);
        }

        #region Operators
        public static int operator -(PitchClass a, PitchClass b)
        {
            return System.Math.Min(Tools.Mod(a.Label - b.Label, 12),
                                   Tools.Mod(b.Label - a.Label, 12));
        }

        public static PitchClass operator +(PitchClass a, int b)
        {
            return new PitchClass(a.Label + b);
        }

        public static PitchClass operator -(PitchClass a, int b)
        {
            return a + (-b);
        }

        public static PitchClass operator -(PitchClass a)
        {
            return new PitchClass(Tools.Mod(-a.Label, 12));
        }

        public static PitchClass operator --(PitchClass a)
        {
            return a - 1;
        }

        public static PitchClass operator ++(PitchClass a)
        {
            return a + 1;
        }

        public static bool operator ==(PitchClass a, PitchClass b)
        {
            return (a.Label == b.Label);
        }

        public static bool operator !=(PitchClass a, PitchClass b)
        {
            return !(a == b);
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj is PitchClass)
                return this == (PitchClass)obj;
            return false;
        }

        public override int GetHashCode()
        {
            return label.GetHashCode();
        }


        public override string ToString()
        {
            var r = from n in Enharmonics
                    where n.Accidental == Accidental.Flat
                      || n.Accidental == Accidental.Sharp
                    select n;

            Note[] notes = (Note[])r;

            String str = notes[0].ToString();
            if (notes.Length > 1)
                str += "/" + notes[1];

            return str;
        }

        public static readonly Note[][] Classes =
		{
			new Note[] { new Note("C"),  new Note("B#"),  new Note("Dbb") },
			new Note[] { new Note("C#"), new Note("Db"),  new Note("B##") },
			new Note[] { new Note("D"),  new Note("C##"), new Note("Ebb") },
			new Note[] { new Note("D#"), new Note("Eb"),  new Note("Fbb") },
			new Note[] { new Note("E"),  new Note("D##"), new Note("Fb")} ,
			new Note[] { new Note("F"),  new Note("E#"),  new Note("Gbb") },
			new Note[] { new Note("F#"), new Note("Gb"),  new Note("E##") },
			new Note[] { new Note("G"),  new Note("F##"), new Note("Abb") },
			new Note[] { new Note("G#"), new Note("Ab")},
			new Note[] { new Note("A"),  new Note("G##"), new Note("Bbb") },
			new Note[] { new Note("A#"), new Note("Bb"),  new Note("Cbb") },
			new Note[] { new Note("B"),  new Note("A##"), new Note("Cb")  },
		};

    }
}