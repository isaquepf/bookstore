using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;

namespace BasisBookstore.Infraestructure.Contexts
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<PurchaseMethod> PurchaseMethods { get; set; }

        public DbSet<BookPurchaseMethod> BookPurchaseMethods { get; set; }

        public DbSet<BookSubject> BookSubjects { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(p =>
            {
                p.ToTable("Autor");
                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                 .HasColumnName("CodAu");

                p.Property(p => p.Name)
                 .HasColumnName("Nome")
                 .HasMaxLength(40);
            });

            modelBuilder.Entity<Book>(p =>
            {
                p.ToTable("Livro");
                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                 .HasColumnName("Codl");

                p.Property(p => p.Title)
                 .HasColumnName("Titulo")
                 .HasMaxLength(40)
                 .IsRequired();

                p.Property(p => p.Publisher)
                 .HasColumnName("Editora")
                 .HasMaxLength(40)
                 .IsRequired();

                p.Property(p => p.Edition)
                 .HasColumnName("Edicao")
                 .IsRequired();

                p.Property(p => p.PublishedYear)
                 .HasMaxLength(4)
                 .IsRequired();
            });


            modelBuilder.Entity<Subject>(p =>
            {
                p.ToTable("Assunto");
                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                 .HasColumnName("CodAs");

                p.Property(p => p.Description)
                 .HasColumnName("Descricao")
                 .HasMaxLength(40)
                 .IsRequired();
            });

            modelBuilder.Entity<PurchaseMethod>(p =>
            {
                p.ToTable("FormaCompra");
                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                 .HasColumnName("CodFC");

                p.Property(p => p.Name)
                 .HasColumnName("Descricao")
                 .HasMaxLength(40)
                 .IsRequired();
            });

            modelBuilder.Entity<PurchaseMethod>()
                .HasData(
                    new PurchaseMethod
                    {
                        Id = 1,
                        Name = "Balcão"
                    },
                    new PurchaseMethod
                    {
                        Id = 2,
                        Name = "Self-service"
                    },
                    new PurchaseMethod
                    {
                        Id = 3,
                        Name = "Internet"
                    },
                     new PurchaseMethod
                     {
                         Id = 4,
                         Name = "Evento"
                     },
                     new PurchaseMethod
                     {
                         Id = 5,
                         Name = "Outros"
                     });

            
            modelBuilder.Entity<BookSubject>(p =>
            {
                p.ToTable("Livro_Assunto");
                p.HasKey(p => p.Id);
                p.HasKey(p => new { p.BookId, p.SubjectId });
                p.Property(p => p.BookId).HasColumnName("Codl");
                p.Property(p => p.SubjectId).HasColumnName("CodAs");

                p.HasOne(bc => bc.Book)
                             .WithMany(b => b.BookSubjects)
                             .HasForeignKey(bc => bc.BookId);

                p.HasOne(bc => bc.Subject)
                    .WithMany(c => c.BookSubjects)
                    .HasForeignKey(bc => bc.SubjectId);
            });


            modelBuilder.Entity<BookAuthor>(p =>
            {
                p.ToTable("Livro_Autor");
                p.HasKey(p => p.Id);
                p.HasKey(p => new { p.BookId, p.AuthorId });                
                p.Property(p => p.BookId).HasColumnName("Codl");
                p.Property(p => p.AuthorId).HasColumnName("CodAu");

                p.HasOne(bc => bc.Book)
                             .WithMany(b => b.BookAuthors)
                             .HasForeignKey(bc => bc.BookId);

                p.HasOne(bc => bc.Author)
                    .WithMany(c => c.BookAuthors)
                    .HasForeignKey(bc => bc.AuthorId);
            });

            modelBuilder.Entity<BookPurchaseMethod>(entity =>
            {
                entity.ToTable("Livro_Forma_Pagamento");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.BookId).HasColumnName("Codl");
                entity.Property(p => p.PurchaseMethodId).HasColumnName("CodFC");
                
                entity.HasKey(e => new { e.BookId, e.PurchaseMethodId });
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)")
                      .HasColumnName("Preco")
                      .IsRequired();

                entity.HasOne(e => e.Book)
                    .WithMany(b => b.BookPurchaseMethods)
                    .HasForeignKey(e => e.BookId);

                entity.HasOne(e => e.PurchaseMethod)
                    .WithMany(p => p.BookPurchaseMethods)
                    .HasForeignKey(e => e.PurchaseMethodId);
            });


    }

        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<BookstoreContext>
        {
            public BookstoreContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BookstoreContext>();
                optionsBuilder.UseSqlite("Data Source=C:\\repositories\\dotnet\\BasisBookstore\\src\\entrypoint\\Basis.Bookstore.Api\\Bookstore.db");

                return new BookstoreContext(optionsBuilder.Options);
            }
        }
    }
}

