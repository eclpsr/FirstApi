using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string SayHello()
        {
            return "Привет, ASP.NET Core!";
        }

        [HttpGet("{name}")]
        public string SayHelloTo(string name)
        {
            return $"Привет, {name}!";
        }
    }
}