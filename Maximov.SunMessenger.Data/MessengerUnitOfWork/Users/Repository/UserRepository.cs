using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;

namespace Maximov.SunMessenger.Data.Repositories
{
    public class UserRepository:IWriteRepository<User>
    {
        private readonly SunMessengerContext db;

        public UserRepository(SunMessengerContext db)
        {
            this.db = db;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User FindById(Guid id)
        {
            return db.Users.Find(id);
        }

        public IQueryable<User> GetAll()
        {
            return db.Users;
        }

        public IEnumerable<User> GetBySpecification(ISpecification<User> specification)
        {
            return specification.Apply(db.Users).ToArray();
        }

        public void Update(User user)
        {
            db.Users.Update(user);
        }

        IEnumerable<User> IReadRepository<User>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
