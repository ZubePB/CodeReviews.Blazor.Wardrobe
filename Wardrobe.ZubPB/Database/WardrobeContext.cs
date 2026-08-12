using Microsoft.EntityFrameworkCore;
using WardrobeInventory.Models;
using WardrobeInventory.Server.Database;

namespace WardrobeInventory.Database;

public class WardrobeContext : DbContext
{
    public DbSet<Cloth> Clothes { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<BodyPart> BodyParts { get; set; }

    public DbSet<Set> Sets { get; set; }

    public WardrobeContext(DbContextOptions<WardrobeContext> options) : base(options)
    {
        // Comment/Uncomment to reset data in the database
        //try { Database.EnsureDeleted(); } catch { }
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={Directory.GetCurrentDirectory()}/Database/Wardrobe.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cloth>().HasKey(x => new { x.Id });
        modelBuilder.Entity<Category>().HasKey(x => new { x.Id });
        modelBuilder.Entity<BodyPart>().HasKey(x => new { x.Id });
        modelBuilder.Entity<Set>().HasKey(x => new { x.Id });

        modelBuilder.Entity<Cloth>().HasOne(x => x.Category).WithMany(x => x.Clothes);
        modelBuilder.Entity<Category>().HasOne(x => x.BodyPart).WithMany(x => x.Categories);

        modelBuilder.Entity<Set>().HasOne(x => x.UpperCloth).WithMany().HasConstraintName("set_cloth_upper_fk").HasForeignKey(x => x.UpperClothId);
        modelBuilder.Entity<Set>().HasOne(x => x.LowerCloth).WithMany().HasConstraintName("set_cloth_lower_fk").HasForeignKey(x => x.LowerClothId);
        modelBuilder.Entity<Set>().HasOne(x => x.Shoes).WithMany().HasConstraintName("set_cloth_shoes_fk").HasForeignKey(x => x.ShoesId);

        modelBuilder.Entity<Cloth>().Ignore(x => x.Sets);
        modelBuilder.Entity<Category>().Ignore(x => x.Clothes);

        using (Dataseed ds = new())
        {
            List<Cloth> clothes = ds.GetClothes();
            modelBuilder.Entity<Cloth>().HasData(clothes);
            modelBuilder.Entity<BodyPart>().HasData(ds.GetBodyParts());
            modelBuilder.Entity<Category>().HasData(ds.GetCategories());
            List<Set> sets = ds.GetSets();
            modelBuilder.Entity<Set>().HasData(sets);
        }

        base.OnModelCreating(modelBuilder);
    }
}
