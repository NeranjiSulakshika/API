using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DataAccessLayer;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public AuthController(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                response = await _authDL.SignUp(request);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                response = await _authDL.SignIn(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("ReadCitizenInformation")]
        public async Task<IActionResult> ReadCitizenInformation()
        {
            ReadCitizenInformationResponse response = null;
            try
            {
                response = await _authDL.ReadCitizenInformation();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateCitizenInformation")]
        public async Task<IActionResult> UpdateCitizenInformation(UpdateCitizenInformationRequest request)
        {
            UpdateCitizenInformationResponse response = null;
            try
            {
                response = await _authDL.UpdateCitizenInformationRequest(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public Task UploadFile(IFormFile file)
        {
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<ActionResult> CreateComplaint(CreateComplaintRequest request)
        {
            CreateComplaintResponse response = new CreateComplaintResponse();
            try
            {
                response = await _authDL.CreateComplaintInformation(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateComplaintInformation")]
        public async Task<IActionResult> UpdateComplaintInformation(UpdateComplaintInformationRequest request)
        {
            UpdateComplaintInformationResponse response = null;
            try
            {
                response = await _authDL.UpdateComplaintInformationRequest(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }
    }
}
