using System;

namespace Accord.Music.Sets
{
	/// <summary>
	///   Musical Set
	/// </summary>
    /// <remarks>
    ///   Musical set theory deals with collections of pitches and pitch classes, which
    ///   may be ordered or unordered, and which can be related by musical operations
    ///   such as transposition, inversion, and complementation. The methods of musical
    ///   set theory are sometimes applied to the analysis of rhythm as well.
    ///   
    ///   References:
    ///    - http://en.wikipedia.org/wiki/Set_theory_(music)
    /// </remarks>
	public interface IMusicalSet<T> where T : IMusicalSet<T>
	{
		T Transpose(int i);
		T Inversion();
		T Retrogade();
	}
}
