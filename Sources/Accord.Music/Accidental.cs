using System;

namespace Accord.Music
{

    /// <summary>
    ///   Accidental Symbols
    /// </summary>
    /// <remarks>
    ///   In music, an accidental is a note whose pitch (or pitch class) is
    ///   not a member of a scale or mode indicated by the most recently
    ///   applied key signature. In musical notation, the symbols used to
    ///   mark such notes, sharps (#), flats (b), and naturals may also be
    ///   called accidentals.
    /// </remarks>
    public enum Accidental
    {
        DoubleFlat = -2,
        Flat = -1,
        Natural = 0,
        Sharp = 1,
        DoubleSharp = 2,
    }

    /// <summary>
    ///   Extension methods for Accidentals from the Accidental Enumeration.
    /// </summary>
    public static class AccidentalEx
    {
        public static Accidental Parse(String s)
        {
            if (s.Length == 0)
                return Accidental.Natural;
            switch (s)
            {
                case "#":
                case "\u266F":
                    return Accidental.Sharp;
                case "b":
                case "\u266D":
                    return Accidental.Flat;
                case "##":
                case "\uD12A":
                    return Accidental.DoubleSharp;
                case "bb":
                case "\uD12B":
                    return Accidental.DoubleFlat;
                case "n":
                case "\u266E":
                    return Accidental.Natural;
                default:
                    throw new ArgumentException();
            }
        }

        public static String ToString(this Accidental accidental)
        {
            return ToString(accidental, false);
        }

        public static String ToString(this Accidental accidental, bool unicode)
        {
            switch (accidental)
            {
                case Accidental.Sharp:
                    return unicode ? "#" : "\u266F";
                case Accidental.Flat:
                    return unicode ? "b" : "\u266D";
                case Accidental.DoubleSharp:
                    return unicode ? "##" : "\uD12A";
                case Accidental.DoubleFlat:
                    return unicode ? "bb" : "\uD12B";
                case Accidental.Natural:
                    return unicode ? "n" : "\u266E";
                default:
                    return String.Empty;
            }
        }
    }

}
