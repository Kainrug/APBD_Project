using Microsoft.EntityFrameworkCore;
using Project_APBD.Enums;
using Project_APBD.Models;

namespace Project_APBD.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected DatabaseContext()
    {

    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IndividualCustomer>().HasData(
            new IndividualCustomer
            {
                Id = 1,
                Address = "Grzybowska",
                Email = "matigorniak@gmail.com",
                PhoneNumber = "123456789",
                FirstName = "Mati",
                LastName = "Gorniak",
                Pesel = "12345678901",
                IsDeleted = false
            },
            new IndividualCustomer
            {
                Id = 2,
                Address = "456 Elm St",
                Email = "jane.smith@example.com",
                PhoneNumber = "987654321",
                FirstName = "Jane",
                LastName = "Smith",
                Pesel = "98765432109",
                IsDeleted = false
            }
        );

        modelBuilder.Entity<CompanyCustomer>().HasData(
            new CompanyCustomer
            {
                Id = 3,
                Address = "Koszykowa",
                Email = "skcompany@gmail.com",
                PhoneNumber = "555123456",
                CompanyName = "Company Inc.",
                KrsNumber = "1234567890",
                IsDeleted = false
            },
            new CompanyCustomer
            {
                Id = 4,
                Address = "Wrzeciono",
                Email = "commerce@gmail.com",
                PhoneNumber = "555987654",
                CompanyName = "Business LLC",
                KrsNumber = "0987654321",
                IsDeleted = false
            }
        );
        
        modelBuilder.Entity<Software>().HasData(
            new Software { Id = 1, Name = "FinanceApp", Description = "Financial software", CurrentVersion = "1.0", Category = "Finance", IsSubscriptionAvailable = false, IsOneTimePurchaseAvailable = true },
            new Software { Id = 2, Name = "EduApp", Description = "Educational software", CurrentVersion = "2.1", Category = "Education", IsSubscriptionAvailable = false, IsOneTimePurchaseAvailable = true }
        );

        modelBuilder.Entity<Discount>().HasData(
            new Discount { Id = 1, Name = "Black Friday Discount", DiscountType = DiscountType.OneTimeFee, Value = 10.0, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 10, 3) }
        );
    }
}