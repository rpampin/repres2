﻿using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.Account;
using Repres.Application.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Repres.Server.Controllers.Identity
{
    [Authorize]
    [Route("api/identity/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICurrentUserService _currentUser;

        public AccountController(IAccountService accountService, ICurrentUserService currentUser)
        {
            _accountService = accountService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPut(nameof(UpdateProfile))]
        public async Task<ActionResult> UpdateProfile(UpdateProfileRequest model)
        {
            var response = await _accountService.UpdateProfileAsync(model, _currentUser.UserId);
            return Ok(response);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPut(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest model)
        {
            var response = await _accountService.ChangePasswordAsync(model, _currentUser.UserId);
            return Ok(response);
        }

        /// <summary>
        /// Get Profile picture by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("profile-picture/{userId}")]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetProfilePictureAsync(string userId)
        {
            return Ok(await _accountService.GetProfilePictureAsync(userId));
        }

        /// <summary>
        /// Get Profile time zone by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("time-zone/{userId}")]
        //[ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetProfileTimeZoneAsync(string userId)
        {
            return Ok(await _accountService.GetProfileUtcMinutesAsync(userId));
        }

        /// <summary>
        /// Get Profile language by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("language/{userId}")]
        //[ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetProfileLanguageAsync(string userId)
        {
            return Ok(await _accountService.GetProfileLanguageAsync(userId));
        }

        /// <summary>
        /// Update Profile Picture
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("profile-picture/{userId}")]
        public async Task<IActionResult> UpdateProfilePictureAsync(UpdateProfilePictureRequest request)
        {
            return Ok(await _accountService.UpdateProfilePictureAsync(request, _currentUser.UserId));
        }

        /// <summary>
        /// Profile has Sheet
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("has-sheet/{userId}")]
        public async Task<IActionResult> GetProfileHasSheetAsync(string userId)
        {
            return Ok(await _accountService.GetProfileHasSheetAsync(userId));
        }
    }
}