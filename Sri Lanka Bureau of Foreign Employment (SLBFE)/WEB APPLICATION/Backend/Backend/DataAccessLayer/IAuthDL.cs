using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccessLayer
{
    public interface IAuthDL
    {
        /// <summary>
        /// Admin Login
        /// </summary>
        public Task<ReadAdminResponse> GetAdmin(GetAdmin request);

        /// <summary>
        /// SignUp
        /// </summary>
        public Task<SignUpResponse> SignUp(SignUpRequest request);

        /// <summary>
        /// SignIn
        /// </summary>
        public Task<SignInResponse> SignIn(SignInRequest request);

        /// <summary>
        /// Get Citizen Information
        /// </summary>
        public Task<ReadCitizenInformationResponse> ReadCitizenInformation();

        /// <summary>
        /// Get Citizen Information By NIC
        /// </summary>
        public Task<CitizenInformationByNICResponse> GetCitizenInformationByNIC(CitizenInformationByNICRequest request);

        /// <summary>
        /// Get Citizen Information By Qualification
        /// </summary>
        public Task<CitizenInformationByQualificationResponse> GetCitizenInformationByQualification(CitizenInformationByQualificationRequest request);

        /// <summary>
        /// Update Citizen Information
        /// </summary>
        //public Task<UpdateCitizenInformationResponse> UpdateCitizenInformationRequest(UpdateCitizenInformationRequest request);

        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        public Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest request);

        /// <summary>
        /// Get Complaints List
        /// </summary>
        public Task<ReadComplaintsResponse> ReadComplaints();

        /// <summary>
        /// Create a Complaint 
        /// </summary>
        public Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request);

        /// <summary>
        /// Reply for Complaints 
        /// </summary>
        public Task<UpdateComplaintInformationResponse> UpdateComplaintInformationRequest(UpdateComplaintInformationRequest request);
    }
}
