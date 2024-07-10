﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hashkart.Domain;

namespace Hashkart.Infrastructure.Helpers
{
    internal class StoredProcedureHandler
    {
        internal async Task<BaseResponse<T>> ExecuteReaderAsync<T>(string connectionString, string storedProc, Func<DbDataReader, Task<T>> readerParserAction, SqlParameter output = null, SqlParameter newid = null, SqlParameter message = null, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProc, connection))
                {
                    try
                    {
                        command.CommandTimeout = 180;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        }

                        if (output != null) command.Parameters.Add(output);
                        if (newid != null) command.Parameters.Add(newid);
                        if (message != null) command.Parameters.Add(message);

                        T response;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            response = await readerParserAction(reader);
                        }

                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        if (message != null)
                            dict = new Dictionary<string, object>
                            {
                                {"message", Convert.ToString(command.Parameters["@message"].Value)}
                            };

                        return new BaseResponse<T>()
                        {
                            code = output != null ? Convert.ToInt32(command.Parameters["@output"].Value) : 1,
                            message = message != null ? Convert.ToString(command.Parameters["@message"].Value) : "",
                            data = response
                            //extension = dict
                        };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.CloseAsync();
                        command.Parameters.Clear();
                    }
                }
            }
        }
        internal async Task<BaseResponse<long>> ExecuteNonQueryAsync(string connectionString, string storedProc, SqlParameter output = null, SqlParameter newid = null, SqlParameter message = null, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProc, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 180;
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        }

                        if (output != null) command.Parameters.Add(output);
                        if (newid != null) command.Parameters.Add(newid);
                        if (message != null) command.Parameters.Add(message);

                        await command.ExecuteNonQueryAsync();

                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        if (message != null)
                            dict = new Dictionary<string, object>
                            {
                                {"message", Convert.ToString(command.Parameters["@message"].Value)}
                            };

                        return new BaseResponse<long>()
                        {
                            code = output != null ? Convert.ToInt32(command.Parameters["@output"].Value) : 1,
                            message = message != null ? Convert.ToString(command.Parameters["@message"].Value) : "",
                            data = newid != null ? Convert.ToInt64(command.Parameters["@newid"].Value) : 0
                            //extension = dict
                        };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.CloseAsync();
                        command.Parameters.Clear();
                    }
                }
            }
        }

        
    }
    
}
