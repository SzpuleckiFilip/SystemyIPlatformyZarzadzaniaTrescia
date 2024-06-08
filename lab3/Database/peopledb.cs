// namespace Company.People.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Cdv.Api.Database;

// public class peopledb : DbContext
// {
//     public peopledb(DbContextOptions<peopledb> options) : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);

//         var people = modelBuilder.Entity<Person>();
//         people.ToTable("Person");
//         people.HasKey(pk => pk.PersonId);
//         people.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
//         people.Property(p => p.LastName).HasMaxLength(100).IsRequired();
//     }

//     public DbSet<Person> People { get; set; }
// }