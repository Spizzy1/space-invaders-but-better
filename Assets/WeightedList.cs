using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class WeightedList<T> : List<WeightedListItem<T>>
    {
        /// <summary>
        /// Takes a random item based on the weights in the list
        /// </summary>
        /// <param name="range">A range between 0 and 1 that makes the output deterministic (1 will always give you the weight at the end, 0.5 will give you in the middle etc.) </param>
        /// <returns></returns>
        public T RandomItem(double range = -1)
        {
            Random rnd = new Random();
            //Returns random double between 0.0 and 1.0
            double sample = range == -1 || range > 1.0 || range < 0 ? rnd.NextDouble() : range;

            //Scales number to the total weight
            double scaled = (sample * TotalWeight);
            float f = (float)scaled;
            float tempf = 0;
            for(int i = 0; i < this.Count; i++)
            {
                tempf += this[i].Weight;
                if(tempf > f)
                {
                    return this[i].Item;
                }
            }
            return this[this.Count].Item;
        }
        /// <summary>
        /// Takes a random item and returns its corresponding index
        /// </summary>
        /// <param name="range">A range between 0 and 1 that makes the output deterministic (1 will always give you the weight at the end, 0.5 will give you in the middle etc.) </param>
        /// <returns></returns>
        public int RandomIndex(double range = -1)
        {
            Random rnd = new Random();
        //Returns random double between 0.0 and 1.0
        double sample = range == -1 || range > 1.0 || range < 0 ? rnd.NextDouble() : range;

        //Scales number to the total weight
        double scaled = (sample * TotalWeight);
        float f = (float)scaled;
        float tempf = 0;
            for(int i = 0; i< this.Count; i++)
            {
                tempf += this[i].Weight;
                if(tempf > f)
                {
                    return i;
                }
            }
            return this.Count;
        }
        public void Add(T item, float weight)
        {
            base.Add(new WeightedListItem<T>(item, weight));
        }
        public float TotalWeight { get { return this.Sum(x => x.Weight); } }

    }
    
    public class WeightedListItem<T>
    {
        public WeightedListItem(T item, float weight)
        {
            item_ = item;
            weight_ = weight;
        }
        T item_;
        public T Item { get { return item_; } set { Item = value; } }
        float weight_;
        public float Weight { get { return weight_; } set { weight_ = value; } }
    }
}
