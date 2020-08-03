using CoreMVCStudy.Web.Data.Entities;
using CoreMVCStudy.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCStudy.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Fernando",
                    LastName = "Figueiras",
                    Email = "fjfigdev@gmail.com",
                    UserName = "fjfigdev@gmail.com"
                };


                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }


            if (!_context.Products.Any())
            {
                this.AddProduct("Produto1", user);
                this.AddProduct("Produto2", user);
                this.AddProduct("Produto3", user);
                this.AddProduct("Produto4", user);
                this.AddProduct("Produto5", user);
                this.AddProduct("Produto6", user);

                await _context.SaveChangesAsync();
            }


        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(100),
                IsAvailable = true,
                Stock = _random.Next(1000),
                User = user
            });
        }
    }
}
