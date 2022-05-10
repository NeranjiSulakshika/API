using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DataAccessLayer;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public static IWebHostEnvironment _env;

        public AuthController(IAuthDL authDL, IWebHostEnvironment env)
        {
            _authDL = authDL;
            _env = env;
        }


        /// <summary>
        /// Admin Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetAdmin([FromBody]GetAdmin request)
        {
            ReadAdminResponse response = null;

            try
            {
                response = await _authDL.GetAdmin(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }


        /// <summary>
        /// Sign Up
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequest request)
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


        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
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


        /// <summary>
        /// Get Citizen Information List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
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


        /// <summary>
        /// Get Citizen Information By NIC
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{NIC}")]
        public async Task<IActionResult> CitizenInformationByNIC([FromHeader] CitizenInformationByNICRequest request)
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


        /// <summary>
        /// Get Citizen Information By Qualification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{Qualification}")]
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


        /// <summary>
        /// Update Citizen's Qualification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("UpdateQualification/{NIC}")]
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


        /// <summary>
        /// Upload Citizen's BirthCertificate, CV, Passport
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> UploadCitizenDocuments(IFormFile file)
        {
            string filename = "";

            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = "File_" + DateTime.Now.TimeOfDay.Milliseconds + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return filename;
        }


        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("Delete/{UserId}")]
        public async Task<IActionResult> DeleteCitizen([FromHeader] DeleteCitizenRequest request)
        {
            DeleteCitizenResponse response = null;
            try
            {
                response = await _authDL.DeleteCitizen(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }


        /// <summary>
        /// Get Complaints List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
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


        /// <summary>
        /// Ceate a Complaint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintRequest request)
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


        /// <summary>
        /// Reply for Complaints
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("Update/{ComplaintId}")]
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
