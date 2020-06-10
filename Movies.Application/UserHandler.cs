using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    public class UserHandler
    {
        private readonly IApplicationDbContext db;

        public UserHandler(IApplicationDbContext db) => this.db = db;

        public async Task<User> GetUserByApiCredentials(string apiKey, string apiSecret)
        {
            return await db.Users.SingleOrDefaultAsync(u => u.ApiKey == apiKey && u.ApiSecret == apiSecret);
        }
    }
}
