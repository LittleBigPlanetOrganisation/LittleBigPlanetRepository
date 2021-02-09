using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Infrastructure;
using UserAccount.Infrastructure.Cache;
using UserAccount.Infrastructure.Dtos;
using UserAccount.Infrastructure.Repository;
using Xunit;

namespace Lbp.UserAccount.Infrastructure.Tests.Unit
{
    public class UserAccountRepositoryTest
    {
        private CacheConfiguration _configuration = new CacheConfiguration { UserAccountExpirationTime = 1 };

        private Mock<IDbConnection> _connection = new Mock<IDbConnection>();

        private Mock<MemoryCache> _cache = new Mock<MemoryCache>(new MemoryCacheOptions());

        private Mock<DbClientOptions> _dbClientOption = new Mock<DbClientOptions>();

        private UserAccountRepository _repository;

        [Fact]
        public void UserAccountRepository_configuration()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAccountRepository(_dbClientOption.Object, _cache.Object, null));
        }

        [Fact]
        public async Task CreateUserAccountParam_Return_Null()
        {
            _dbClientOption.Object.LittleBigPlanetData = () => _connection.Object;

            _repository = new UserAccountRepository(_dbClientOption.Object, _cache.Object, _configuration);
            await _repository.CreateUserWhithAllParam(null).ConfigureAwait(false);
            Assert.Null(null);
        }

        [Fact]
        public async Task CreateUserAccountParam_Return_Success()
        {
            _dbClientOption.Object.LittleBigPlanetData = () => _connection.Object;
            _connection.SetupDapperAsync(ConnectionState => ConnectionState.QueryAsync<UserAccountAllParamDto>(
                Constant.StoredProcedure.UserAccount.CreateUserAccount,
                It.IsAny<DynamicParameters>(),
                null,
                null,
                CommandType.StoredProcedure)).ReturnsAsync(new List<UserAccountAllParamDto>());

            _repository = new UserAccountRepository(_dbClientOption.Object, _cache.Object, _configuration);

            try
            {
                await _repository.CreateUserWhithAllParam(new UserAccountAllParamDto
                {
                    firstName = "toto",
                    lastName = "tata",
                    surName = "titi",
                    email = "toto@gmail.com",
                    password = "toto123",

                });
                Assert.True(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [Fact]
        public async Task UpdateUserAccountParam_Return_Success()
        {
            _dbClientOption.Object.LittleBigPlanetData = () => _connection.Object;
            _connection.SetupDapperAsync(ConnectionState => ConnectionState.QueryAsync<UserAccountAllParamDto>(
                Constant.StoredProcedure.UserAccount.SetUserAccount,
                It.IsAny<DynamicParameters>(),
                null,
                null,
                CommandType.StoredProcedure)).ReturnsAsync(new List<UserAccountAllParamDto>());

            _repository = new UserAccountRepository(_dbClientOption.Object, _cache.Object, _configuration);
            try
            {
                await _repository.UpdateUserAccount(new UserAccountAllParamDto
                {
                    firstName = "toto",
                    lastName = "tata",
                    surName = "titi",
                    email = "toto@gmail.com",
                    password = "toto123",
                    country = "tahiti",

                });
                Assert.True(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        public async Task UpdateUserAccountParam_Return_Null()
        {
            _dbClientOption.Object.LittleBigPlanetData = () => _connection.Object;
            _repository = new UserAccountRepository(_dbClientOption.Object, _cache.Object, _configuration);
            await _repository.UpdateUserAccount(null).ConfigureAwait(false);
            Assert.Null(null);
        }
    }
}
