using Microsoft.EntityFrameworkCore;
using GameLibraryApi.Models;
using Microsoft.Extensions.Options;

namespace GameLibraryApi.Data
{
    public class ApiContext : DbContext 
    {
        public DbSet<GameLibrary> GameLibraries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) :base(options)
        { 
        }
    }
}
