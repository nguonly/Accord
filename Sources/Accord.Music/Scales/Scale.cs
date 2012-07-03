using System;
using Accord.Music.Sets;
using System.Collections.Generic;

namespace Accord.Music.Scales
{
    /// <summary>
    ///  Scale is a group of musical notes ordered by pitch.
    ///</summary>
    /// <remarks>
    ///	 In music, a scale is a group of musical notes collected in
    ///	 ascending and descending order, that provides material for
    ///	 or is used to conveniently represent part or all of a musical
    ///	 work including melody and/or harmony. Scales are ordered in
    ///  pitch or pitch class, with their ordering providing a measure
    ///	 of musical distance.
    ///	</remarks>
    ///  Major scales: http://musiced.about.com/od/lessonsandtips/qt/scales.htm
    /// a scale is a pitch class *collection*.
    public class Scale : NoteSet
    {

        public IntervalSet Intervals { get; set; }
        // Storing intervals allows for direct access to any of the scale degrees
        // Storing steps would require additional computing steps to perform the same.


        /// <summary>
        ///   Gets or sets the tonic note for the scale.
        /// </summary>
        /// <remarks>
        ///   The tonic is the central and most stable note of the scale
        /// </remarks>
        public Note Tonic { get; set; }

        //In diatonic set theory, the deep scale property is the quality of pitch class collections or scales containing each interval class a unique number of times.
        public bool DeepScale
        {
            get { throw new NotImplementedException(); }
        }


        public Scale(Note root, IntervalSet intervals)
        {

        }

        public Scale(Note root, params int[] halfSteps)
        {

        }

        public Scale(Note root, NoteSet notes)
        {

        }

        public Scale(Note root, String intervals)
        {

        }




        #region Common Scales
        public static Scale Major(Note root)
        {
            return new Scale(root, "T-T-S-T-T-T-S");
        }

        public static Scale Minor(Note root)
        {
            return new Scale(root, "2-1-2-2-1-2-2");
        }

        public static Scale Chromatic(Note root)
        {
            return new Scale(root, "S-S-S-S-S-S-S-S-S-S-S-S");
        }

        public static Scale WholeTone(Note root)
        {
            return new Scale(root, "2-2-2-2-2-2");
        }
        #endregion
    }
}
