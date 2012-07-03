using System;
using System.Collections.Generic;

using Accord.Music.Notation;

namespace Accord.Music.Melody
{
		// //lead sheet note/beat locus
	public class Position
	{
		

		public HashSet<NoteSign> Notes;
		public Chord Chord;
		public String Syllabes;

        public Position()
        {
            Notes = new HashSet<NoteSign>();
        }

        public Position(NoteSign note) : this()
        {
            Notes.Add(note);
        }

        public void Add(NoteSign note)
        {
            Notes.Add(note);
        }

        public void Play(int bpm)
        {
            foreach (NoteSign note in Notes)
            {
                note.Play(bpm);
            }
        }
	}
}
