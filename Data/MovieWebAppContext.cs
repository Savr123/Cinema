#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieWebApp.Models;

namespace MovieWebApp.Data
{
    public class MovieWebAppContext : DbContext
    {


        public MovieWebAppContext (DbContextOptions<MovieWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MovieWebApp.Models.User> Users { get; set; }
        public DbSet<MovieWebApp.Models.Movie> Movies { get; set; }
    }
}