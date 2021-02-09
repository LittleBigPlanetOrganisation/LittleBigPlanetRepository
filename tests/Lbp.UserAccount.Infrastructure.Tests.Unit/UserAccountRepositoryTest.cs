using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Lbp.UserAccount.Infrastructure;
using NUnit.Framework;
using Xunit;
using System.Threading.Tasks;

namespace Lbp.UserAccount.Infrastructure.Tests.Unit
{
    public class UserAccountRepositoryTest
    {
        private Mock<IDbConnection> _connection = new Mock<IDbConnection>();

        private Mock<DbClientOptions> _dbClientOption = new Mock<DbClientOptions>();

        private UserAccountRepository _repository;

        [Fact]
        public async Task CreateUserAccountParam_Return_Null()
        {
            Assert.Null();
        }

    }
}
