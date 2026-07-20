using CafeReservation.Application.Common.Interfaces.Authentication;
using CafeReservation.Application.Common.Interfaces.Persistence;
using CafeReservation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {

        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool isUnique = await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken);
            if (!isUnique)
            {
                throw new Exception("The email you entered is not unique !");
            }

            var hashedPassword = _passwordHasher.Hash(request.Password);

            User user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                hashedPassword);

            await _userRepository.AddAsync(user, cancellationToken);

            return user.Id;
        }


    }

}
