using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Helpers.Paging.Model
{
    public class PagingQueryParams
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string Searching { get; set; }
        public SortingDirection SortingDirection{ get; set; }
    }

    public enum SortingDirection : Int16
    {
        ASC = 1,
        DESC
    }
}
