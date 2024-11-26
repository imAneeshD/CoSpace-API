using CoSpace.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
        Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken);
        Task<bool> DeleteRefreshToken();
    }
}
