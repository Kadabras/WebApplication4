using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using WebApplication4.EfStuff.DbModel;
using WebApplication4.EfStuff.Repositories;

namespace WebApplication4.EfStuff
{
    public static class SeedExtention
    {
        public const string DefaultAdminName = "admin";

        public static void Seed(WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                SeedRoles(scope);
                SeedUsers(scope);
            }
        }

        private static void SeedRoles(IServiceScope scope)
        {
            var roleRepository = scope.ServiceProvider.GetService<RoleRepository>();
            var roles = roleRepository.GetAll();

            if (!roles.Any())
            {
                var adminRole = new Role()
                {
                    Name = Role.Admin,
                    RoleUsers = new List<User>()
                };
                roleRepository.Save(adminRole);

                var userRole = new Role()
                {
                    Name = Role.User,
                    RoleUsers = new List<User>()
                };
                roleRepository.Save(userRole);

                var supportRole = new Role()
                {
                    Name = Role.Support,
                    RoleUsers = new List<User>()
                };
                roleRepository.Save(supportRole);

                var superAdminRole = new Role()
                {
                    Name = Role.SuperAdmin,
                    RoleUsers = new List<User>()
                };
                roleRepository.Save(superAdminRole);

            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetService<UserRepository>();
            var getAllRoles = scope.ServiceProvider.GetService<RoleRepository>().GetAll();
            var admin = userRepository.GetUserByName(DefaultAdminName);
            var randomNames = new List<string> { "Silaterr", "Yestio", "Whindali", "Shlongi", "Beroldek" };
            var random = new Random();

            if (admin == null)
            {
                var roles = new List<Role>();
                roles.Add(getAllRoles.First(x => x.Name == Role.Admin));

                admin = new User()
                {
                    Name = DefaultAdminName,
                    Age = 32,
                    Email = "admin@admin.com",
                    Roles = roles
                };

                userRepository.Save(admin);
            }

            if (userRepository.Count() < 10)
            {
                for (int i = 0; i < 100; i++)
                {
                    string randomName;
                    do
                    {
                        randomName = randomNames[random.Next(randomNames.Count())] + random.Next(1000);
                    }
                    while (userRepository.GetUserByName(randomName) != null);

                    string randomEmail;
                    do
                    {
                        randomEmail = randomNames[random.Next(randomNames.Count())] + random.Next(1000) + "@user.com";
                    }
                    while (userRepository.GetUserByEmail(randomEmail) != null);

                    var roles = new List<Role>();
                    var rnd = random.Next(getAllRoles.Count()) + 1;
                    roles.Add(getAllRoles.First(x => x.Id == rnd));

                    var randomUser = new User()
                    {
                        Name = randomName,
                        Age = random.Next(18, 60),
                        Email = randomEmail,
                        Roles = roles
                    };

                    userRepository.Save(randomUser);
                }
            }
        }
    }
}
