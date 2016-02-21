using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class DbCacheItem<T>
    {

        public DbCacheItem(int exp = 30)
        {
            _expInMins = exp;
        }

        int _expInMins;

        private List<T> _items = null;
        public List<T> Items
        {
            get
            {
                _fetchedTime = DateTime.Now;
                return _items;
            }
            set { _items = value; }
        }
        DateTime _fetchedTime = DateTime.Now;
        public bool IsExpired
        {
            get
            {
                return (DateTime.Now - _fetchedTime).TotalMinutes > 30;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _items == null;
            }
        }

        public bool CanRefresh
        {
            get
            {
                return IsEmpty || IsExpired;
            }
        }
    }
}