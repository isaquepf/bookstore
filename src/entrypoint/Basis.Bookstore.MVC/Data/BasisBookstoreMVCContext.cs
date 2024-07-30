using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Basis.Bookstore.Api.Model;

namespace Basis.Bookstore.MVC.Data
{
    public class BasisBookstoreMVCContext : DbContext
    {
        public BasisBookstoreMVCContext (DbContextOptions<BasisBookstoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Basis.Bookstore.Api.Model.AuthorModel> AuthorModel { get; set; } = default!;
        public DbSet<Basis.Bookstore.Api.Model.PurchaseMethodModel> PurchaseMethodModel { get; set; } = default!;
        public DbSet<Basis.Bookstore.Api.Model.BookModel> BookModel { get; set; } = default!;
    }
}
