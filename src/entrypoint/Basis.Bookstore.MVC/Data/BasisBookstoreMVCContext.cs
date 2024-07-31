using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Basis.Bookstore.Api.Model;

namespace Basis.Bookstore.Mvc.Data
{
    public class BasisBookstoreMvcContext : DbContext
    {
        public BasisBookstoreMvcContext (DbContextOptions<BasisBookstoreMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Basis.Bookstore.Api.Model.SubjectModel> SubjectModel { get; set; } = default!;
        public DbSet<Basis.Bookstore.Api.Model.AuthorModel> AuthorModel { get; set; } = default!;
        public DbSet<Basis.Bookstore.Api.Model.PurchaseMethodModel> PurchaseMethodModel { get; set; } = default!;
        public DbSet<Basis.Bookstore.Api.Model.BookModel> BookModel { get; set; } = default!;
    }
}
