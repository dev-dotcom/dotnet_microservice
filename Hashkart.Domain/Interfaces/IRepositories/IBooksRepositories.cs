using Hashkart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashkart.Domain.Interfaces.IRepositories
{
    public interface IBooksRepositories
    {
        Task<Books> Get();
        Task<BaseResponse<long>> Create(Author model);
        Task<BaseResponse<long>> Update(Author model);
        Task<BaseResponse<long>> Delete(Author model);
    }
}
