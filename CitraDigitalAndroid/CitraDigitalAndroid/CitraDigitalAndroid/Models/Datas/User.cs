using System;
using System.Collections.Generic;
using System.Linq;

namespace CitraDigitalAndroid.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public bool Status { get; set; }

        public UserType UserType
        {
            get
            {
                if (UserRoles != null && UserRoles.Count() > 0)
                {
                    Role role = UserRoles.FirstOrDefault().Role;
                    return (UserType)Enum.Parse(typeof(UserType), role.Name);
                }

                return UserType.None;
            }
        }

        public string FullName
        {
            get
            {
                var fullName = $"{FirstName} {LastName}";
                return fullName.Trim();
            }
        }
    }
}