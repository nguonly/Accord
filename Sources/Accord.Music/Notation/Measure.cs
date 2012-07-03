using System;
using Accord.Music.Melody;
using System.Collections.Generic;

namespace Accord.Music.Notation
{
	public enum BarTypes
	{
		Standard,
		Double,
		SongEnd,
		RepeatBegin,
		RepeatEnd,
		RepeatEndAndBegin,
	}
	
	/// <summary>
	///   Measure
	/// </summary>
	public class Measure
	{
		public Measure Previous { get; internal set; }
		public Measure Next { get; internal set; }
		public Staff Owner { get; internal set; }
		
		public int Index { get { return Owner.IndexOf(this);}}
		
		public Nullable<int> Bpm;
		public KeySignature KeySignature; // if null, should use previous staff values
		public TimeSignature TimeSignature; // if null, should use previous staff values
		public Clef Clef; // if null use previous

        public List<Position> Locus { get; set; } 

        public bool ShowKeySignature { get; set; }
        public bool ShowTimeSignature { get; set; }
        public bool ShowClef { get; set; }
        public BarTypes Bar { get; set; }
		
		public Measure(Staff staff)
		{
			this.Owner = staff;
            Locus = new List<Position>();
		}

        public bool IsFull
        {
            get { throw new NotImplementedException(); }
        }

        public void Play()
        {
            foreach (Position unit in Locus)
            {
                unit.Play(Owner.Bpm);
            }
        }
		
	}
}
