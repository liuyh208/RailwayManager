using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/menu", "GET")]
    public class MenuGet
    {
        public string From { get; set; }
    }

    [Route("/menutree/{RoleId}", "GET"), Route("/menutree", "GET")]
    public class MenuTree
    {
        public string RoleId { get; set; }
    }
}