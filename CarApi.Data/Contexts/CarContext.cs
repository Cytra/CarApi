using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarApi.Data.Contexts
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }

        public DbSet<Ad> Ads { get; set; }
    }
}
