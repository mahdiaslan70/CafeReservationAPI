using CafeReservation.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Queries
{

    public record GetUserInfoQuery(string Email) : IRequest<UserInfoDTO>;
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserInfoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserInfoDTO> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserInfoByEmailAsync(query.Email, cancellationToken);

            if(result == null) { throw new KeyNotFoundException("The user was not found !"); }

            return result;
        }
    }
}
