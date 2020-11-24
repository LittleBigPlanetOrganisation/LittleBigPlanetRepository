using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UserAccount.Infrastructure
{
    public class DbClientOptions : IOptions<DbClientOptions>
    {
        public DbClientOptions Value => this;


        public Func<IDbConnection> LittleBigPlanetData { get; set; }

    }
}
