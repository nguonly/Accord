using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Accord.Music
{
    /// <summary>
    ///   A series of sharp or flat symbols placed on the staff.
    /// </summary>
    /// <remarks>
    ///   A key signature is a series of sharp or flat symbols placed on
    ///   the staff, designating notes that are to be consistently played
    ///   one semitone higher or lower than the equivalent natural notes
    ///   unless otherwise altered with an accidental.
    /// </remarks>
    public class KeySignature : KeyedCollection<NoteName, Note>
    {

        /// <summary>
        ///   Gets the accidental designated for a note by this signature.
        /// </summary>
        public virtual new Accidental this[NoteName note]
        {
            get
            {
                if (base.Contains(note))
                    return base[note].Accidental;
                else return Accidental.Natural;
            }
            set
            {
                if (base.Contains(note))
                {
                    int index = base.IndexOf(base[note]);
                    base.Remove(note);
                    base.Insert(index, new Note(note, value));
                }
                else base.Add(new Note(note, value));
            }
        }

        public Note[] GetNotes()
        {
            throw new NotImplementedException();
        }


        protected override NoteName GetKeyForItem(Note item)
        {
            return item.Name;
        }
    }
}
