using GasWebMap.Services.Responses;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/login", "POST")]
    public class UserLogin
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }

    [Route("/login", "DELETE")]
    public class DeleteUser : DeleteOperation
    {
    }
}