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

        public async Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request)
        {

            return await _appliactionRL.CreateComplaintInformation(request);

        }

        public async Task<ReadCitizenInformationResponse> ReadCitizenInformation()
        {
            return await _appliactionRL.ReadCitizenInformation(); 
        }

        public async Task<UpdateCitizenInformationResponse> UpdateCitizenInformation(UpdateCitizenInformationRequest request)
        {
            return await _appliactionRL.UpdateCitizenInformationRequest(request);
        }

        public async Task<UpdateComplaintInformationResponse> UpdateComplaintInformation(UpdateComplaintInformationRequest request)
        {
            return await _appliactionRL.UpdateComplaintInformationRequest(request);
        }

        /*public async Task<DeleteInformationResponse> DeleteInformation(DeleteInformationRequest request)
        {
            return await _crudAppliactionRL.DeleteInformation(request);
        }

        public async Task<SearchInformationByIdResponse> SearchInformationById(SearchInformationByIdRequest request)
        {
            return await _crudAppliactionRL.SearchInformationById(request);
        }*/
    }
}
