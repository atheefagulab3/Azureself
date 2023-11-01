using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using prj.Context;
using prj.Model.DTO;
using prj.Model.Helpers;
using System.Text;

namespace prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly ProfileContext _profileContext;

        public ForgotPasswordController(ProfileContext context)
        {
            _profileContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            string? email_id = request?.Email;

            if (string.IsNullOrEmpty(email_id))
            {
                return BadRequest("Please provide a valid email address.");
            }

            var user = await _profileContext.profiles.FirstOrDefaultAsync(u => u.EmailId == email_id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            string randomPassword = GenerateRandomPassword();
            string passwordu = PasswordHasher.HashPassword(randomPassword);

            user.Password = passwordu;
            await _profileContext.SaveChangesAsync();

            return Ok("Password reset successful. Check your email for the new password." + randomPassword);
        }

        private string GenerateRandomPassword()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int passwordLength = 10;

            Random random = new Random();
            var password = new StringBuilder(passwordLength);
            for (int i = 0; i < passwordLength; i++)
            {
                password.Append(allowedChars[random.Next(allowedChars.Length)]);
            }

            return password.ToString();
        }
    }

    public class ForgotPasswordRequest
    {
        public string? Email { get; set; }
    }
}



