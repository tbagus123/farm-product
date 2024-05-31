using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFarmProduct.Models;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace MyFarmProduct.Data
{
    public class DataSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                string[] roles = new string[] { "Employee", "Farmer" };

                var newrolelist = new List<IdentityRole>();
                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        var iRole = new IdentityRole
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        };
                        newrolelist.Add(iRole);
                    }
                }
                await context.Roles.AddRangeAsync(newrolelist);
                await context.SaveChangesAsync();

                #region Seed Employee
                var user1 = new IdentityUser
                {
                    UserName = "user1@hotmail.com",
                    NormalizedUserName = "USER1@HOTMAIL.COM",
                    Email = "user1@hotmail.com",
                    NormalizedEmail = "USER1@HOTMAIL.COM",
                    PhoneNumber = "+2711111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                var user2 = new IdentityUser
                {
                    UserName = "user2@hotmail.com",
                    NormalizedUserName = "USER2@HOTMAIL.COM",
                    Email = "user2@hotmail.com",
                    NormalizedEmail = "USER2@HOTMAIL.COM",
                    PhoneNumber = "+2722222222222",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var employee1 = new Employee
                {
                    Name = "Thabiso Mlambo",
                    Email = "user1@hotmail.com",
                    Contact = "+2711111111111",
                    Address = "123 Main Street",
                    City = "Johannesburg",
                    State = "Gauteng",
                    ZipCode = "2000",
                    Department = "IT",
                };

                var employee2 = new Employee
                {
                    Name = "Sipho Mlambo",
                    Email = "user2@hotmail.com",
                    Contact = "+2722222222222",
                    Address = "456 Main Street",
                    City = "Pretoria",
                    State = "Gauteng",
                    ZipCode = "1000",
                    Department = "HR",
                };

                var userStore = new UserStore<IdentityUser>(context);
                if (!userStore.Users.Any(u => u.UserName == user1.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user1, "User.123");
                    user1.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(user1);
                    employee1.UserId = user1.Id;
                }
                else
                {
                    user1 = userStore.Users.FirstOrDefault(u => u.UserName == user1.UserName);
                }

                if (!userStore.Users.Any(u => u.UserName == user2.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user2, "User.145");
                    user2.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(user2);
                    employee2.UserId = user2.Id;
                }
                else
                {
                    user2 = userStore.Users.FirstOrDefault(u => u.UserName == user2.UserName);
                }
                if (!context.Employees.Any(u => u.Name == employee1.Name))
                {
                    await context.Employees.AddAsync(employee1);
                }
                if (!context.Employees.Any(u => u.Name == employee2.Name))
                {
                    await context.Employees.AddAsync(employee2);
                }
                if (userStore.GetRolesAsync(user1).Result.Count == 0)
                    await userStore.AddToRoleAsync(user1, "EMPLOYEE");
                if (userStore.GetRolesAsync(user2).Result.Count == 0)
                    await userStore.AddToRoleAsync(user2, "EMPLOYEE");
                #endregion


                #region Seed Farmer
                var farmerUser1 = new IdentityUser
                {
                    UserName = "adriaan@hotmail.com",
                    NormalizedUserName = "ADRIAAN@HOTMAIL.COM",
                    Email = "adriaan@hotmail.com",
                    NormalizedEmail = "ADRIAAN@HOTMAIL.COM",
                    PhoneNumber = "+2733333333333",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var farmerUser2 = new IdentityUser
                {
                    UserName = "petrus@hotmail.com",
                    NormalizedUserName = "PETRUS@HOTMAIL.COM",
                    Email = "petrus@hotmail.com",
                    NormalizedEmail = "PETRUS@HOTMAIL.COM",
                    PhoneNumber = "+2744444444444",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var farmerUser3 = new IdentityUser
                {
                    UserName = "justin@hotmail.com",
                    NormalizedUserName = "JUSTIN@HOTMAIL.COM",
                    Email = "justin@hotmail.com",
                    NormalizedEmail = "JUNSTIN@HOTMAIL.COM",
                    PhoneNumber = "+2755555555555",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var farmer1 = new Farmer
                {
                    Name = "Adriaan Matthews",
                    Email = "adriaan@hotmail.com",
                    Contact = "+2711111111111",
                    Address = "123 Main Street",
                    City = "Johannesburg",
                    State = "Gauteng",
                    ZipCode = "2000",
                    FarmSize = 100,
                    AdditionalInfo = "I have a farm in the North West",
                    UserId = farmerUser1.Id
                };

                var farmer2 = new Farmer
                {
                    Name = "Petrus Sithole",
                    Email = "petrus@hotmail.com",
                    Contact = "+2722222222222",
                    Address = "456 Main Street",
                    City = "Pretoria",
                    State = "Gauteng",
                    ZipCode = "1000",
                    FarmSize = 153,
                    AdditionalInfo = "I have a farm in the Free State",
                    UserId = farmerUser2.Id
                };

                var farmer3 = new Farmer
                {
                    Name = "Justin Joubert",
                    Email = "justin@hotmail.com",
                    Contact = "+2722222222222",
                    Address = "456 Main Street",
                    City = "Pretoria",
                    State = "Gauteng",
                    ZipCode = "1000",
                    FarmSize = 125,
                    AdditionalInfo = "I have a farm in the Eastern Cape",
                    UserId = farmerUser3.Id
                };

                if (!context.Users.Any(u => u.UserName == farmerUser1.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(farmerUser1, "Farmer.1");
                    farmerUser1.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(farmerUser1);
                    farmer1.UserId = farmerUser1.Id;
                }
                else
                {
                    farmerUser1 = userStore.Users.FirstOrDefault(u => u.UserName == farmerUser1.UserName);
                }
                await context.SaveChangesAsync();
                if (!context.Users.Any(u => u.UserName == farmerUser2.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(farmerUser2, "Farmer.2");
                    farmerUser2.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(farmerUser2);
                    farmer2.UserId = farmerUser2.Id;
                }
                else
                {
                    farmerUser2 = userStore.Users.FirstOrDefault(u => u.UserName == farmerUser2.UserName);
                }
                await context.SaveChangesAsync();

                if (!context.Users.Any(u => u.UserName == farmerUser3.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(farmerUser3, "Farmer.3");
                    farmerUser3.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(farmerUser3);
                    farmer3.UserId = farmerUser3.Id;
                }
                else
                {
                    farmerUser3 = userStore.Users.FirstOrDefault(u => u.UserName == farmerUser3.UserName);
                }
                await context.SaveChangesAsync();

                if (!context.Farmers.Any(u => u.Name == farmer1.Name))
                {
                    await context.Farmers.AddAsync(farmer1);
                }
                if (!context.Farmers.Any(u => u.Name == farmer2.Name))
                {
                    await context.Farmers.AddAsync(farmer2);
                }
                if (!context.Farmers.Any(u => u.Name == farmer3.Name))
                {
                    await context.Farmers.AddAsync(farmer3);
                }

                if (userStore.GetRolesAsync(farmerUser1).Result.Count == 0)
                    await userStore.AddToRoleAsync(farmerUser1, "FARMER");
                if (userStore.GetRolesAsync(farmerUser2).Result.Count == 0)
                    await userStore.AddToRoleAsync(farmerUser2, "FARMER");
                if (userStore.GetRolesAsync(farmerUser3).Result.Count == 0)
                    await userStore.AddToRoleAsync(farmerUser3, "FARMER");
                #endregion

                #region Seed Product
                Product product1 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Maize (Corn)",
                    Category = "Farm Product",
                    Description = "Description for Maize (Corn)",
                    ProductionDate = DateTime.UtcNow,
                    Price = 10.0,
                    Quantity = 100,
                    Unit = "kg",
                    Image = "maizeseeds.png",
                    FarmerId = farmer1.Id
                };

                Product product2 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wheat",
                    Category = "Farm Product",
                    Description = "Description for Wheat",
                    ProductionDate = DateTime.UtcNow,
                    Price = 15.0,
                    Quantity = 120,
                    Unit = "kg",
                    Image = "Wheat.png",
                    FarmerId = farmer1.Id
                };

                Product product3 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Barley",
                    Category = "Farm Product",
                    Description = "Description for Barley",
                    ProductionDate = DateTime.UtcNow,
                    Price = 12.0,
                    Quantity = 80,
                    Unit = "kg",
                    Image = "Barley.png",
                    FarmerId = farmer1.Id
                };
                Product product4 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Sorghum",
                    Category = "Farm Product",
                    Description = "Description for Sorghum",
                    ProductionDate = DateTime.UtcNow,
                    Price = 8.0, // Sample price
                    Quantity = 90, // Sample quantity
                    Unit = "kg",
                    Image = "Sorghum.png",
                    FarmerId = farmer1.Id
                };

                Product product5 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Sunflower seeds",
                    Category = "Farm Product",
                    Description = "Description for Sunflower seeds",
                    ProductionDate = DateTime.UtcNow,
                    Price = 20.0, // Sample price
                    Quantity = 50, // Sample quantity
                    Unit = "kg",
                    Image = "Sunflowerseeds.png",
                    FarmerId = farmer1.Id
                };

                Product product6 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Soybeans",
                    Category = "Farm Product",
                    Description = "Description for Soybeans",
                    ProductionDate = DateTime.UtcNow,
                    Price = 10.0, // Sample price
                    Quantity = 100, // Sample quantity
                    Unit = "kg",
                    Image = "Soybeans.png",
                    FarmerId = farmer2.Id
                };

                Product product7 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Potatoes",
                    Category = "Vegetable",
                    Description = "Description for Potatoes",
                    ProductionDate = DateTime.UtcNow,
                    Price = 8.0, // Sample price
                    Quantity = 150, // Sample quantity
                    Unit = "kg",
                    Image = "Potatoes.png",
                    FarmerId = farmer2.Id
                };

                Product product8 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Sugarcane",
                    Category = "Farm Product",
                    Description = "Description for Sugarcane",
                    ProductionDate = DateTime.UtcNow,
                    Price = 5.0, // Sample price
                    Quantity = 200, // Sample quantity
                    Unit = "kg",
                    Image = "Sugarcane.png",
                    FarmerId = farmer2.Id
                };

                Product product9 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Apples",
                    Category = "Fruit",
                    Description = "Description for Apples",
                    ProductionDate = DateTime.UtcNow,
                    Price = 2.5, // Sample price
                    Quantity = 50, // Sample quantity
                    Unit = "kg",
                    Image = "Apples.png",
                    FarmerId = farmer2.Id
                };

                Product product10 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Oranges",
                    Category = "Fruit",
                    Description = "Description for Oranges",
                    ProductionDate = DateTime.UtcNow,
                    Price = 1.8, // Sample price
                    Quantity = 40, // Sample quantity
                    Unit = "kg",
                    Image = "Oranges.png",
                    FarmerId = farmer2.Id
                };

                Product product11 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Grapes",
                    Category = "Fruit",
                    Description = "Description for Grapes",
                    ProductionDate = DateTime.UtcNow,
                    Price = 3.0, // Sample price
                    Quantity = 30, // Sample quantity
                    Unit = "kg",
                    Image = "Grapes.png",
                    FarmerId = farmer3.Id
                };

                Product product12 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Peaches",
                    Category = "Fruit",
                    Description = "Description for Peaches",
                    ProductionDate = DateTime.UtcNow,
                    Price = 2.0, // Sample price
                    Quantity = 20, // Sample quantity
                    Unit = "kg",
                    Image = "Peaches.png",
                    FarmerId = farmer3.Id
                };

                Product product13 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Nectarines",
                    Category = "Fruit",
                    Description = "Description for Nectarines",
                    ProductionDate = DateTime.UtcNow,
                    Price = 2.2, // Sample price
                    Quantity = 25, // Sample quantity
                    Unit = "kg",
                    Image = "Nectarines.png",
                    FarmerId = farmer3.Id
                };

                Product product14 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Plums",
                    Category = "Fruit",
                    Description = "Description for Plums",
                    ProductionDate = DateTime.UtcNow,
                    Price = 1.5, // Sample price
                    Quantity = 35, // Sample quantity
                    Unit = "kg",
                    Image = "Plums.png",
                    FarmerId = farmer3.Id
                };

                Product product15 = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tomatoes",
                    Category = "Vegetable",
                    Description = "Description for Tomatoes",
                    ProductionDate = DateTime.UtcNow,
                    Price = 1.0, // Sample price
                    Quantity = 60, // Sample quantity
                    Unit = "kg",
                    Image = "Tomatoes.png",
                    FarmerId = farmer3.Id
                };
                if (!context.Products.Any(u => u.Name == product1.Name))
                {
                    await context.Products.AddAsync(product1);
                }
                if (!context.Products.Any(u => u.Name == product2.Name))
                {
                    await context.Products.AddAsync(product2);
                }
                if (!context.Products.Any(u => u.Name == product3.Name))
                {
                    await context.Products.AddAsync(product3);
                }
                if (!context.Products.Any(u => u.Name == product4.Name))
                {
                    await context.Products.AddAsync(product4);
                }
                if (!context.Products.Any(u => u.Name == product5.Name))
                {
                    await context.Products.AddAsync(product5);
                }
                if (!context.Products.Any(u => u.Name == product6.Name))
                {
                    await context.Products.AddAsync(product6);
                }
                if (!context.Products.Any(u => u.Name == product7.Name))
                {
                    await context.Products.AddAsync(product7);
                }
                if (!context.Products.Any(u => u.Name == product8.Name))
                {
                    await context.Products.AddAsync(product8);
                }
                if (!context.Products.Any(u => u.Name == product9.Name))
                {
                    await context.Products.AddAsync(product9);
                }
                if (!context.Products.Any(u => u.Name == product10.Name))
                {
                    await context.Products.AddAsync(product10);
                }
                if (!context.Products.Any(u => u.Name == product11.Name))
                {
                    await context.Products.AddAsync(product11);
                }
                if (!context.Products.Any(u => u.Name == product12.Name))
                {
                    await context.Products.AddAsync(product12);
                }
                if (!context.Products.Any(u => u.Name == product13.Name))
                {
                    await context.Products.AddAsync(product13);
                }
                if (!context.Products.Any(u => u.Name == product14.Name))
                {
                    await context.Products.AddAsync(product14);
                }
                if (!context.Products.Any(u => u.Name == product15.Name))
                {
                    await context.Products.AddAsync(product15);
                }
                #endregion
                context.SaveChanges();
            }
        }
    }
}
