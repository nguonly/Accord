using System;
using System.Collections.Generic;


namespace Accord.Music.Sets
{
	/// <summary>
	/// Description of PitchSet.
	/// </summary>
	public class PitchSet : HashSet<Pitch>, IMusicalSet<PitchSet>
	{
		public PitchSet()
		{
		}

        public PitchSet Inversion()
        {
            throw new NotImplementedException();
        }

        public PitchSet Retrogade()
        {
            throw new NotImplementedException();
        }
		
		
		public PitchSet Transpose(int semitones)
		{
			PitchSet transposed = new PitchSet();
			foreach (Pitch p in this)
			{
				transposed.Add(p+semitones);
			}
			return transposed;
		}
		
		public static PitchSet operator+(PitchSet a, int semitones)
		{
			return a.Transpose(semitones);
		}
		
		public static PitchSet operator-(PitchSet a, int semitones)
		{
			return a.Transpose(-semitones);
		}

    }
}
