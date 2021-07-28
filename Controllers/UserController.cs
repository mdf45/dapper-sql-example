using Microsoft.AspNetCore.Mvc;
using MsSQL.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MsSQL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserData _data;

        public UserController(UserData data)
        {
            _data = data;
        }

        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            return Ok(await _data.LoadData<Models.UserModel, object>("SELECT [user_id] AS [UserId], [user_name] AS [Username] FROM [mytable];", null, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
        {
            if (!Request.RouteValues.ContainsKey("id")) return BadRequest(new { message = "Error, please set an id" });

            var ver = int.TryParse(Request.RouteValues["id"].ToString(), out int id);

            if (!ver) return BadRequest(new { message = "Incorrect id" });

            return Ok(await _data.LoadData<Models.UserModel, object>("SELECT [user_id] AS [UserId], [user_name] AS [Username] FROM [mytable] WHERE [user_id] = @id;",
                new { id }, cancellationToken));
        }
    }
}
