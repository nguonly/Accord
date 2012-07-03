using System;
using System.Linq;

namespace Accord.Music
{

    /// <summary>
    ///   Note names for the (seven-note) diatonic major scale.
    /// </summary>
    /// <remarks>
    ///   The enum values are the distance between the note and C in
    ///   semitones. The  Diatonic Major Scale is do-re-mi-fa-so-la-si-(do).
    /// </remarks>
    public enum NoteName { C = 0, D = 2, E = 4, F = 5, G = 7, A = 9, B = 11 };


    /// <summary>
    ///   Musical Note
    /// </summary>
    public struct Note : IComparable<Note>
    {
        private NoteName name;
        private Accidental accidental;


        #region Constructor
        public Note(NoteName name, Accidental accidental)
        {
            this.name = name;
            this.accidental = accidental;
        }

        public Note(NoteName name)
            : this(name, Accidental.Natural)
        {

        }

        public Note(string name)
        {
            this.name = NoteNameEx.Parse(name.Substring(0, 1));
            this.accidental = AccidentalEx.Parse(name.Substring(1));
        }

        /// <summary>
        ///   Constructs a new note.
        /// </summary>
        /// <param name="integer">The integer notation value for the note.</param>
        public Note(int integer)
        {
            int nearest = integer;
            var notes = (int[])Enum.GetValues(typeof(NoteName));
            int i = notes.Length - 1;
            while (nearest >= integer) nearest -= notes[i--];

            this.name = (NoteName)nearest;
            this.accidental = (Accidental)(integer - nearest);
        }
        #endregion


        #region Properties
        public NoteName Name
        {
            get { return name; }
            set { name = value; }
        }

        public Accidental Accidental
        {
            get { return accidental; }
            set { accidental = value; }
        }

        /// <summary>
        ///   Gets the value of the note in integer notation.
        /// </summary>
        public int Class
        {
            get { return (int)Name + (int)Accidental; }
        }
        #endregion


        /// <summary>
        ///   Gets the note's pitch in the default octave.
        /// </summary>
        public Pitch GetPitch()
        {
            return new Pitch(this);
        }

        /// <summary>
        ///   Gets the note's pitch in a given octave.
        /// </summary>
        /// <param name="octave">The pitch octave.</param>
        public Pitch GetPitch(int octave)
        {
            return new Pitch(this, octave);
        }





        public void Resolve()
        {
            Resolve(Accidental.Sharp);
        }

        public Note Resolve(Accidental resolveTo)
        {
            return PitchClass.Classes[this.Class].First(n => n.Accidental == resolveTo);
        }


        public Note Sharpen()
        {
            // Increments a note (adds a sharp)
            if (this.Accidental != Accidental.DoubleSharp)
                this.Accidental++;
            return this;
        }

        public Note Flatten()
        {
            // Decrements a note (adds a flat)
            if (this.Accidental != Accidental.DoubleSharp)
                this.Accidental++;
            return this;
        }


        #region Operators
        public static Note operator ++(Note note)
        {
            return note + 1;
        }

        public static Note operator --(Note note)
        {
            return note - 1;
        }

        public static Note operator +(Note note, int semitones)
        {
            return new Note(note.Class + semitones);
        }

        public static Note operator -(Note note, int semitones)
        {
            return note + (-semitones);
        }

        public static Note operator +(Note note, IntervalName i)
        {
            return new Note(note.Class + i.Semitones());
        }

        public static Note operator -(Note note, IntervalName i)
        {
            return note + (-i.Semitones());
        }

        public static bool operator ==(Note a, Note b)
        {
            return a.Name == b.Name && a.Accidental == b.Accidental;
        }

        public static bool operator !=(Note a, Note b)
        {
            return !(a == b);
        }
        #endregion


        public override int GetHashCode()
        {
            return (int)Name ^ (int)Accidental;
        }

        /// <summary>
        ///   Compares if the note has the same letter-name,
        ///   and accidental, disregarding any enharmonic
        ///   equivalence information, as another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Note)
                return this == (Note)obj;
            else return false;
        }

        /// <summary>
        ///   Compares if the note has the same letter-name,
        ///   and accidental, disregarding any enharmonic
        ///   equivalence information, as another note.
        /// </summary>
        /// <param name="note">The note to be compared.</param>
        /// <returns>Returns true if the two are equal, false otherwise.</returns>
        public bool Equals(Note note)
        {
            return this == note;
        }

        /// <summary>
        ///   Compares if the notes are enharmonic equivalents,
        ///   having the same pitch despite having different names.
        /// </summary>
        /// <param name="note">The note to be compared.</param>
        /// <returns>Returns true if the two are equivalent, false otherwise.</returns>
        public bool EnharmonicEquals(Note note)
        {
            return this.Class == note.Class;
        }




        public int CompareTo(Note other)
        {
            if (this.name > other.name)
                return this.name - other.name;
            if (this.name < other.name)
                return other.name - this.name;
            else
            {
                if (this.accidental > other.accidental)
                    return 1;
                if (this.accidental < other.accidental)
                    return -1;
                else return 0;
            }
        }

        public static Note Parse(String s)
        {
            NoteName nName = NoteNameEx.Parse(s.Substring(0, 1));
            Accidental nAccidental = AccidentalEx.Parse(s.Substring(1));
            return new Note(nName, nAccidental);
        }


        public override string ToString()
        {
            return NoteNameEx.ToString(name) + AccidentalEx.ToString(accidental);
        }
    }



    public static class NoteNameEx
    {

        public static int Distance(this NoteName a, NoteName b)
        {
            return Accord.Math.Distance.Modular(a.Index(), b.Index(), 12);
        }

        public static int Index(this NoteName name)
        {
            switch (name)
            {
                case NoteName.C: return 0;
                case NoteName.D: return 1;
                case NoteName.E: return 2;
                case NoteName.F: return 3;
                case NoteName.G: return 4;
                case NoteName.A: return 5;
                case NoteName.B: return 6;
                default:
                    throw new ArgumentException();
            }
        }

        public static String ToString(this NoteName note)
        {
            return Enum.GetName(typeof(NoteName), note);
        }

        public static NoteName Parse(String s)
        {
            try
            {
                return (NoteName)Enum.Parse(typeof(NoteName), s.ToUpper());
            }
            catch
            {
                switch (s.ToLower())
                {
                    case "do":
                        return NoteName.C;
                    case "re":
                        return NoteName.D;
                    case "mi":
                        return NoteName.E;
                    case "fa":
                        return NoteName.F;
                    case "sol":
                        return NoteName.G;
                    case "la":
                        return NoteName.A;
                    case "si":
                        return NoteName.B;
                    default:
                        throw new FormatException();
                }
            }
        }
    }

}