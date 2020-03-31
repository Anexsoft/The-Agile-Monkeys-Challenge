using Microsoft.AspNetCore.Mvc;

namespace CRM.Client.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Running ..";
        }
    }
}
