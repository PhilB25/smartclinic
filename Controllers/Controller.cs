using Microsoft.AspNetCore.Mvc;
using smartclinic.Services;

namespace smartclinic.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class Controller : ControllerBase
    {
        public readonly DBContext _context;

        public Controller(DBContext context)
        {
            _context = context;
        }

    }
}