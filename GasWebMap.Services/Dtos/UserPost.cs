using System;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Responses
{
    [Route("/user", "POST,PUT")]
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Guid DeaprtID { get; set; }

        public string Telephone { get; set; }
        public bool Sms { get; set; }
    }

    [Route("/user", "GET")]
    public class UserGet
    {
        public Guid DepartID { get; set; }
    }
    
    [Route("/user2", "DELETE")]
    [Serializable]
    public class UserDelete:DeleteOperation<Guid>
    {
    }
}