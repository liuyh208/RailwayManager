using GasWebMap.Core;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services
{
    [Authenticate]
    public class AccountService : ServiceBase
    {
        private ILogger Log = LogFactory.GetLogger(typeof (AccountService));

        public ResponseResult Post(UserLogin user)
        {
            
            return ResponseResult.SuccessRes;
        }
    }
}