using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Users.Queries
{
    public static class UserQuerableExtensions
    {
        public static User WithLogin(this IQueryable<User> users, string login)
            => users.Where(u => u.Login == login).FirstOrDefault();
    }
}
