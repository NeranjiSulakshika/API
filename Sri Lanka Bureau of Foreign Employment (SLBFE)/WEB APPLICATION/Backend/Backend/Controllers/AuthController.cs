using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DataAccessLayer;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAdmin()
        {
            ReadAdminResponse response = null;
            try
            {
                response = await _authDL.GetAdmin();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
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

        [HttpGet]
        [Route("Get/{Email}, {Password}, {Affiliation}")]
        public async Task<ActionResult> SignIn([FromHeader] SignInRequest request)
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
        [Route("GetCitizenList")]
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

        [HttpGet]
        [Route("GetCitizenInformationByNIC/{NIC}")]
        public async Task<IActionResult> CitizenInformationByNIC([FromHeader]CitizenInformationByNICRequest request)
        {
            CitizenInformationByNICResponse response = null;
            try
            {
                response = await _authDL.GetCitizenInformationByNIC(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCitizenInformationByQualification/{Qualification}")]
        public async Task<IActionResult> CitizenInformationByQualification([FromHeader] CitizenInformationByQualificationRequest request)
        {
            CitizenInformationByQualificationResponse response = null;
            try
            {
                response = await _authDL.GetCitizenInformationByQualification(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateCitizen")]
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

        [HttpPut]
        public Task UploadFile(IFormFile file)
        {
            return Task.CompletedTask;
        }

        [HttpGet]
        [Route("GetComplaintsList")]
        public async Task<IActionResult> ReadComplaints()
        {
            ReadComplaintsResponse response = null;
            try
            {
                response = await _authDL.ReadComplaints();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("CreateComplaint")]
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
        [Route("ReplyComplaint")]
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
