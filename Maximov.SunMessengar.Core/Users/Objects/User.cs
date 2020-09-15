using Maximov.SunMessenger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Users.Objects
{
    public class User:IEntity
    {

        protected User()
        {
        }

        public User(string name, string login, string password) : this()
        {
            this.Id = new Guid();
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Login = string.IsNullOrWhiteSpace(login) ? throw new ArgumentNullException(nameof(login)) : login;
            Password = string.IsNullOrWhiteSpace(password) ? throw new ArgumentNullException(nameof(password)) : password;
            ActiveFrom = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public DateTime ActiveFrom { get; protected set; }

        public void ChangeName(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
        }

        public void ChangePassword(string password)
        {
            Password = string.IsNullOrWhiteSpace(password) ? throw new ArgumentNullException(nameof(password)) : password;
        }
    }
}
