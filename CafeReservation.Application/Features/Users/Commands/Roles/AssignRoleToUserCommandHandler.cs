using CafeReservation.Application.Common.Interfaces.Persistence;
using CafeReservation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Commands.Roles
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
    {

        private readonly IUserRepository _userRepository;
        public AssignRoleToUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new Exception("There is no User with this Email !");
            }

            user.AddRole(request.RoleToAssign);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

