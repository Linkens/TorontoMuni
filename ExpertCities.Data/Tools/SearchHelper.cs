using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzySharp;

namespace ExpertCities.Data
{
    public class SearchHelper<T>
    {
        public event EventHandler ListChanged;
        public Func<T, string> GetDisplay;
        public Func<T, string> GetSimpleSearch;
        protected List<T> _Items;
        protected List<T> _FilteredItems;
        public List<T> FilteredItems => _FilteredItems;
        protected string _StringSearch;
        protected  Timer _Timer;
        public T Selected { get; set; }
        public string StringSearch { get => _StringSearch; set { _StringSearch = value; SearchChanged(); } }
        public SearchHelper(List<T> Items)
        {
            _Items = Items;
            _FilteredItems = Items.ToList();
            _Timer = new Timer(OnTime, null,Timeout.Infinite, Timeout.Infinite);
        }
        public void SearchChanged()
        {
            _Timer.Change(600, Timeout.Infinite);
        }
        private void OnTime(object? state)
        {
            if( string .IsNullOrEmpty(_StringSearch ))
            {
                _FilteredItems = _Items.ToList();
            }
            else if(GetSimpleSearch !=null)
            {
                var Groups = _Items.GroupBy(i => Fuzz.PartialRatio(  _StringSearch, GetSimpleSearch(i))).OrderByDescending(k=>k.Key);
                var First = Groups.FirstOrDefault();
                if (First != null && First.Any() && First.Key > 15)
                {
                    _FilteredItems = First.ToList();
                    foreach (var item in Groups.Skip(1))
                        if (item.Key > 95) _FilteredItems.AddRange(item.ToList());
                }
                else
                    _FilteredItems = new List<T>();
            }
            ListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
