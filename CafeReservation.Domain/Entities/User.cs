using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CafeReservation.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        private User()
        {
        }

        public static User Create(String firstName, String lastName, String email, String passwordHash, RoleType roleType = RoleType.User)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash
            };

            user.UserRoles.Add(new UserRole { RoleId = (int)roleType });

            return user;

        }

        public void AddRole(RoleType roleType)
        {
            if (!UserRoles.Any(r => r.RoleId == (int)roleType))
            {
                UserRoles.Add(new UserRole { RoleId = (int)roleType });
            }

        }

        public void RemoveRole(RoleType roleType)
        {
            if(UserRoles == null) return;

            var roleToRemove = UserRoles.FirstOrDefault(r => r.RoleId == (int)roleType);

            if (roleToRemove != null)
            {
                if (UserRoles.Count <= 1)
                {
                    throw new InvalidOperationException("System users should have at least 1 role !");
                }

                UserRoles.Remove(roleToRemove);
            }
        }
    }
}
