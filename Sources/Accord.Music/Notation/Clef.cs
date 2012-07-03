using System;

namespace Accord.Music
{
	/// <summary>
	///   A clef is a musical symbol used to indicate the pitch of written notes.
	/// </summary>
	/// <remarks>
	///   Placed on one of the lines at the beginning of the staff, it indicates
	///   the name and pitch of the notes on that line. This line serves as a
	///   reference point by which the names of the notes on any other line or
	///   space of the staff may be determined.
	/// </remarks>
	public struct Clef
	{
		private String name;
		private Pitch pitch;
		private int line;
		// private Image image
		
		/// <summary>
		///   Gets the name for the Clef.
		/// </summary>
		public String Name {
			get { return name; }
		}
		
		/// <summary>
		///   Gets the pitch of the reference line.
		/// </summary>
		public Pitch Pitch {
			get { return pitch; }
		}
		
		/// <summary>
		///   Gets the reference line for the pitch associated with this Clef.
		/// </summary>
		public int Line {
			get { return line; }
		}
		
		/// <summary>
		///   Constructs a new Clef.
		/// </summary>
		/// <param name="name">The name of the Clef.</param>
		/// <param name="pitch">The pitch associated with the Clef.</param>
		/// <param name="line">The reference line in where the associated pitch is located.</param>
		public Clef(String name, Pitch pitch, int line)
		{
			this.name = name;
			this.pitch = pitch;
			this.line = line;
		}
		
		/// <summary>
		///   Constructs a new Clef.
		/// </summary>
		/// <param name="name">The name of the Clef.</param>
		/// <param name="pitch">The pitch associated with the Clef.</param>
		/// <param name="line">The reference line in where the associated pitch is located.</param>
		public Clef(String name, string pitch, int line) :
			this(name, new Pitch(pitch), line)
		{
		}
		
		
		/// <summary>
		///   The Treble G-Clef.
		/// </summary>
		public static readonly Clef Treble = new Clef("Treble", "G4", 2);
		
		/// <summary>
		///   The Bass F-Clef.
		/// </summary>
		public static readonly Clef Bass = new Clef("Bass","F3",4);
		
		/// <summary>
		///   The Alto C-Clef.
		/// </summary>
		public static readonly Clef Alto = new Clef("Alto","C4",3);
		
		/// <summary>
		///   The Tenor C-Clef.
		/// </summary>
		public static readonly Clef Tenor = new Clef("Tenor","C4",4);
		
		/// <summary>
		///   The Neutral (percussion) Clef.
		/// </summary>
		public static readonly Clef Neutral= new Clef("Neutral",null,0);

	}
}
