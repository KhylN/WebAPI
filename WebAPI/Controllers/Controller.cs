using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeVaultSecure.Services;

namespace SafeVaultSecure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SafeVaultController : ControllerBase
    {
        private readonly DatabaseService _db;

        public SafeVaultController(DatabaseService db)
        {
            _db = db;
        }

        // ✅ Public endpoint with input validation
        [HttpGet("search")]
        public IActionResult SearchUser([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Invalid username.");

            var user = _db.GetUser(username);
            return user != null ? Ok(user) : NotFound("User not found.");
        }

        // ✅ Protected endpoint (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/data")]
        public IActionResult GetAdminData()
        {
            return Ok("Sensitive admin data accessed securely!");
        }
    }
}
