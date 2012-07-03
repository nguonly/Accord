using System;

namespace Accord.Music
{

    /// <summary>
    ///   Musical note notation sign
    /// </summary>
    /// <remarks>
    ///   In standard notation, a single musical sound is written as
    ///   a note. The two most important things a written piece of
    ///   music needs to tell you about a note are its pitch - how
    ///   high or low it is - and its duration - how long it lasts.
    /// </remarks>
    public class NoteSign
    {
        /// <summary>
        ///   Gets or sets the pitch of the note.
        /// </summary>
        /// <remarks>
        ///   The pitch contains information about note, octave and frequency.
        /// </remarks>
        public Pitch Pitch { get; set; }

        /// <summary>
        ///   Gets or sets the duration of the note.
        /// </summary>
        public Duration Value { get; set; }

        /// <summary>
        ///   Gets or sets the current number of duration
        ///   augmentation dots for the note.
        /// </summary>
        public ushort Augmentation { get; set; }

        /// <summary>
        ///   Gets a value indicating if the sign is a rest.
        /// </summary>
        public bool IsRest
        {
            get { return Pitch == null; }
        }

        /// <summary>
        ///   Computes the actual duration for the note
        ///   considering the augmentation effects.
        /// </summary>
        public double Duration
        {
            get
            {
                double a = System.Math.Pow(2, (int)Value);
                return 2 * a - (a / System.Math.Pow(2, Augmentation));
            }
        }


        /// <summary>
        ///   Constructs a new note sign.
        /// </summary>
        /// <param name="note">The note value.</param>
        /// <param name="duration">The note duration.</param>
        public NoteSign(Note note, Duration duration)
        {
            this.Pitch = new Pitch(note);
            this.Value = duration;
        }

        /// <summary>
        ///   Constructs a new note sign.
        /// </summary>
        /// <param name="note">The note value.</param>
        /// <param name="duration">The note duration.</param>
        public NoteSign(string note, Duration duration)
        {
            this.Pitch = new Pitch(new Note(note));
            this.Value = duration;
        }

        /// <summary>
        ///   Constructs a new rest sign.
        /// </summary>
        /// <param name="duration">The sign duration.</param>
        public NoteSign(Duration duration)
        {
            this.Value = duration;
        }


        /// <summary>
        ///   Named constructor for a new rest sign.
        /// </summary>
        /// <param name="value">The sign duration.</param>
        public static NoteSign Rest(Duration value)
        {
            return new NoteSign(value);
        }



        public override string ToString()
        {
            return base.ToString();
        }

        public void Play(int bpm)
        {
            double mpb = 1.0 / bpm;
            int milliseconds = (int)(mpb * 60 * 1000 * this.Duration);

            if (!IsRest)
                this.Pitch.Play(milliseconds);

            else System.Threading.Thread.Sleep(milliseconds);
        }
    }


}
