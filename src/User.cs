using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.src
{
    public class User
    {
        private string? _name;
        private UserRole _role;
        private string? _password;

        public string Name
        {
            get { if (_name is not null) { return _name; } else { return "Null"; } }
            set { _name = value; }
        }
        public string Password
        {
            get { if (_password is not null) { return _password; } else { return "Null"; } }
            set { _password = value; }
        }

        public UserRole Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }

        public UserReadDTO ConvertToRead()
        {
            return new UserReadDTO
            {
                Name = _name,
                Role = _role

            };
        }
    }

    public class UserReadDTO
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }

    }
}

