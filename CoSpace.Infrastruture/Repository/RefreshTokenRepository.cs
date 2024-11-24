using Azure.Core;
using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture.Repository
{
    public class RefreshTokenRepository(ApplicationDbContext dbContext, RepositoryBase<Role> repositoryBase, ICurrentUserService currentUserService) : IRefreshTokenRepository
    {

        public async Task<RefreshToken> GetRefreshTokenAsync(LogoutRequest request)
        {
            var refreshToken = await dbContext.RefreshToken.SingleOrDefault(rt => rt.Token == request.RefreshToken);

            if (refreshToken == null)
            {
                return null;
            }
            return refreshToken;
        }
    }
}
