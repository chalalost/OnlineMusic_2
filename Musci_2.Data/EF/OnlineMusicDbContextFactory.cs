﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.EF
{
    public class OnlineMusicDbContextFactory : IDesignTimeDbContextFactory<OnlineMusicDbContext>
    {
        /*public OnlineMusicDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("eShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<OnlineMusicDbContext>();
            optionsBuilder.UseSqlServer(connectionStringmodelBuilder.Seed(););

            return new OnlineMusicDbContext(optionsBuilder.Options);
        }*/
        public OnlineMusicDbContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}