using System;

namespace Accord.Music.Sets
{
	/// <summary>
	/// Description of PitchClassSet.
	/// </summary>
	public class PitchClassSet : IMusicalSet<PitchClassSet>
	{
		public PitchClassSet()
		{
		}
		
		public IntervalVector IntervalVector
		{
			get {
			throw new NotImplementedException();
			}
		}
		
		public void Complement()
		{
			
		}
		
		public int Prime
		{
			get { 
				throw new NotImplementedException();
			}
		}
		
		// Interval String Notation (ISN)
		// http://solomonsmusic.net/setheory.htm
		public String ISN;
		
		//http://en.wikipedia.org/wiki/Multiplication_(music)
		//When dealing with pitch class sets, multiplication modulo 12 is a common operation.
		
		public static PitchClassSet operator*(PitchClassSet a, int b)
		{
			throw new NotImplementedException();
		}
		
		//pitch multiplication, which is somewhat akin to the Cartesian product of pitch class sets.
		public static PitchClassSet operator*(PitchClassSet a, PitchClassSet b)
		{
			throw new NotImplementedException();
		}
		
		public PitchClassSet Transpose(int a)
		{
			throw new NotImplementedException();
		}
		
		public PitchClassSet Inversion()
		{
			throw new NotImplementedException();
		}
		
		public PitchClassSet Retrogade()
		{
			throw new NotImplementedException();
		}
	}
}
