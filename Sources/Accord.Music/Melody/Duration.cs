using System;

namespace Accord.Music
{
	/// <summary>
	///   Note time durations
	/// </summary>
	public enum Duration {
		/// <summary>Longa</summary>
		Longa = -2,
		
		/// <summary>Breve</summary>
		Breve = -1,
		
		/// <summary>Whole note</summary>
		Semibreve = 0,
		
		/// <summary>Half note</summary>
		Minin = 1,
		
		/// <summary>Quarter note</summary>
		Crotchet = 2,
		
		/// <summary>Eighth note</summary>
		Quaver = 3,
		
		/// <summary>Sixteenth note</summary>
		Semiquaver = 4,
		
		/// <summary>Thirty-second note</summary>
		Demisemiquaver = 5,
		
		/// <summary>Sixty-fourth note</summary>
		Hemidemisemiquaver = 6, 
		
		/// <summary>One-hundred-twenty-eighth note</summary>
		Quasihemidemisemiquaver = 7
	}

    public static class DurationEx
    {

        public static String ToString(this Duration duration)
        {
            switch (duration)
            {
                case Duration.Longa:
                    return "\uD1B7";
                case Duration.Breve:
                    return "\uD1B8";
                case Duration.Semibreve:
                    return "\uD15D";
                case Duration.Minin:
                    return "\uD15E";
                case Duration.Crotchet:
                    return "\u2669";
                case Duration.Quaver:
                    return "\u266A";
                case Duration.Semiquaver:
                    return "\uD161";
                case Duration.Demisemiquaver:
                    return "\uD162";
                default:
                    return String.Empty;
            }
        }
    }
}
