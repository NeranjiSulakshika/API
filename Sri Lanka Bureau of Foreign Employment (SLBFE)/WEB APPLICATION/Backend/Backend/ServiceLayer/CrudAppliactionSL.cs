using Backend.Model;
using Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    public class CrudAppliactionSL : ICrudAppliactionSL
    {
        public readonly IAuthDL _appliactionRL;
        public CrudAppliactionSL(IAuthDL appliactionRL)
        {
            _appliactionRL = appliactionRL;
        }

        /// <summary>
        /// Admin Login
        /// </summary>
        public async Task<ReadAdminResponse> GetAdmin(GetAdmin request)
        {
            return await _appliactionRL.GetAdmin(request);
        }

        /// <summary>
        /// Get Citizen Information
        /// </summary>
        public async Task<ReadCitizenInformationResponse> ReadCitizenInformation()
        {
            return await _appliactionRL.ReadCitizenInformation();
        }

        /// <summary>
        /// Get Citizen Information By NIC
        /// </summary>
        public async Task<CitizenInformationByNICResponse> GetCitizenInformationByNIC(CitizenInformationByNICRequest request)
        {
            return await _appliactionRL.GetCitizenInformationByNIC(request);
        }

        /// <summary>
        /// Get Citizen Information By Qualification
        /// </summary>
        public async Task<CitizenInformationByQualificationResponse> GetCitizenInformationByQualification(CitizenInformationByQualificationRequest request)
        {
            return await _appliactionRL.GetCitizenInformationByQualification(request);
        }

        /// <summary>
        /// Update Citizen Information
        /// </summary>
        public async Task<UpdateCitizenInformationResponse> UpdateCitizenInformation(UpdateCitizenInformationRequest request)
        {
            return await _appliactionRL.UpdateCitizenInformationRequest(request);
        }

        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        public async Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest request)
        {
            return await _appliactionRL.DeleteCitizen(request);
        }

        /// <summary>
        /// Get Complaints List
        /// </summary>
        public async Task<ReadComplaintsResponse> ReadComplaints()
        {
            return await _appliactionRL.ReadComplaints();
        }

        /// <summary>
        /// Create a Complaint 
        /// </summary>
        public async Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request)
        {
            return await _appliactionRL.CreateComplaintInformation(request);
        }

        /// <summary>
        /// Reply for Complaints 
        /// </summary>
        public async Task<UpdateComplaintInformationResponse> UpdateComplaintInformation(UpdateComplaintInformationRequest request)
        {
            return await _appliactionRL.UpdateComplaintInformationRequest(request);
        }
    }
}
