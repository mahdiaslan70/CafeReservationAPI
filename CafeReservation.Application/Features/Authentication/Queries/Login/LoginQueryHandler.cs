using CafeReservation.Application.Common.Interfaces.Authentication;
using CafeReservation.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.email, cancellationToken);

            if (user == null)
                throw new Exception("Invalid Email or Password !");

            bool isPasswordValid = _passwordHasher.Verify(request.password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Invalid Email or Password !");

            var token = await _jwtProvider.Generate(user);

            return token;
        }

    }
}
