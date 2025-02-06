using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Taller.CodeChallengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Login")]
        public async IActionResult Post([FromBody]string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";
            
            using (var connection = new SqlConnection("[ConnectionString]"))
            {
                var p1 = new SqlParameter("@Username", username);
                var command = new SqlCommand(query, connection);

                command.Parameters.Add(p1);

                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return NotFound("User not found.");
                }

                return Ok($"Hello, {username}");
            }
        }
    }
}
