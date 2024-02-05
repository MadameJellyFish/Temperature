using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service.Contract
{
    public interface IAuthService
    {
        Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(string userName);

        Task<(string AccessToken, string RefreshToken)> RefreshAsync(string token);
    }
}
