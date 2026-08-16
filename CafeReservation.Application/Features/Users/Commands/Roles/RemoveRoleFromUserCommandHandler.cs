using CafeReservation.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Commands.Roles
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public RemoveRoleFromUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new Exception("There is no User with this email !");
            }

            user.RemoveRole(request.RoleToRemove);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
