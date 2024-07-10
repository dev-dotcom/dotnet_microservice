using Hashkart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashkart.Domain.Interfaces.IServices
{
    public interface IBooksServices
    {
        Task<Books> Get();
        Task<BaseResponse<string>> Create();
        Task<BaseResponse<string>> Update();
        Task<BaseResponse<string>> Delete();
    }
}
