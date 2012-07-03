using System;
using System.Collections.Generic;

namespace Accord.Music.Notation
{
	/// <summary>
	///   Description of Staff.
	/// </summary>
	public class Staff : List<Measure>
	{
        private int bpm;
		public String Voice { get; set; }

        public int Bpm
        {
            get { return bpm; }
            set { bpm = value; }
        }
			
		public Staff()
		{
			this.Add(new Measure(this));
		}

        public void Play()
        {
            foreach (Measure measure in this)
            {
                measure.Play();
            }
        }
	}
	
}
