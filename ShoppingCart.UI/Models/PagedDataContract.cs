using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShoppingCart.UI.Models
{
    public class PagedDataResultContract<T>
    {
        public PagedDataResultContract(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            RelatedProductDataContract = (IEnumerable<Product>) items;
            Paging = new PagingData
            {
                PageNo = pageNo,
                PageSize = pageSize,
                RecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0 ? (int) Math.Ceiling(totalRecordCount/(double) pageSize) : 0
            };
        }

        public IEnumerable<Product> RelatedProductDataContract { get; set; }

        public PagingData Paging { get; set; }

        public class PagingData
        {
            [JsonProperty("TotalRecordCount")]
            public long RecordCount { get; set; }

            [JsonProperty("PageSize")]
            public int PageSize { get; set; }

            [JsonProperty("TotalPages")]
            public int PageCount { get; set; }

            [JsonProperty("PageNo")]
            public int PageNo { get; set; }
        }
    }
}