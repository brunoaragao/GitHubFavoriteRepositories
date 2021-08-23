using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubFavoriteRepositories.DbContexts
{
    public class GitHubIntegrationContext : DbContext
    {
        public DbSet<FavoriteRepository> FavoriteRepositories { get; set; }

        public string DbPath { get; private set; }

        public GitHubIntegrationContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}GitHubIntegration.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    [Index(nameof(RepositoryId), IsUnique = true)]
    public class FavoriteRepository
    {
        public long Id { get; set; }
        public long RepositoryId { get; set; }
    }
}
