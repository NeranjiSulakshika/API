using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    public interface ICrudAppliactionSL
    {
        public Task<ReadAdminResponse> GetAdmin();
        public Task<ReadCitizenInformationResponse> ReadCitizenInformation();
        public Task<CitizenInformationByNICResponse> GetCitizenInformationByNIC(CitizenInformationByNICRequest request);
        public Task<CitizenInformationByQualificationResponse> GetCitizenInformationByQualification(CitizenInformationByQualificationRequest request);
        public Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request);
        public Task<UpdateCitizenInformationResponse> UpdateCitizenInformation(UpdateCitizenInformationRequest request);
        public Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest response);
        public Task<UpdateComplaintInformationResponse> UpdateComplaintInformation(UpdateComplaintInformationRequest request);
        public Task<ReadComplaintsResponse> ReadComplaints();
    }
}
