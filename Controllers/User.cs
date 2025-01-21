using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using smartclinic.Services;
namespace smartclinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class User : Controller
    {
        UserService _userService;
        public User(DBContext context) : base(context)
        {
            _userService = new UserService(context);

        }

        [HttpGet("/ExportUser")]
        public IActionResult ExportUser()
        {
            var users = _userService.GetUser();
            return File(_userService.UserToExcel(users).ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Users.xlsx");
        }
  
    }

}