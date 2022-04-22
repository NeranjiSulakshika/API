using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.CommonUtility
{
    public class SqlQueries
    {

        static IConfiguration _sqlQueryConfiguration = new ConfigurationBuilder()
            .AddXmlFile("SqlQueries.xml", true, true)
            .Build();

        public static string CreateInformationQuery { get { return _sqlQueryConfiguration["CreateInformationQuery"]; } }
        public static string ReadCitizenInformation { get { return _sqlQueryConfiguration["ReadCitizenInformation"]; } }
        public static string UpdateCitizenInformationRequest { get { return _sqlQueryConfiguration["UpdateCitizenInformationRequest"]; } }
        public static string SearchInformationById { get { return _sqlQueryConfiguration["SearchInformationById"]; } }
        public static string DeleteInformation { get { return _sqlQueryConfiguration["DeleteInformation"]; } }
    }
}
