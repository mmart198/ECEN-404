using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace LisServer_e.Controllers
{
    [ApiController]
    [Route("api")]
    public class LISController : ControllerBase
    {
        private readonly ILogger<LISController> _logger;

        // Start Defining Variables here0
        MySql.Data.MySqlClient.MySqlConnection conn;
        private static readonly string myConnectionString = "server=Lusherengineeringservices.com;" +
            "user id=ECENBSWL;database=ECEN403_BSWL;persistsecurityinfo=True;port=3315;pwd=ECENBSWL403;";

        // end declarations

        public LISController(ILogger<LISController> logger)
        {
            _logger = logger;
        }

        //completed on start up
        
        [HttpGet]
        public IEnumerable<LIS> Get()
        {
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                Console.WriteLine("Status 200");
                return Enumerable.Range(1, 1).Select(index => new LIS
                {
                    Status = "Connection Successful."
                })
                .ToArray();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                var rng = new Random();
                return Enumerable.Range(1, 1).Select(index => new LIS
                {
                    Status = "Connection Failed."
                })
                .ToArray();
            }
        }

        //Search Function
        
        [HttpGet("search")]
        //Makes all listed variables optional
        public IEnumerable<LIS> Search(
            string sampleID = " ",
            string sampleLog = " ",
            string sampleName = " ",
            string companyName = " ",
            string customerName = " ",
            int customerID = 0,
            double meanDiameter = 0,
            double comfortFactor =0,
            double fmin_meanDiameter =-999,
            double fmax_meanDiameter = 0,
            double fmin_ComfortFactor =-999,
            double fmax_ComfortFactor=0)
        {
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "SELECT * FROM ECEN403_BSWL.SampleData_403";
                Console.WriteLine($"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE SampleID = {sampleID};");
                
                
                if (sampleID != " ")
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE SampleID = {sampleID};";
                }
                else if (sampleLog != " ")
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE SampleLog = {sampleLog};";
                }
                else if (sampleName != " ")
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE SampleName = {sampleName};";
                }
                else if (companyName!= " ")
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE CompanyName = {companyName};";
                }
                else if (customerName!=" ")
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE CustomerName = {customerName};";
                }
                
                else if (customerID != 0)
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE CustomerID = {customerID};";
                }

                else if(meanDiameter !=0)
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE MeanDiameter = {meanDiameter};";
                }

                else if (comfortFactor != 0)
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE ComfortFactor = {comfortFactor};";
                }

                else if (fmin_meanDiameter !=-999 && fmax_meanDiameter !=0)
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE MeanDiameter BETWEEN {fmin_meanDiameter} AND {fmax_meanDiameter};";
                }

                else if (fmin_ComfortFactor != -999 && fmax_ComfortFactor != 0)
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403 WHERE MeanDiameter BETWEEN {fmin_ComfortFactor} AND {fmax_ComfortFactor};";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<LIS> list = new List<LIS>();
                
                //creates array for results
                while (rdr.Read())
                {
                    LIS data = new LIS();
                   
                    //Sample ID
                    if(HasColumn(rdr,"SampleID"))
                        if (rdr["SampleID"] != DBNull.Value)
                            data.SampleID = (string)rdr["SampleID"];
                        else
                        data.SampleID = "null";

                    //Sample Log
                    if (HasColumn(rdr, "SampleLog"))
                        if(rdr["SampleLog"] != DBNull.Value)
                            data.SampleLog = (string)rdr["SampleLog"];
                        else
                            data.SampleLog = "null";

                    //Sample Name
                    if (HasColumn(rdr, "SampleName"))
                        if (rdr["SampleName"] != DBNull.Value)
                            data.SampleName = (string)rdr["SampleName"];
                        else
                            data.SampleName = "null";
                    
                    //Date/Time
                    if (HasColumn(rdr, "Date/Time"))
                        if (rdr["Date/Time"] != DBNull.Value)
                            data.Date_Time = (DateTime)rdr["Date/Time"]+"";
                        else
                            data.Date_Time = "null";

                    //Company Name
                    if (HasColumn(rdr, "CompanyName"))
                        if (rdr["CompanyName"] != DBNull.Value)
                            data.CompanyName = (string)rdr["CompanyName"];
                        else
                            data.CompanyName = "null";

                    //Customer Name
                    if (HasColumn(rdr, "CustomerName"))
                        if (rdr["CustomerName"] != DBNull.Value)
                            data.CustomerName = (string)rdr["CustomerName"];
                        else
                            data.CustomerName = "null";

                    //Customer ID
                    if (HasColumn(rdr, "CustomerID"))
                        if (rdr["CustomerID"] != DBNull.Value)
                            data.CustomerID = (int)rdr["CustomerID"];
                        else
                            data.CustomerID = 0;

                    //Mean Diameter
                    if (HasColumn(rdr, "MeanDiameter"))
                        if (rdr["MeanDiameter"] != DBNull.Value)
                            data.MeanDiameter = (double)rdr["MeanDiameter"];
                        else
                            data.MeanDiameter = 0;

                    //Comfort Factor
                    if (HasColumn(rdr, "ComfortFactor"))
                        if (rdr["ComfortFactor"] != DBNull.Value)
                            data.ComfortFactor = (double)rdr["ComfortFactor"];
                        else
                            data.ComfortFactor = 0;
                    
                    //TotalFiberCount
                    if (HasColumn(rdr, "TotalFiberCount"))
                        if (rdr["TotalFiberCount"] != DBNull.Value)
                            data.TotalFiberCount = (int)rdr["TotalFiberCount"];
                        else
                            data.TotalFiberCount = 0;
                    //STD
                    if(HasColumn(rdr, "STD"))
                        if (rdr["STD"] != DBNull.Value)
                        data.STD = (double)rdr["STD"];
                    else
                        data.STD= 0;

                    //VariationCoefficient
                    if (HasColumn(rdr, "VariationCoefficient"))
                        if (rdr["VariationCoefficient"] != DBNull.Value)
                            data.VariationCoefficient = (double)rdr["VariationCoefficient"];
                        else
                            data.VariationCoefficient = 0;

                    //CEM
                    if (HasColumn(rdr, "CEM"))
                        if (rdr["CEM"] != DBNull.Value)
                            data.CEM = (double)rdr["CEM"];
                        else
                            data.CEM = 0;

                    //MinStaple
                    if (HasColumn(rdr, "MinStaple"))
                        if (rdr["MinStaple"] != DBNull.Value)
                            data.MinStaple = (double)rdr["MinStaple"];
                        else
                            data.MinStaple = 0;

                    //MaxStaple
                    if (HasColumn(rdr, "MaxStaple"))
                        if (rdr["MaxStaple"] != DBNull.Value)
                            data.MaxStaple = (double)rdr["MaxStaple"];
                        else
                            data.MaxStaple = 0;

                    //MinDiameter
                    if (HasColumn(rdr, "MinDiameter"))
                        if (rdr["MinDiameter"] != DBNull.Value)
                            data.MinDiameter = (double)rdr["MinDiameter"];
                        else
                            data.MinDiameter = 0;

                    //MaxDiameter
                    if (HasColumn(rdr, "MaxDiameter"))
                        if (rdr["MaxDiameter"] != DBNull.Value)
                            data.MaxDiameter = (double)rdr["MaxDiameter"];
                        else
                            data.MaxDiameter = 0;

                    //Histogram Data
                    if (HasColumn(rdr, "Fibers_Hist"))
                        if (rdr["Fibers_Hist"] != DBNull.Value)
                            data.FibersHist = (string)rdr["Fibers_Hist"];
                        else
                            data.FibersHist = "null";

                    //MeanCurvature
                    if (HasColumn(rdr, "MeanCurvature"))
                        if (rdr["MeanCurvature"] != DBNull.Value)
                            data.MeanCurvature = (double)rdr["MeanCurvature"];
                        else
                            data.MeanCurvature = 0;

                    //SDCurvature
                    if (HasColumn(rdr, "SDCurvature"))
                        if (rdr["SDCurvature"] != DBNull.Value)
                            data.SDCurvature = (double)rdr["SDCurvature"];
                        else
                            data.SDCurvature = 0;

                    //StapleLength
                    if (HasColumn(rdr, "StapleLength"))
                        if (rdr["StapleLength"] != DBNull.Value)
                            data.StapleLength = (double)rdr["StapleLength"];
                        else
                            data.StapleLength = 0;

                    //Successful Test
                    data.Status = "Success";
                    list.Add(data);
                }
                IEnumerable<LIS> ret = list;
                return ret;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                var rng = new Random();
                return Enumerable.Range(1, 1).Select(index => new LIS
                {
                    Status = "Failed"
                })
                .ToArray();
            }
        }

        //Handles missing fields
        public static bool HasColumn(MySqlDataReader dr, string ColumnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {

                if (dr.GetName(i).Equals(ColumnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

       // [HttpGet("upload")]



    }
}
