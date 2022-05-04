using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    public interface ICrudAppliactionSL
    {
        /// <summary>
        /// Admin Login
        /// </summary>
        public Task<ReadAdminResponse> GetAdmin(GetAdmin request);

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
        //public Task<UpdateCitizenInformationResponse> UpdateCitizenInformation(UpdateCitizenInformationRequest request);

        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        public Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest response);

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
        public Task<UpdateComplaintInformationResponse> UpdateComplaintInformation(UpdateComplaintInformationRequest request);
    }
}
