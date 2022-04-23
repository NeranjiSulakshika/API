using Microsoft.Extensions.Configuration;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Backend.DataAccessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        int ConnectionTimeOut = 180;

        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnectionString"]);
        }


        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReadAdminResponse> GetAdmin(GetAdmin request)
        {
            ReadAdminResponse response = new ReadAdminResponse();
            response.readAdmin = new List<GetAdmin>();
            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM slbfe.admin 
                                    WHERE UserName=@UserName AND Password=@PassWord;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);

                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.Message = "Login Successfull";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Login Failed";
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
            /*
            try
            {
                string SqlQuery = @"SELECT * FROM slbfe.admin ";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    await _mySqlConnection.OpenAsync();
                    using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                GetAdmin getResponse = new GetAdmin();

                                //getResponse.AdminId = _sqlDataReader["AdminId"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["AdminId"]) : 0;

                                getResponse.UserName = _sqlDataReader["UserName"] != DBNull.Value ? _sqlDataReader["UserName"].ToString() : string.Empty;

                                getResponse.Password = _sqlDataReader["Password"] != DBNull.Value ? _sqlDataReader["Password"].ToString() : string.Empty;

                                response.readAdmin.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;*/
        }


        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if(_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM slbfe.user_details 
                                    WHERE Email=@Email AND Password=@PassWord AND Affiliation=@Affiliation;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);
                    sqlCommand.Parameters.AddWithValue("@Affiliation", request.Affiliation);
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.Message = "Login Successfull";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Login Failed";
                            return response;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }


        /// <summary>
        /// Sign Up
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                if (!request.Password.Equals(request.ConfigPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                string SqlQuery = @"INSERT INTO 
                                    slbfe.user_details 
                                    (NIC, Name, Address, Age, Profession, Email, PassWord, Affiliation) VALUES 
                                    (@NIC, @Name, @Address, @Age, @Profession, @Email, @PassWord, @Affiliation)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NIC", request.NIC);
                    sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                    sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                    sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                    sqlCommand.Parameters.AddWithValue("@Profession", request.Profession);
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);
                    sqlCommand.Parameters.AddWithValue("@Affiliation", request.Affiliation);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if(Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }


        /// <summary>
        /// Get Citizen Information List
        /// </summary>
        /// <returns></returns>
        public async Task<ReadCitizenInformationResponse> ReadCitizenInformation()
        {
            ReadCitizenInformationResponse response = new ReadCitizenInformationResponse();
            response.readCitizenInformation = new List<ReadCitizenInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = @"SELECT * FROM slbfe.user_details ";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    await _mySqlConnection.OpenAsync();
                    using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ReadCitizenInformation getResponse = new ReadCitizenInformation();

                                getResponse.UserId = _sqlDataReader["UserId"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["UserId"]) : 0;

                                getResponse.NIC = _sqlDataReader["NIC"] != DBNull.Value ? _sqlDataReader["NIC"].ToString() : string.Empty;

                                getResponse.Name = _sqlDataReader["Name"] != DBNull.Value ? _sqlDataReader["Name"].ToString() : string.Empty;

                                getResponse.Address = _sqlDataReader["Address"] != DBNull.Value ? _sqlDataReader["Address"].ToString() : string.Empty;

                                getResponse.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;

                                getResponse.Profession = _sqlDataReader["Profession"] != DBNull.Value ? _sqlDataReader["Profession"].ToString() : string.Empty;

                                getResponse.Email = _sqlDataReader["Email"] != DBNull.Value ? _sqlDataReader["Email"].ToString() : string.Empty;

                                getResponse.Affiliation = _sqlDataReader["Affiliation"] != DBNull.Value ? _sqlDataReader["Affiliation"].ToString() : string.Empty;

                                response.readCitizenInformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }


        /// <summary>
        /// Get Citizen Information By NIC
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CitizenInformationByNICResponse> GetCitizenInformationByNIC(CitizenInformationByNICRequest request)
        {
            CitizenInformationByNICResponse response = new CitizenInformationByNICResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = @"SELECT * FROM slbfe.user_details WHERE NIC = @NIC AND Affiliation = 'Citizen'";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    sqlCommand.Parameters.AddWithValue("@NIC", request.NIC);
                    await _mySqlConnection.OpenAsync();
                    using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            await _sqlDataReader.ReadAsync();
                            response.getCitizenInformationByNIC = new GetCitizenInformationByNIC();

                            response.getCitizenInformationByNIC.Name = _sqlDataReader["Name"] != DBNull.Value ? _sqlDataReader["Name"].ToString() : string.Empty;
                            response.getCitizenInformationByNIC.Address = _sqlDataReader["Address"] != DBNull.Value ? _sqlDataReader["Address"].ToString() : string.Empty;
                            response.getCitizenInformationByNIC.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;
                            response.getCitizenInformationByNIC.Profession = _sqlDataReader["Profession"] != DBNull.Value ? _sqlDataReader["Profession"].ToString() : string.Empty;
                            response.getCitizenInformationByNIC.Email = _sqlDataReader["Email"] != DBNull.Value ? _sqlDataReader["Email"].ToString() : string.Empty;
                        }
                        else
                        {
                            response.Message = "No Citizen Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }


        /// <summary>
        /// Get Citizen Information By Qualification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CitizenInformationByQualificationResponse> GetCitizenInformationByQualification(CitizenInformationByQualificationRequest request)
        {
            CitizenInformationByQualificationResponse response = new CitizenInformationByQualificationResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = @"SELECT * FROM slbfe.user_details WHERE Qualification = @Qualification AND Affiliation = 'Citizen'";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    sqlCommand.Parameters.AddWithValue("@Qualification", request.Qualification);
                    await _mySqlConnection.OpenAsync();
                    using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            await _sqlDataReader.ReadAsync();
                            response.getCitizenInformationByQualification = new GetCitizenInformationByQualification();

                            response.getCitizenInformationByQualification.Name = _sqlDataReader["Name"] != DBNull.Value ? _sqlDataReader["Name"].ToString() : string.Empty;
                            response.getCitizenInformationByQualification.Address = _sqlDataReader["Address"] != DBNull.Value ? _sqlDataReader["Address"].ToString() : string.Empty;
                            response.getCitizenInformationByQualification.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;
                            response.getCitizenInformationByQualification.Profession = _sqlDataReader["Profession"] != DBNull.Value ? _sqlDataReader["Profession"].ToString() : string.Empty;
                            response.getCitizenInformationByQualification.Email = _sqlDataReader["Email"] != DBNull.Value ? _sqlDataReader["Email"].ToString() : string.Empty;
                        }
                        else
                        {
                            response.Message = "No Citizen Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }


        /// <summary>
        /// Update Citizen Information
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UpdateCitizenInformationResponse> UpdateCitizenInformationRequest(UpdateCitizenInformationRequest request)
        {
            UpdateCitizenInformationResponse resposne = new UpdateCitizenInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_mySqlConnection != null)
                {
                    string SqlQuery = @"UPDATE slbfe.user_details SET Name= @Name , Age=@Age where UserId=@UserId ";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                        sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return resposne;
        }


        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest request)
        {
            DeleteCitizenResponse resposne = new DeleteCitizenResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_mySqlConnection != null)
                {
                    string SqlQuery = @"DELETE FROM slbfe.user_details WHERE UserId = @UserId";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return resposne;
        }


        /// <summary>
        /// Get Complaints List
        /// </summary>
        /// <returns></returns>
        public async Task<ReadComplaintsResponse> ReadComplaints()
        {
            ReadComplaintsResponse response = new ReadComplaintsResponse();
            response.readComplaints = new List<ReadComplaints>();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = @"SELECT * FROM slbfe.complaints ";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    await _mySqlConnection.OpenAsync();
                    using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ReadComplaints getResponse = new ReadComplaints();

                                getResponse.ComplaintId = _sqlDataReader["ComplaintId"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ComplaintId"]) : 0;

                                getResponse.Complaint = _sqlDataReader["Complaint"] != DBNull.Value ? _sqlDataReader["Complaint"].ToString() : string.Empty;

                                getResponse.Reply = _sqlDataReader["Reply"] != DBNull.Value ? _sqlDataReader["Reply"].ToString() : string.Empty;

                                response.readComplaints.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }


        /// <summary>
        /// Create Complaint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateComplaintResponse> CreateComplaintInformation(CreateComplaintRequest request)
        {
            CreateComplaintResponse response = new CreateComplaintResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO 
                                    slbfe.complaints 
                                    (Complaint, Reply) VALUES 
                                    (@Complaint, @Reply)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Complaint", request.Complaint);
                    sqlCommand.Parameters.AddWithValue("@Reply", request.Reply);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }


        /// <summary>
        /// Reply for Complaint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UpdateComplaintInformationResponse> UpdateComplaintInformationRequest(UpdateComplaintInformationRequest request)
        {
            UpdateComplaintInformationResponse resposne = new UpdateComplaintInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_mySqlConnection != null)
                {
                    string SqlQuery = @"UPDATE slbfe.complaints SET Complaint= @Complaint , Reply=@Reply where ComplaintId=@ComplaintId ";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ComplaintId", request.ComplaintId);
                        sqlCommand.Parameters.AddWithValue("@Complaint", request.Complaint);
                        sqlCommand.Parameters.AddWithValue("@Reply", request.Reply);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return resposne;
        }
    }
}
