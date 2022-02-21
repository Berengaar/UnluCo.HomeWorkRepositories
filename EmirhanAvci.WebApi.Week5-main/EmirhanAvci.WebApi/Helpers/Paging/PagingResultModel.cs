using EmirhanAvci.WebApi.Helpers.Paging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Helpers.Paging
{
    public class PagingResultModel<T> : List<T>
    {
        public PagingQueryParams PagingParams { get; set; }
        public PagingResponseModel Result { get; set; }

        public PagingResultModel(PagingQueryParams pagingParams)
        {
            PagingParams = pagingParams;
            Result = new PagingResponseModel();
        }
        public void GetData(IQueryable<T> query)
        {
            Result.TotalCount = query.Count();
            Result.TotalPages = (int)Math.Ceiling(Result.TotalCount / (double)PagingParams.PageSize);
            Result.CurrentPage = PagingParams.Page;
            Result.NextPage = Result.TotalPages >= Result.CurrentPage + 1 ? Result.CurrentPage + 1 : Result.CurrentPage;
            Result.PreviousPage = Result.CurrentPage >= 1 ? Result.CurrentPage - 1 : Result.CurrentPage;
            //Result.PreviousPage = Result.CurrentPage == 1 ? Result.CurrentPage : Result.CurrentPage - 1;          

            var result = query.Skip((PagingParams.Page - 1) * PagingParams.PageSize).Take(PagingParams.PageSize).ToList();

            // Ordering
            if (!string.IsNullOrWhiteSpace(PagingParams.Sort))
            {
                var entity = typeof(T);

                var property = entity.GetProperty(PagingParams.Sort);

                result.OrderBy(o => property.GetValue(o, null));
            }

            // Searching 
            if (!string.IsNullOrEmpty(PagingParams.Searching))
            {
                var entity = typeof(T);
                var property = entity.GetProperty(PagingParams.Searching);
                result.Where(w => property.GetValue(w, null).ToString().ToLower().Contains(PagingParams.Searching.ToLower()));
            }

            AddRange(result);

        }

    }
}
