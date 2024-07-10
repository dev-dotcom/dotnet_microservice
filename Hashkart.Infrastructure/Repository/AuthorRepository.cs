using Hashkart.Domain;
using Hashkart.Domain.Entities;
using Hashkart.Domain.Interfaces;
using Hashkart.Domain.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hashkart.Infrastructure.Helpers;
using System.Data.Common;

namespace Hashkart.Infrastructure.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly StoredProcedureHandler sp = new StoredProcedureHandler();
        public AuthorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponse<long>> Create(Author model)
        {
            try
            {
                var sqlParams = new List<SqlParameter>()
                {
                    new SqlParameter("@mode","add"),
                    new SqlParameter("@name",model.Name),
                    new SqlParameter("@originCountry",model.CountryOfOrigin),
                    new SqlParameter("@createdAt",model.CreatedAt),
                    
                };

                SqlParameter output = new SqlParameter();
                output.ParameterName = "@output";
                output.Direction = ParameterDirection.Output;
                output.SqlDbType = SqlDbType.Int;

                SqlParameter newid = new SqlParameter();
                newid.ParameterName = "@newid";
                newid.Direction = ParameterDirection.Output;
                newid.SqlDbType = SqlDbType.BigInt;

                SqlParameter message = new SqlParameter();
                message.ParameterName = "@message";
                message.Direction = ParameterDirection.Output;
                message.SqlDbType = SqlDbType.NVarChar;
                message.Size = 50;

                return await sp.ExecuteNonQueryAsync(_configuration.GetConnectionString("DBconnection"), "sp_Author", output, newid, message, sqlParams.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<BaseResponse<long>> Delete(Author model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Author>>> Get(int PageIndex, int PageSize)
        {
            try
            {
                var sqlParams = new List<SqlParameter>() {
                    new SqlParameter("@pageSize", PageSize),
                    new SqlParameter("@pageIndex", PageIndex),
                    
                };

                SqlParameter output = new SqlParameter();
                output.ParameterName = "@output";
                output.Direction = ParameterDirection.Output;
                output.SqlDbType = SqlDbType.Int;

                SqlParameter message = new SqlParameter();
                message.ParameterName = "@message";
                message.Direction = ParameterDirection.Output;
                message.SqlDbType = SqlDbType.NVarChar;
                message.Size = 50;

                return await sp.ExecuteReaderAsync(_configuration.GetConnectionString("DBconnection"), "sp_GetAuthor", AuthorParserAsync, output, newid: null, message, sqlParams.ToArray());

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Author>> AuthorParserAsync(DbDataReader reader)
        {
            List<Author> author = new List<Author>();
            while (await reader.ReadAsync())
            {
                author.Add(new Author()
                {
                    //RowNumber = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("RowNumber"))),
                    //PageCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PageCount"))),
                    //RecordCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("RecordCount"))),
                    Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Id"))),
                    Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name"))),
                    CountryOfOrigin = Convert.ToString(reader.GetValue(reader.GetOrdinal("OriginCountry"))),
                    CreatedAt = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("CreatedAt"))),
                    UpdatedAt = string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("UpdatedAt")).ToString()) ? null : Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("UpdatedAt"))),


                });
            }
            return author;
        }

        public Task<BaseResponse<long>> Update(Author model)
        {
            throw new NotImplementedException();
        }
    }
}
