using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}
