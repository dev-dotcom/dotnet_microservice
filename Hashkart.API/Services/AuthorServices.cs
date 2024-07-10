using Hashkart.Domain;
using Hashkart.Domain.Entities;
using Hashkart.Domain.Interfaces.IRepositories;
using Hashkart.Domain.Interfaces.IServices;

namespace Hashkart.API.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _repository;
        public AuthorServices(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse<long>> Create(Author model)
        {
            return await _repository.Create(model);  
        }

        public async Task<BaseResponse<long>> Delete(Author model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Author>>> Get(int PageIndex, int PageSize) 
        {
            return await _repository.Get(PageIndex, PageSize);   
        }

        public async Task<BaseResponse<long>> Update(Author model)
        {
            throw new NotImplementedException();
        }
    }
}
