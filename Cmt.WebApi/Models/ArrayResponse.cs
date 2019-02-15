using System.Collections.Generic;

namespace Cmt.WebApi.Models
{
    public class ArrayResponse<TModel>
    {
        public IList<TModel> Data { get; set; }
    }
}
