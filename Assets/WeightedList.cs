using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class WeightedList<T> : List<WeightedListItem<T>>
    {
        public float TotalWeight { get { return this.Sum(x => x.Weight); } }

    }
    
    public class WeightedListItem<T>
    {
        WeightedListItem(T item, float weight)
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
