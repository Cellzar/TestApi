using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace TestDB.Context
{
    public partial class TestContext : DbContext
    {
        private string CadenaConexion = "";
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options, IConfiguration configuration)
            : base(options)
        {
            CadenaConexion = configuration.GetConnectionString("DefaultConnection");
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=CESARTG; database=Test;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Author");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Book");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Excerpt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public bool SaveAllBooks(DataTable data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                using (SqlConnection con = new SqlConnection(CadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("[Sp_SaveAllBooks]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var parametro = cmd.Parameters.AddWithValue("@tbl", data);
                        parametro.SqlDbType = SqlDbType.Structured;
                        parametro.TypeName = "dbo.Book_type";
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDatos);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SaveAllAuthors(DataTable data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                using (SqlConnection con = new SqlConnection(CadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("[Sp_SaveAllAuthors]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var parametro = cmd.Parameters.AddWithValue("@tbl", data);
                        parametro.SqlDbType = SqlDbType.Structured;
                        parametro.TypeName = "dbo.Author_type";
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDatos);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
