using System.Numerics;

namespace Hashkart.API.Models
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public object Data { get; set; }
        public Meta Meta { get; set; }

        public ApiResponse(object data, int totalRecords = 0, int totalPages = 0, int thisPageSize = 0)
        {
            this.Status = "success";
            this.Data = data;

            if (data is System.Collections.IEnumerable && !(data is string))
            {
                this.Meta = new Meta
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    thisPageSize = thisPageSize
                };
            }
        }
    }
    public class Meta
    {
        public int? TotalRecords { get; set; }
        public int? TotalPages { get; set; }
        public int? thisPageSize { get; set; }

    }
}
