using System;
using System.Collections.Generic;

namespace Accord.Music.Sets
{
    /// <summary>
    ///   Interval Set
    /// </summary>
    public class IntervalSet : HashSet<Interval>, IMusicalSet<IntervalSet>
    {

        public List<Note> Steps { get; set; }
        private Note root;


        public Note Root
        {
            get { return root; }
            set { root = value; }
        }

        public IntervalSet(Note root)
        {
        }

        public IntervalSet(params Interval[] intervals)
        {
            foreach (Interval interval in intervals)
            {
                this.Add(interval);
            }
        }

        public IntervalSet(Note root, params Note[] notes)
        {

        }

        public IntervalSet(Note root, String intervals)
        {

        }

        public IntervalSet(params int[] steps)
        {
        }

        public IntervalSet(string intervals)
        {
            string[] arr = intervals.Split('-');
            foreach (string interval in arr)
            {
                this.Add(new Interval(interval));
            }
        }

        public override string ToString()
        {
            return root + Steps.ToString();
        }

        public IntervalSet Transpose(int i)
        {
            throw new NotImplementedException();
        }

        public IntervalSet Inversion()
        {
            throw new NotImplementedException();
        }

        public IntervalSet Retrogade()
        {
            throw new NotImplementedException();
        }
    }
}
