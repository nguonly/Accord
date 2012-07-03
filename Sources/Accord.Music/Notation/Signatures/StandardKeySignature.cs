using System;
using System.Collections.Generic;
using System.Linq;

using Accord.Music.Scales;

namespace Accord.Music
{
    /// <summary>
    ///   Standard Key Signature
    /// </summary>
    /// <remarks>
    ///   Unlike the KeySignature class, this class is associated with
    ///   a particular key.
    /// </remarks>
    public class StandardKeySignature : KeySignature
    {
        private Key key;


        public Key Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    build();
                }
            }
        }


        public StandardKeySignature(Key key)
        {
            this.key = key;
        }


        private void build()
        {
            if (!key.Minor)
            {
                Note c = new Note(NoteName.C);
                while (c != key.Note)
                {
                    this.Add(c - 1);
                    c += IntervalName.Fifth;
                }
            }
            else
            {
                Note a = new Note(NoteName.A);
                while (a != key.Note)
                {
                    this.Add(a - 1);
                    a += IntervalName.Fifth;
                }
            }
        }


        public static StandardKeySignature operator ++(StandardKeySignature a)
        {
            return new StandardKeySignature(a.Key += IntervalName.Fifth); // Adds a fifth
        }

        public static StandardKeySignature operator --(StandardKeySignature a)
        {
            return new StandardKeySignature(a.Key -= IntervalName.Fifth); // Decreases by a fifth
        }


    }
}
