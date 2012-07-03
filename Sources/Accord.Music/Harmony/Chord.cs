using Accord.Music.Scales;


namespace Accord.Music
{
    using System;
    using Accord.Music.Sets;

    /// <summary>
    ///  Musical Chord.
    /// </summary>
    /// <remarks>
    ///   References:
    ///    - http://www.smithfowler.org/music/Chord_Formulas.htm
    /// </remarks>
    public class Chord
    {
        private IntervalSet components;
       // private ScaleDegree rootDegree;

        public Pitch Root { get; set; }
        public Pitch Bass { get; set; }
        public NoteSet Notes { get; set; }
        public IntervalSet Intervals
        {
            get { return components; }
            set { components = value; }
        }
        public int PitchClassCount { get; set; }

        public bool Inverted
        {
            get { return Bass != Root; }
        }

        //	public enum Quality { major, minor, augmented, diminished, dominant, major-seventh, minor-seventh, diminished-seventh, augmented-seventh, half-diminished, major-minor, major-sixth, minor-sixth, dominant-ninth, major-ninth, minor-ninth, dominant-11th, major-11th, minor-11th, dominant-13th, major-13th, minor-13th, suspended-second, suspended-fourth, other, none};

        public Chord(Note root, params Interval[] intervals)
        {

        }


        public void Play(Duration duration)
        {
            throw new NotImplementedException();
        }
    }
}
