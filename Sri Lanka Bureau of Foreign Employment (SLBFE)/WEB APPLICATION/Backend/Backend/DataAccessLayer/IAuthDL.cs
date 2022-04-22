using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccessLayer
{
    public interface IAuthDL
    {
        public Task<ReadAdminResponse> GetAdmin();
        public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<SignInResponse> SignIn(SignInRequest request);
        public Task<ReadCitizenInformationResponse> ReadCitizenInformation();
        public Task<CitizenInformationByNICResponse> GetCitizenInformationByNIC(CitizenInformationByNICRequest request);
        public Task<CitizenInformationByQualificationResponse> GetCitizenInformationByQualification(CitizenInformationByQualificationRequest request);
        public Task<UpdateCitizenInformationResponse> UpdateCitizenInformationRequest(UpdateCitizenInformationRequest request);
        public Task<ReadComplaintsResponse> ReadComplaints();
        public Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request);
        public Task<UpdateComplaintInformationResponse> UpdateComplaintInformationRequest(UpdateComplaintInformationRequest request);
    }
}
