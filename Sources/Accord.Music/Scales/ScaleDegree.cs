using System;

namespace Accord.Music.Scales
{
	/// <summary>
	/// Description of ScaleDegree.
	/// </summary>
	public enum ScaleDegree
	{
		Tonic,        // First
		Supertonic,   // Second (note above tonic)
		Mediant,	  // Third (half way between tonic and dominant)
		Subdominant,  // Fourth (fifth below tonic)
		Dominant,     // Fifth (fifth above tonic)
		Submediant,   // Sixth (half-way between tonic-subdominant)
		Subtonic,     // Lowered seventh (note below tonic)
		Leading,      // Seventh (leads into tonic)
	}
	
}
