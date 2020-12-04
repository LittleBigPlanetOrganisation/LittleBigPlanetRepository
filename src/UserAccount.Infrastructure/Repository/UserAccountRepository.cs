using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Infrastructure.Cache;
using UserAccount.Infrastructure.Dtos;

namespace UserAccount.Infrastructure.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private readonly Func<IDbConnection> _connection;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// The cache configuration
        /// </summary>
        private readonly CacheConfiguration _configuration;
        private readonly CacheHelper _cacheHelper;


        public UserAccountRepository(IOptions<DbClientOptions> connection, IMemoryCache cache, CacheConfiguration configuration)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = connection?.Value?.LittleBigPlanetData ?? throw new ArgumentNullException(nameof(connection));

            _cacheHelper = new CacheHelper(_cache);
        }

        public async Task CreateUserWhithAllParam(UserAccountAllParamDto param)
        {
            if(param == null)
            {
                return;
            }
            using (var connection = _connection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idUser", dbType: DbType.Int64, value: param.idUser);
                parameters.Add("@firstName", dbType: DbType.String, value: param.firstName);
                parameters.Add("@lastname", dbType: DbType.String, value: param.lastname);
                parameters.Add("@postalAddress", dbType: DbType.String, value: param.postalAddress);
                parameters.Add("@postalCode", dbType: DbType.String, value: param.postalCode);
                parameters.Add("@city", dbType: DbType.String, value: param.city);
                parameters.Add("@country", dbType: DbType.String, value: param.country);
                parameters.Add("@email", dbType: DbType.String, value: param.email);
                parameters.Add("@password", dbType: DbType.String, value: param.password);
                parameters.Add("@isAdmin", dbType: DbType.Boolean, value: param.isAdmin);
                parameters.Add("@urlPicture", dbType: DbType.String, value: param.urlPicture);
                parameters.Add("@updateDate", dbType: DbType.DateTime2, value: DateTime.Now);
                await connection.QueryAsync(
                    Constant.StoredProcedure.UserAccount.CreateUserAccount,
                    parameters,
                    commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task UpdateUserAccount(UserAccountAllParamDto param)
        {
            if (param == null)
            {
                return;
            }
            using (var connection = _connection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idUser", dbType: DbType.Int64, value: param.idUser);
                parameters.Add("@firstName", dbType: DbType.String, value: param.firstName);
                parameters.Add("@lastname", dbType: DbType.String, value: param.lastname);
                parameters.Add("@surName", dbType: DbType.String, value: param.surName);
                parameters.Add("@postalAddress", dbType: DbType.String, value: param.postalAddress); // pas dans la proc
                parameters.Add("@postalCode", dbType: DbType.String, value: param.postalCode);       //  pas dans la proc
                parameters.Add("@city", dbType: DbType.String, value: param.city);                //   pas dans la proc
                parameters.Add("@country", dbType: DbType.String, value: param.country);           //  pas dans la proc
                parameters.Add("@email", dbType: DbType.String, value: param.email);
                parameters.Add("@password", dbType: DbType.String, value: param.password);
                parameters.Add("@urlPicture", dbType: DbType.String, value: param.urlPicture);
                parameters.Add("@updateDate", dbType: DbType.DateTime2, value: DateTime.Now);
                await connection.QueryAsync(
                    Constant.StoredProcedure.UserAccount.SetUserAccount,
                    parameters,
                    commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<UserAccountAllParamDto>> GetUserAccountById(long idUser)
        {
            if (idUser < 0)
            {
                return null;
            }
            using (var connection = _connection())
            { 
                var res = await _cache.GetOrCreateAsync(
                    string.Format(Constant.Cache.UserAccountKey, idUser),
                    async entry =>
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@idUser", dbType: DbType.Int64, value: idUser);

                        entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(_configuration.UserAccountExpirationTime));
                        var result = await connection.QueryAsync<UserAccountAllParamDto>(
                            Constant.StoredProcedure.UserAccount.GetUserAccount,
                            parameters,
                            commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        entry.Priority = CacheItemPriority.High;
                        entry.Size = Constant.Cache.UserAccountMediumSize;
                        return result;
                    }).ConfigureAwait(false);
                return res;
            }
        }

        public async Task<UserAccountAllParamDto> GetUserAccountByLogin(string surName, string password)
        {
            if (password.Length < 5 || surName.Length <= 0)
            {
                return null;
            }
            using (var connection = _connection())
            {
                var res = await _cache.GetOrCreateAsync(
                    string.Format(Constant.Cache.UserAccountKey, surName, password),
                    async entry =>
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@surName", dbType: DbType.Int64, value: surName);
                        parameters.Add("@password", dbType: DbType.Int64, value: password);

                        entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(_configuration.UserAccountExpirationTime));
                        var result = await connection.QueryAsync<UserAccountAllParamDto>(
                            Constant.StoredProcedure.UserAccount.GetUserAccount,
                            parameters,
                            commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        entry.Priority = CacheItemPriority.High;
                        entry.Size = Constant.Cache.UserAccountMediumSize;
                        return result.FirstOrDefault();
                    }).ConfigureAwait(false);
                return res;
            }
        }

        public async Task DeleteUserAccount(long idUser)
        {
            await this.DeleteUserAccount(idUser, Constant.StoredProcedure.UserAccount.DeleteUserAccount)
                .ConfigureAwait(false);
        }

        private async Task DeleteUserAccount(long idUser, string proc)
        {
            if (idUser < 0)
            {
                using (var connection = _connection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@idUser", dbType: DbType.Int64, value: idUser);
                    parameters.Add("@updateDate", dbType: DbType.DateTime2, value: DateTime.Now);
                    await connection.QueryAsync(
                        proc,
                        parameters,
                        commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                }
            }
        }
    }
}
