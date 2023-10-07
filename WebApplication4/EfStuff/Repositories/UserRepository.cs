using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using WebApplication4.EfStuff.DbModel;

namespace WebApplication4.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private ILogger<UserRepository> _logger;

        private Random _random = new Random();

        public UserRepository(WebContext webContext,
            ILogger<UserRepository> logger) : base(webContext)
        {
            _logger = logger;
        }

        public User GetRandomUser() => GetAllQueryable().Skip(_random.Next(Count())).First();

        public User GetUserByName(string name)
        {
            return GetAllQueryable().FirstOrDefault(x => x.Name == name);
        }
        public User GetUserByEmail(string email)
        {
            return GetAllQueryable().FirstOrDefault(x => x.Email == email);
        }


        public User GetUserById(long Id)
        {
            return GetAllQueryable().SingleOrDefault(x => x.Id == Id);
        }

    }
}
