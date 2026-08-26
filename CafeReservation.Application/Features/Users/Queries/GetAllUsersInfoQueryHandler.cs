using CafeReservation.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Queries
{

    public record GetAllUsersInfoQuery() : IRequest<IEnumerable<UserInfoDTO>>;
    public class GetAllUsersInfoQueryHandler
        : IRequestHandler<GetAllUsersInfoQuery, IEnumerable<UserInfoDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersInfoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserInfoDTO>> Handle(GetAllUsersInfoQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersInfoAsync(cancellationToken);
        }
    }
}
