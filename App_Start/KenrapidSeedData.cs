using System.Linq;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Kenrapid.CRM.Web.Utilities;
using System;

namespace Kenrapid.CRM.Web
{
    public class KenrapidSeedData : IRunAtStartup
    {
        private readonly KenrapidDbContext _context;
        public UserManager<ApplicationUser> userManager { get; private set; }
        public KenrapidSeedData(KenrapidDbContext context)
        {
            _context = context;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public void Execute()
        {
            var user1 = _context.Users.FirstOrDefault() ??
                        _context.Users.Add(new ApplicationUser { UserName = "JuneTodd" });

            var user2 = _context.Users.FirstOrDefault(u => u.UserName == "DougStone") ??
                        _context.Users.Add(new ApplicationUser { UserName = "DougStone" });

            var user3 = _context.Users.FirstOrDefault(u => u.UserName == "GarrettHoward") ??
                        _context.Users.Add(new ApplicationUser { UserName = "GarrettHoward" });

            _context.SaveChanges();

            if (!_context.Issues.Any())
            {
                _context.Issues.Add(new Issue(user2, user1, IssueType.Bug, "Viewing details crashes", "Sometimes, viewing an issue's details will cause a crash.  It seems to only happen when there is a full moon out!"));
                _context.Issues.Add(new Issue(user3, user1, IssueType.Support, "Second account", "I need a second account for my cat to use.  My cat finds all kinds of bugs, and I really want him to be able to log the issues himself."));
                _context.Issues.Add(new Issue(user1, user2, IssueType.Enhancement, "Tablet-Friendly UX", "I'd like to see the app support use from a tablet.  The web app works from a tablet, but it's clunky.  I want the UX to be streamlined and optimized for touch."));

                _context.SaveChanges();
            }

            userManager.Create(new ApplicationUser
            {
                Email = "dev.linhlv@gmail.com",
                UserName = "admin"
            }, "123456");
            
            userManager.Create(new ApplicationUser
            {
                Email = "dev.linhlv@gmail.com",
                UserName = "linhle"
            }, "P@ssw0rd1");

            if (!_context.Customers.Any())
            {
                AddNewCustomers(_context);

                AddExistingCustomers(_context);

                AddTerminatedCustomers(_context);

                AddVendors(_context);

                AddCategories(_context);

                AddMaterials(_context);

                AddColors(_context);

                _context.SaveChanges();

                AddProducts(_context);
            }
        }


        private static readonly decimal[] _prices = { 39.99m, 29.99m, 19.99m, 49.99m };
        private static readonly decimal[] _sizes = { 10, 20, 25, 15, 30, 5, 45, 40, 55, 35 };
        private static readonly decimal[] _pcsse = { 10, 20, 24, 30 };
        private static readonly decimal[] _cbm = { 10, 25, 15, 30, 5, 30 };
        private static readonly decimal[] _cm = { 37, 56, 29, 62, 28, 43, 33, 31};

        private static readonly string[] _description = { "Square Box", "Compartment tray", "Napkin holder memo box", "A short description of the item", "Geometry. the act or process of describing a figure." };

        private static void AddProducts(KenrapidDbContext context)
        {
            for (var i = 0; i < 500; i++)
            {
                var prod = new Product
                {
                    //Name = "".RandomName(20, false),
                    Code = "".RandomName(6, false),
                    Material =
                        context.Materials.OrderBy(o => o.Id)
                            .Skip(new Random().Next(context.Materials.Count()))
                            .FirstOrDefault(),
                    Category =
                        context.Categories.OrderBy(o => o.Id)
                            .Skip(new Random().Next(context.Categories.Count()))
                            .FirstOrDefault(),
                    Vendor =
                        context.Vendors.OrderBy(o => o.Id)
                            .Skip(new Random().Next(context.Vendors.Count()))
                            .FirstOrDefault(),
                    Color =
                        context.Colors.OrderBy(o => o.Id)
                            .Skip(new Random().Next(context.Colors.Count()))
                            .FirstOrDefault(),
                    StandardCost = _prices.Skip(new Random().Next(_prices.Count())).FirstOrDefault(),
                    ListPrice = _prices.Skip(new Random().Next(_prices.Count())).FirstOrDefault(),
                   
                    Size = _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault() + "x"
                    + _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault() + "x"
                    + _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault()
                    ,
                    Packaging = _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault() + "x"
                    + _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault() + "x"
                    + _sizes.Skip(new Random().Next(_sizes.Count())).FirstOrDefault(),
                    PCSSE = _pcsse.Skip(new Random().Next(_pcsse.Count())).FirstOrDefault(),
                    CBM = _cbm.Skip(new Random().Next(_pcsse.Count())).FirstOrDefault(),
                    CMW = _cm.Skip(new Random().Next(_cm.Count())).FirstOrDefault(),
                    CMD = _cm.Skip(new Random().Next(_cm.Count())).FirstOrDefault(),
                    CMH = _cm.Skip(new Random().Next(_cm.Count())).FirstOrDefault(),

                    Description = _description.Skip(new Random().Next(_pcsse.Count())).FirstOrDefault(),
                    LastModifiedDate = DateTime.Now

                };

                context.Products.Add(prod);

                var ran = new Random();

                for (var j = 0; j < 5; j++)
                {
                    var num = ran.Next(1, 11);
                    context.ProductImages.Add(new ProductImage
                    {
                        Guid = Guid.NewGuid(),
                        ProductId = prod.Id,
                        ImageFileUrl = "img" + Convert.ToString(num) + ".jpg",
                        LastModifiedDate = DateTime.Now
                    });
                }

                context.SaveChanges();
            }
        }

        private static void AddCategories(KenrapidDbContext context)
        {
            context.Categories.Add(new Category
            {
                Name = "Jewelry",
                Description = "Jewelry Jewelry Jewelry",
                LastModifiedDate = DateTime.Now
            });

            context.Categories.Add(new Category
            {
                Name = "Home Décor",
                Description = "Home Décor Home Décor Home Décor",
                LastModifiedDate = DateTime.Now
            });

            context.Categories.Add(new Category
            {
                Name = "Furniture",
                Description = "Furniture Furniture Furniture",
                LastModifiedDate = DateTime.Now
            });
            context.Categories.Add(new Category
            {
                Name = "Yard & Outdoors",
                Description = "Yard & Outdoors Yard & Outdoors Yard & Outdoors",
                LastModifiedDate = DateTime.Now
            });
            context.Categories.Add(new Category
            {
                Name = "Arts & Events",
                Description = "Arts & Events Arts & Events Arts & Events",
                LastModifiedDate = DateTime.Now
            });
        }

        private static void AddMaterials(KenrapidDbContext context)
        {
            context.Materials.Add(new Material
            {
                Name = "Animal Skin",
                Description = "Animal Skin Animal Skin Animal Skin",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Athema Ruins",
                Description = "Athema Ruins Athema Ruins Athema Ruins",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Bird Wing",
                Description = "Bird Wing Bird Wing Bird Wing",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Bird Wing",
                Description = "Bird Wing Bird Wing Bird Wing"
            });

            context.Materials.Add(new Material
            {
                Name = "Black Fur",
                Description = "Black Fur Black Fur Black Fur",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Hard Dragon Skin",
                Description = "Hard Dragon Skin Hard Dragon Skin Hard Dragon Skin",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Icule Water",
                Description = "Icule Water Icule Water Icule Water",
                LastModifiedDate = DateTime.Now
            });

            context.Materials.Add(new Material
            {
                Name = "Huge Leaf",
                Description = "Huge Leaf Huge Leaf Huge Leaf",
                LastModifiedDate = DateTime.Now
            });
        }

        private static void AddVendors(KenrapidDbContext context)
        {
            context.Vendors.Add(new Vendor
            {
                Name = "Vendor 1",
                Description = "Vendor 1 description",
                HomePhone = "".RandomName(15, false),
                HomeAddress = "".RandomName(200, false),
                HomeEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                WorkPhone = "".RandomName(15, false),
                WorkAddress = "".RandomName(200, false),
                WorkEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                LastModifiedDate = DateTime.Now
            });

            context.Vendors.Add(new Vendor
            {
                Name = "Vendor 2",
                Description = "Vendor 2 description",
                HomePhone = "".RandomName(15, false),
                HomeAddress = "".RandomName(200, false),
                HomeEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                WorkPhone = "".RandomName(15, false),
                WorkAddress = "".RandomName(200, false),
                WorkEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                LastModifiedDate = DateTime.Now
            });

            context.Vendors.Add(new Vendor
            {
                Name = "Vendor 3",
                Description = "Vendor 3 description",
                HomePhone = "".RandomName(15, false),
                HomeAddress = "".RandomName(200, false),
                HomeEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                WorkPhone = "".RandomName(15, false),
                WorkAddress = "".RandomName(200, false),
                WorkEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                LastModifiedDate = DateTime.Now
            });

            for (var i = 0; i < 100; i++)
            {
                context.Vendors.Add(new Vendor
                {
                    Name = "".RandomName(20, false),
                    Description = "".RandomName(1000, false),
                    HomePhone = "".RandomName(15, false),
                    HomeAddress = "".RandomName(200, false),
                    HomeEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                    WorkPhone = "".RandomName(15, false),
                    WorkAddress = "".RandomName(200, false),
                    WorkEmail = ("".RandomName(20, false) + "@" + "".RandomName(5, false) + ".com").ToLower(),
                    LastModifiedDate = DateTime.Now
                });
            }
        }

        private static void AddColors(KenrapidDbContext context)
        {
            context.Colors.Add(new Color
            {
                Name = "Blue",
                Description = "Blue",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Brown",
                Description = "Brown",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Cyan",
                Description = "Cyan",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Gold",
                Description = "Gold",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Gray",
                Description = "Gray",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Green",
                Description = "Green",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Magenta",
                Description = "Magenta",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Navy",
                Description = "Navy",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Orange",
                Description = "Orange",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Pink",
                Description = "Pink",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Red",
                Description = "Red",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Silver",
                Description = "Silver",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Violet",
                Description = "Violet",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "White",
                Description = "White",
                LastModifiedDate = DateTime.Now
            });

            context.Colors.Add(new Color
            {
                Name = "Yellow",
                Description = "Yellow",
                LastModifiedDate = DateTime.Now
            });
        }

        private static void AddTerminatedCustomers(KenrapidDbContext context)
        {
            context.Customers.Add(new Customer
            {
                Name = "Arlo Seymour",
                HomeEmail = "Arlo@home.com",
                WorkEmail = "Arlo@work.com",
                WorkAddress = "123 Arlo Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Seymour Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-90),
                TerminationDate = DateTime.Today.ToStartOfMonth().AddDays(5),
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Porter Jakeman",
                HomeEmail = "Porter@home.com",
                WorkEmail = "Porter@work.com",
                WorkAddress = "123 Porter Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Jakeman Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-75),
                TerminationDate = DateTime.Today.ToStartOfMonth().AddDays(10),
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Edwyn Perry",
                HomeEmail = "Edwyn@home.com",
                WorkEmail = "Edwyn@work.com",
                WorkAddress = "123 Edwyn Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Perry Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-45),
                TerminationDate = DateTime.Today.ToStartOfMonth().AddDays(15),
                LastModifiedDate = DateTime.Now
            });
        }

        private static void AddExistingCustomers(KenrapidDbContext context)
        {
            context.Customers.Add(new Customer
            {
                Name = "Gosse Greene",
                HomeEmail = "Gosse@home.com",
                WorkEmail = "Gosse@work.com",
                WorkAddress = "123 Gosse Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Greene Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-20),
                Risks = new List<Risk>()
                    {
                        new Risk{Title = "Considering vendor switch", Description = "His contract is expiring next month, and he's evaluating other vendors.  He likes the services we provide, but feels he is paying too much."}
                    },
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Warwick Rye",
                HomeEmail = "Warwick@home.com",
                WorkEmail = "Warwick@work.com",
                WorkAddress = "123 Warwick Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Rye Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-15),
                Opportunities = new List<Opportunity>
                    {
                        new Opportunity{Title = "Expanding business", Description = "Warwick's business is booming.  He's considering acquiring a competitor.  If that happens, he'll need a *lot* of custom development to integrate the two systems."}
                    },
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Odell Dennel",
                HomeEmail = "Odell@home.com",
                WorkEmail = "Odell@work.com",
                WorkAddress = "123 Odell Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Dennel Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(-10),
                Risks = new List<Risk>
                    {
                        new Risk{Title = "Customer may not pay", Description = "Odell is not pleased with the solution we developed.  He has threatened to stop all future payments, including outstanding invoices for work that has already been performed."}
                    },
                LastModifiedDate = DateTime.Now
            });
        }

        private static void AddNewCustomers(KenrapidDbContext context)
        {
            context.Customers.Add(new Customer
            {
                Name = "John Doe",
                HomeEmail = "john@home.com",
                WorkEmail = "john@work.com",
                WorkAddress = "123 Main Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Second Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth(),
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Roy Irvine",
                HomeEmail = "roy@home.com",
                WorkEmail = "roy@work.com",
                WorkAddress = "123 Roy Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Irvine Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(5),
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Vere Rowland",
                HomeEmail = "vere@home.com",
                WorkEmail = "vere@work.com",
                WorkAddress = "123 Vere Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Roland Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(10),
                LastModifiedDate = DateTime.Now
            });

            context.Customers.Add(new Customer
            {
                Name = "Zack Beasley",
                HomeEmail = "zack@home.com",
                WorkEmail = "zack@work.com",
                WorkAddress = "123 Zack Street\r\nSuite B\r\nNew York, NY 55555",
                HomeAddress = "321 Beasley Street\r\nApt 1205\r\nNew York, NY 55555",
                HomePhone = "(555) 123-4555",
                WorkPhone = "(555) 321-5444",
                CreatedDate = DateTime.Today.ToStartOfMonth().AddDays(15),
                Opportunities = new List<Opportunity>
                    {
                        new Opportunity{Title = "Interested in on-site support", Description = "Zack likes the solution we developed for his business.  He's interested in our on-site support services, too."}
                    },
                LastModifiedDate = DateTime.Now
            });
        }
    }
}