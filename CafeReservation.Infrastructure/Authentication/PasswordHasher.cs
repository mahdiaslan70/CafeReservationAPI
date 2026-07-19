using System;
using System.Collections.Generic;
using System.Text;
using CafeReservation.Application.Common.Interfaces;
using CafeReservation.Application.Common.Interfaces.Authentication;
using BC = BCrypt.Net.BCrypt;

namespace CafeReservation.Infrastructure.Authentication
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
