using System;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Models;

namespace ShopApi.DAL.Context
{
    public class ShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Users

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.UserId);
            builder.Entity<User>().Property(x => x.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            builder.Entity<User>().Property(x => x.LastName).IsRequired().HasMaxLength(40);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);
            builder.Entity<User>().HasMany(x => x.Orders).WithOne(x => x.User);

            builder.Entity<User>().HasData
            (
                new User
                {
                    UserId = 1111,
                    FirstName = "Vitalik",
                    LastName = "Hurich",
                    Login = "Vitalik",
                    Password = "123"
                },
                new User
                {
                    UserId = 1112,
                    FirstName = "Andrey",
                    LastName = "Andrey",
                    Login = "asd",
                    Password = "asd"
                },
                new User
                {
                    UserId = 1113,
                    FirstName = "I",
                    LastName = "I",
                    Login = "zxcvb",
                    Password = "zxcvb"
                }
            );

            #endregion

            #region Roles

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().HasKey(x => x.RoleId);
            builder.Entity<Role>().Property(x => x.RoleId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.RoleName).IsRequired();
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);

            builder.Entity<Role>().HasData
            (
                new Role { RoleId = 1, RoleName = "Admin"},
                new Role { RoleId = 2, RoleName = "Manager"},
                new Role { RoleId = 3, RoleName = "User"}
            );

            #endregion

            #region UserRoles

            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(x => new {x.RoleId, x.UserId});

            builder.Entity<UserRole>().HasData
            (
                new UserRole { UserId = 1111, RoleId = 1},
                new UserRole { UserId = 1112, RoleId = 2},
                new UserRole { UserId = 1113, RoleId = 3}
            );

            #endregion

            #region Orders

            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(x => x.OrderId);
            builder.Entity<Order>().Property(x => x.OrderId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(x => x.UserId).IsRequired();
            builder.Entity<Order>().Property(x => x.TimeOfCrestion).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne(x => x.Order);

            builder.Entity<Order>().HasData
            (
                new Order { OrderId = 1234, UserId = 1111 },
                new Order { OrderId = 1235, UserId = 1111 },
                new Order { OrderId = 1236, UserId = 1111 }
            );

            #endregion

            #region OrderDetails

            builder.Entity<OrderDetail>().ToTable("OrderDetails");
            builder.Entity<OrderDetail>().HasKey(x => x.OrderDetailId);
            builder.Entity<OrderDetail>().Property(x => x.OrderDetailId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<OrderDetail>().Property(x => x.GoodId).IsRequired();
            builder.Entity<OrderDetail>().Property(x => x.OrderId).IsRequired();

            builder.Entity<OrderDetail>().HasData
            (
                new OrderDetail { OrderDetailId = 555, GoodId = 10000, OrderId = 1234, Count = 1},
                new OrderDetail { OrderDetailId = 556, GoodId = 10001, OrderId = 1234, Count = 2},
                new OrderDetail { OrderDetailId = 557, GoodId = 10002, OrderId = 1235, Count = 1}
            );

            #endregion

            #region Categories

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(x => x.CategoryId);
            builder.Entity<Category>().Property(x => x.CategoryId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(x => x.CategoryName).IsRequired().HasMaxLength(50);
            builder.Entity<Category>().HasMany(x => x.Goods).WithOne(x => x.Category);

            builder.Entity<Category>().HasData
            (
                new Category { CategoryId = 777, CategoryName = "Smartphone"},
                new Category { CategoryId = 778, CategoryName = "Laptop"},
                new Category { CategoryId = 779, CategoryName = "Headphones"},
                new Category { CategoryId = 780, CategoryName = "Refrigerator"},
                new Category { CategoryId = 781, CategoryName = "Washer"},
                new Category { CategoryId = 782, CategoryName = "TV"}
            );

            #endregion

            #region Manufacturers

            builder.Entity<Manufacturer>().ToTable("Manufacturers");
            builder.Entity<Manufacturer>().HasKey(x => x.ManufacturerId);
            builder.Entity<Manufacturer>().Property(x => x.ManufacturerId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Manufacturer>().Property(x => x.ManufacturerName).IsRequired().HasMaxLength(50);
            builder.Entity<Manufacturer>().HasMany(x => x.Goods).WithOne(x => x.Manufacturer);

            builder.Entity<Manufacturer>().HasData
            (
                new Manufacturer { ManufacturerId = 1001, ManufacturerName = "Apple"},
                new Manufacturer { ManufacturerId = 1002, ManufacturerName = "Samsung"},
                new Manufacturer { ManufacturerId = 1003, ManufacturerName = "Xiaomi"},
                new Manufacturer { ManufacturerId = 1004, ManufacturerName = "Huawei"},
                new Manufacturer { ManufacturerId = 1005, ManufacturerName = "Marshall"},
                new Manufacturer { ManufacturerId = 1006, ManufacturerName = "HyperX"},
                new Manufacturer { ManufacturerId = 1007, ManufacturerName = "Bosch"},
                new Manufacturer { ManufacturerId = 1008, ManufacturerName = "LG"}
            );

            #endregion

            #region Good

            builder.Entity<Good>().ToTable("Goods");
            builder.Entity<Good>().HasKey(x => x.GoodId);
            builder.Entity<Good>().Property(x => x.GoodId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Good>().Property(x => x.GoodName).IsRequired().HasMaxLength(50);
            builder.Entity<Good>().Property(x => x.GoodCount).IsRequired();
            builder.Entity<Good>().Property(x => x.GoodPriceMinimal).IsRequired();
            builder.Entity<Good>().Property(x => x.GoodPriceActual).IsRequired();
            builder.Entity<Good>().Property(x => x.Available).IsRequired();
            builder.Entity<Good>().HasMany(x => x.OrderDetails).WithOne(x => x.Good);

            builder.Entity<Good>().HasData
            (
                new Good 
                {
                    GoodId = 10000,
                    GoodName = "IPhone X",
                    GoodCount = 10,
                    GoodPriceMinimal = 1000,
                    GoodPriceActual = 1200,
                    CategoryId = 777,
                    ManufacturerId = 1001,
                    Description = "Black",
                    GoodImageURL = "https://i.citrus.ua/imgcache/size_800/uploads/shop/5/5/5533198c217ea9a4280fdf804df8f088.jpg"
                },
                new Good 
                {
                    GoodId = 10001,
                    GoodName = "Samsung Galaxy S20",
                    GoodCount = 0,
                    GoodPriceMinimal = 1000,
                    GoodPriceActual = 1200,
                    CategoryId = 777,
                    ManufacturerId = 1002,
                    Description = "Blue",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQcxwD1mhDFGWJGt355hmea8WOJtkeCF0lgCrhMQC3fbA4405T43Ev3kFbazwc&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10002,
                    GoodName = "Apple MacBook Pro 13\" A2251 Retina 512GB 2020",
                    GoodCount = 20,
                    GoodPriceMinimal = 2000,
                    GoodPriceActual = 2200,
                    CategoryId = 778,
                    ManufacturerId = 1001,
                    Description = "Silver",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRRTGTNMwvpWVxSAyuxhhngZcNY_DcjW4pgvLX3VyCd9gH8xYCx2XaLfcHF&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10003,
                    GoodName = "Marshall Major III Bluetooth",
                    GoodCount = 10,
                    GoodPriceMinimal = 130,
                    GoodPriceActual = 160,
                    CategoryId = 779,
                    ManufacturerId = 1005,
                    Description = "Black",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQ0djRMG26xkz0_JBXv7WHbhL9vOTRAorCK1YrMEn6S5uMDfGDYwqLBuMofeF19JgrxCrvMkQ_n&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10004,
                    GoodName = "BOSCH KGN39VI306",
                    GoodCount = 5,
                    GoodPriceMinimal = 700,
                    GoodPriceActual = 800,
                    CategoryId = 780,
                    ManufacturerId = 1007,
                    Description = "Silver",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSSeOT9Uryu5uOoLgjBI_WEkXpmGI82qP87FeirEWV4UPgwFFveVU91_u6S&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10005,
                    GoodName = "LG F0J6NN0W",
                    GoodCount = 20,
                    GoodPriceMinimal = 600,
                    GoodPriceActual = 700,
                    CategoryId = 781,
                    ManufacturerId = 1008,
                    Description = "White",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT2DIlJEafxhJkt2j6jf4gg9FnziHx2KJKFyDjtoKAyJGj8FUyQ_cRDHJ1TvWA&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10006,
                    GoodName = "Samsung UE55TU7100UXUA",
                    GoodCount = 0,
                    GoodPriceMinimal = 1000,
                    GoodPriceActual = 1200,
                    CategoryId = 782,
                    ManufacturerId = 1002,
                    Description = "Black",
                    GoodImageURL = "data:image/jpeg;base64,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"
                },
                new Good 
                {
                    GoodId = 10007,
                    GoodName = "Xiaomi Redmi Note 9S 4/64GB",
                    GoodCount = 10,
                    GoodPriceMinimal = 300,
                    GoodPriceActual = 350,
                    CategoryId = 777,
                    ManufacturerId = 1003,
                    Description = "White",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSm41K8iJYAv3Q_roxI5LljYKwVsj8Tgiy4jL9yEyl_p1ZsQCmoeUnWKMH_8xW4znzKcjtUvAc&usqp=CAc"
                },
                new Good 
                {
                    GoodId = 10008,
                    GoodName = "iPhone SE 64GB (2020)",
                    GoodCount = 1,
                    GoodPriceMinimal = 800,
                    GoodPriceActual = 850,
                    CategoryId = 777,
                    ManufacturerId = 1001,
                    Description = "Red",
                    GoodImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQmhfoW9U6OU7VJgLue_6MAm-qRFBCXZZ1fsdnDULc8DibgUJINlp-6X2_xj_U&usqp=CAc"
                }
            );

            #endregion
        }
    }
}