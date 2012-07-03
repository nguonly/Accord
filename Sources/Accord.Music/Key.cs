using System;

namespace Accord.Music
{
    /// <summary>
    ///   Musical Key
    /// </summary>
    public struct Key : IComparable<Key>, IEquatable<Key>
    {
        private Note note;
        private bool minor;


        public StandardKeySignature Signature
        {
            get { return new StandardKeySignature(this); }
        }

        public Note Note
        {
            get
            { return this.note; }
            set { this.note = value; }
        }

        public bool Minor
        {
            get
            { return this.minor; }
            set { this.minor = value; }
        }



        public Key(Note note)
        {
            this.note = note;
            this.minor = false;
        }

        public Key(Note note, bool minor)
        {
            this.note = note;
            this.minor = minor;
        }


        public Key[] GetEnharmonics()
        {
            throw new NotImplementedException();
        }



        #region Operators
        public static Key operator +(Key k, int semitones)
        {
            return new Key(k.Note + semitones);
        }

        public static Key operator -(Key k, int semitones)
        {
            return new Key(k.Note - semitones);
        }

        public static Key operator +(Key k, IntervalName i)
        {
            return k + i.Semitones();
        }

        public static Key operator -(Key k, IntervalName i)
        {
            return k - i.Semitones();
        }


        public int CompareTo(Key other)
        {
            if (this.minor && !other.minor)
                return -1;

            return this.Note.CompareTo(other.Note);
        }

        public bool Equals(Key other)
        {
            return this.minor == other.minor &&
                this.Note == other.Note;
        }

        public static bool operator ==(Key a, Key b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Key a, Key b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return minor.GetHashCode() ^ note.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
