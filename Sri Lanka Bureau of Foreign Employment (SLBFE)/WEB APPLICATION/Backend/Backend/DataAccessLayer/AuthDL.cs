using Microsoft.Extensions.Configuration;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;
using ExcelDataReader;
using System.Data;
using System.Text.RegularExpressions;

namespace Backend.DataAccessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public readonly string EmailRegex = @"^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$";

        int ConnectionTimeOut = 180;

        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:MySqlDBConnectionString"]);
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

                //Check Null Or Empty
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                {
                    response.Message = "Usename Or Password cannot Null or Empty";
                }

                //SQL Query
                string SqlQuery = @"SELECT * 
                                    FROM slbfe.admin 
                                    WHERE UserName=@UserName AND Password=@Password;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);

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
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
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

                //Check Null Or Empty
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Affiliation))
                {
                    response.Message = "Usename Or Password Or Affiliation cannot Null or Empty";
                }

                //Email Validation
                if (!(Regex.IsMatch(request.Email, EmailRegex)))
                {
                    response.IsSuccess = false;
                    response.Message = "Email is not correct format!";
                    return response;
                }

                //SQL Query
                string SqlQuery = @"SELECT * 
                                    FROM slbfe.user_details 
                                    WHERE Email=@Email AND Password=@Password AND Affiliation=@Affiliation;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
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
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
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

                //Password Validation
                if (!request.Password.Equals(request.ConfigPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                //Email Validation
                if (!(Regex.IsMatch(request.Email, EmailRegex)))
                {
                    response.IsSuccess = false;
                    response.Message = "Email is not correct format";
                    return response;
                }

                //SQL Query
                string SqlQuery = @"INSERT INTO 
                                    slbfe.user_details 
                                    (NIC, Name, Address, Age, Profession, Email, Password, Affiliation, Qualification) VALUES 
                                    (@NIC, @Name, @Address, @Age, @Profession, @Email, @Password, @Affiliation, @Qualification)";

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
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    sqlCommand.Parameters.AddWithValue("@Affiliation", request.Affiliation);
                    sqlCommand.Parameters.AddWithValue("@Qualification", request.Qualification);
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
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
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
                //SQL Query
                string SqlQuery = @"SELECT * FROM slbfe.user_details WHERE Affiliation = 'Citizen' ";

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

                                getResponse.Qualification = _sqlDataReader["Qualification"] != DBNull.Value ? _sqlDataReader["Qualification"].ToString() : string.Empty;

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
                //SQL Query
                string SqlQuery = @"SELECT * FROM slbfe.user_details WHERE NIC = @NIC AND Affiliation = 'Citizen'";

                //Check Null Or Empty
                if (string.IsNullOrEmpty(request.NIC))
                {
                    response.Message = "NIC cannot Null or Empty";
                }

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
                            response.getCitizenInformationByNIC.Qualification = _sqlDataReader["Qualification"] != DBNull.Value ? _sqlDataReader["Qualification"].ToString() : string.Empty;
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
            response.getCitizenInformationByQualification = new List<GetCitizenInformationByQualification>();

            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                //SQL Query
                string SqlQuery = @"SELECT * FROM slbfe.user_details WHERE Qualification = @Qualification AND Affiliation = 'Citizen'";

                //Check Null Or Empty
                if (string.IsNullOrEmpty(request.Qualification))
                {
                    response.Message = "Qualification cannot Null or Empty";
                }

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
                            while (await _sqlDataReader.ReadAsync())
                            {
                                GetCitizenInformationByQualification getResponse = new GetCitizenInformationByQualification();

                                getResponse.NIC = _sqlDataReader["NIC"] != DBNull.Value ? _sqlDataReader["NIC"].ToString() : string.Empty;

                                getResponse.Name = _sqlDataReader["Name"] != DBNull.Value ? _sqlDataReader["Name"].ToString() : string.Empty;

                                getResponse.Address = _sqlDataReader["Address"] != DBNull.Value ? _sqlDataReader["Address"].ToString() : string.Empty;

                                getResponse.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;

                                getResponse.Profession = _sqlDataReader["Profession"] != DBNull.Value ? _sqlDataReader["Profession"].ToString() : string.Empty;

                                getResponse.Email = _sqlDataReader["Email"] != DBNull.Value ? _sqlDataReader["Email"].ToString() : string.Empty;

                                response.getCitizenInformationByQualification.Add(getResponse);
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
        /// Update Citizen Qualification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /*public async Task<UpdateCitizenInformationResponse> UpdateCitizenInformationRequest(UpdateCitizenInformationRequest request)
        {
            UpdateCitizenInformationResponse response = new UpdateCitizenInformationResponse();

            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (_mySqlConnection != null)
                {
                    //SQL Query
                    string SqlQuery = @"UPDATE slbfe.user_details SET Qualification=@Qualification where UserId=@UserId ";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@Qualification", request.Qualification);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();

                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Informations Not Update";
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
        }*/


        /// <summary>
        /// Delete Citizen Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteCitizenResponse> DeleteCitizen(DeleteCitizenRequest request)
        {
            DeleteCitizenResponse response = new DeleteCitizenResponse();

            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (_mySqlConnection != null)
                {
                    //SQL Query
                    string SqlQuery = @"DELETE FROM slbfe.user_details WHERE UserId = @UserId AND Affiliation = 'Citizen'";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();

                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Please check the provided information carefully";
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
                //SQL Query
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

                //SQL Query
                string SqlQuery = @"INSERT INTO slbfe.complaints (Complaint) VALUES (@Complaint)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Complaint", request.Complaint);
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
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
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
            UpdateComplaintInformationResponse response = new UpdateComplaintInformationResponse();

            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (_mySqlConnection != null)
                {
                    //SQL Query
                    string SqlQuery = @"UPDATE slbfe.complaints SET Reply=@Reply where ComplaintId=@ComplaintId ";

                    using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ComplaintId", request.ComplaintId);
                        sqlCommand.Parameters.AddWithValue("@Reply", request.Reply);
                        await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();

                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Information Not Update";
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
    }
}
