using Microsoft.AspNetCore.Mvc;
using prj.Service.Interface;
using prj.Model;
using prj.Model.DTO;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService service;

        public UserProfileController(IUserProfileService service)
        {
            this.service = service;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var profiles = await service.GetUserProfile();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("All/reg")]
        public async Task<IActionResult> Add(UserProfile profile)
        {
            try
            {
                var addedProfile = await service.AddUserProfile(profile);
                return Ok(addedProfile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserProfile profile)
        {
            try
            {
                var updatedProfile = await service.UpdateUserProfile(profile);
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> DeleteProfile(int customerId)
        {
            try
            {
                var deletedProfile = await service.DeleteUserProfile(customerId);
                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("filter/{CustomerId}")]
        public async Task<IActionResult> GetUserProfileById(int customerId)
        {
            try
            {
                var profile = await service.GetUserProfileById(customerId);
                if (profile == null)
                {
                    return NotFound();
                }
                return Ok(profile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("login/{customerId}")]
        public async Task<IActionResult> GetUserLoginById(int customerId)
        {
            try
            {
                var loginDto = await service.GetUserProfileById(customerId);
                if (loginDto == null)
                {
                    return NotFound();
                }
                return Ok(loginDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("profile_dto/{customerId}")]
        public async Task<IActionResult> GetUserProfilesById(int customerId)
        {
            try
            {
                var profileDto = await service.GetUserProfileById(customerId);
                if (profileDto == null)
                {
                    return NotFound();
                }
                return Ok(profileDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("login_dto/{customerId}")]
        public async Task<IActionResult> PutLogin(int customerId, UserLoginDTO loginDTO)
        {
            try
            {
                var updatedLoginDto = await service.AddUserLogin(customerId, loginDTO);
                if (updatedLoginDto == null)
                {
                    return NotFound();
                }
                return Ok(updatedLoginDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("profile_dto/{customerId}")]
        public async Task<IActionResult> PutPro(int customerId, UserProfileDTO profileDTO)
        {
            try
            {
                var updatedProfileDto = await service.AddUserProfiles(profileDTO, customerId);
                if (updatedProfileDto == null)
                {
                    return NotFound();
                }
                return Ok(updatedProfileDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("ChangePass/{customerId}")]
        public async Task<ActionResult<HashPasswordDTO>> ChangePassword(int customerId, ChangePasswordDTO model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
                {
                    return BadRequest("OldPassword and NewPassword must not be null or empty.");
                }
                var doctorDto = await service.ChangeUserPassword(customerId, model.OldPassword, model.NewPassword);

                if (doctorDto == null)
                {
                    return BadRequest("Invalid old password.");
                }

                return Ok(doctorDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("updateimage/{customerId}")]
        public async Task<IActionResult> UpdateImage(int customerId, [FromForm] ChangeImageDTO changeImageDTO)
        {
            try
            {
                ChangeImageDTO updatedImage = await service.UpdateUserImage(customerId, changeImageDTO);
                return Ok(updatedImage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, $"An error occurred during image update: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserRegisterDTO loginDTO = await service.UserRegister(registerDTO);
                return Ok(loginDTO);
            }
            catch (ApplicationException ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("view_image/{id}")]
        public async Task<IActionResult> ViewImage(int customerId)
        {
            try
            {
                var profileDto = await service.ViewUserImage(customerId);

                if (profileDto == null)
                {
                    return NotFound();
                }

                return Ok(profileDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("login/Authenticator")]
        public async Task<IActionResult> UserLogin(AuthenticatorDTO userLoginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = await service.UserLogin(userLoginDTO);

                    if (!string.IsNullOrEmpty(token))
                    {
                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}

