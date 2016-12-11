using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Kenrapid.CRM.Web.Infrastructure
{
    public class PagedViewModel<T> 
    {
        private readonly IEnumerable<T> _list = null;
        private readonly int _count = 0;
        public PagedViewModel(IEnumerable<T> list, int count)
        {
            _list = list;
            _count = count;
        }
        public int TotalCount { get { return _count; }
           
        }

        public IEnumerable<T> Data { get { return _list; }
          
        }
    }
}