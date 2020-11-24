using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using LabInfoSys;

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
                } else
                {
                    sql = $"SELECT * FROM ECEN403_BSWL.SampleData_403;";
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
                    if (HasColumn(rdr, "DateTime"))
                        if (rdr["DateTime"] != DBNull.Value)
                            data.Date_Time = (DateTime)rdr["DateTime"]+"";
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
        [HttpPost("Upload")]
       public void Upload([FromBody] MesRequest request)
        {
            System.Console.WriteLine("%^^^^^^^^^^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^%^");
            string cs = @"server=Lusherengineeringservices.com;user id=ECENBSWL;database=ECEN403_BSWL;persistsecurityinfo=True;port=3315;pwd=ECENBSWL403";  //connect to mysql
            using var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            System.Console.WriteLine(request.mesFile);
            List<string> lines = request.mesFile; //Read line, and split it by whitespace into an array of strings
            col newcol = new col();

            string line;
            int counter = 0;
            int stapleindex = lines.FindIndex(a => a.Contains("**Staple"));
            int alongindex = lines.FindIndex(a => a.Contains("**Along"));

            System.Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$    " + lines.Count);
            System.Console.WriteLine(lines[0]);
            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i];
                System.Console.WriteLine();
                if (line.Contains("****"))
                {
                    System.Console.WriteLine("hit me");
                    string line12 = lines[counter + 12].Trim();
                    string[] entry = Regex.Split(line12, @"\s+");

                    //hist
                    string line13 = lines[counter + 13].Trim();
                    string[] entry0 = Regex.Split(line13, @"\s+");

                    int mind = int.Parse(entry0[0]);
                    int maxd = int.Parse(entry0[1]);
                    int histdata = maxd - mind + 1;

                    string hist = lines[counter + 14].Trim();
                    string[] totalhist = Regex.Split(hist, @"\s+");
                    int histline = counter + 14;
                    while (totalhist.Length < histdata)
                    {
                        hist += " " + lines[histline + 1].Trim();
                        totalhist = Regex.Split(hist, @"\s+");
                        histline++;
                    }
                    string hist1 = hist.Replace(' ', ',');


                    //curve = along-1
                    int newalong = lines.FindIndex(alongindex, a => a.Contains("**Along"));
                    alongindex = newalong + 1;

                    string line37 = lines[newalong - 1].Trim();
                    string[] entry1 = Regex.Split(line37, @"\s+");

                    string sampleID = lines[counter + 1].Trim();
                    DateTime dateTime = DateTime.Parse(lines[counter + 2] + " " + lines[counter + 3]);
                    string date = dateTime.ToString("yyyy-MM-dd H:mm:ss");
                    string customerName = lines[counter + 4];
                    string sampleLog = lines[counter + 5];
                    string companyName = lines[counter + 6];
                    string sampleName = lines[counter + 7];
                    int totalFiberCount = int.Parse(lines[counter + 9]);
                    double meanDiameter = double.Parse(lines[counter + 10]);
                    double sTD = double.Parse(lines[counter + 11]);
                    double variationCoefficient = double.Parse(entry[0]);
                    double cEM = double.Parse(entry[1]);
                    double comfortFactor = double.Parse(entry[2]);
                    double minDiameter = double.Parse(entry0[0]);
                    double maxDiameter = double.Parse(entry0[1]);
                    double meanCurvature = double.Parse(entry1[0]);
                    double sDCurvature = double.Parse(entry1[1]);
                    string fibers_Hist = hist1;
                    customerName = Regex.Replace(customerName, @"\t|\n|\r", "");
                    //CustomerName in MySQL
                    //System.Console.WriteLine("INSERT INTO CustomerName (name) SELECT * FROM CustomerName WHERE NOT EXISTS (SELECT name FROM CustomerName WHERE name = '" + customerName + "')");
                    //cmd.CommandText = "INSERT INTO CustomerName(name) SELECT* FROM CustomerName WHERE NOT EXISTS(SELECT name FROM CustomerName WHERE name = '" + customerName + "')";
                    //cmd.ExecuteNonQuery();
                    string sql = $"SELECT * FROM ECEN403_BSWL.CustomerName WHERE name = '{customerName}';";
                    cmd = new MySqlCommand(sql, con);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    List<LIS> list = new List<LIS>();

                    //creates array for results
                    customerName = Regex.Replace(customerName, @"\t|\n|\r", "");
                    string existingName = "nullName";
                    while (rdr.Read())
                    {
                        if (HasColumn(rdr, "name"))
                            if (rdr["name"] != DBNull.Value)
                            {
                                existingName = (string)rdr["name"];
                            }
                    }
                    rdr.Close();
                    System.Console.WriteLine(existingName + "    " + customerName);
                    if (existingName != customerName)
                    {
                        cmd.CommandText = $"INSERT INTO CustomerName (name) VALUES('{customerName}');";
                        cmd.ExecuteNonQuery();
                    }
                    //staple
                    if (lines.FindIndex(a => a.Contains("**Staple")) != -1)
                    {
                        System.Console.WriteLine("HIT ME 2");
                        int newstaple = lines.FindIndex(stapleindex, a => a.Contains("**Staple"));
                        stapleindex = newstaple + 1;

                        string line47 = lines[newstaple + 3].Trim();
                        string[] entry2 = Regex.Split(line47, @"\s+");

                        string line48 = lines[newstaple + 4].Trim();
                        string[] entry3 = Regex.Split(line48, @"\s+");

                        double stapleLength = double.Parse(entry2[0]);
                        double minStaple = double.Parse(entry3[0]);
                        double maxStaple = double.Parse(entry3[1]);

                        var wool = new[]
                        {
                                new col {
                                    SampleID = sampleID.Replace("'","''"),
                                    DateTime = dateTime,
                                    CustomerName = customerName,
                                    SampleLog = sampleLog,
                                    CompanyName = companyName,
                                    SampleName = sampleName,
                                    TotalFiberCount = totalFiberCount,
                                    MeanDiameter = meanDiameter,
                                    STD = sTD,
                                    VariationCoefficient = variationCoefficient,
                                    CEM = cEM,
                                    ComfortFactor = comfortFactor,
                                    MinDiameter = minDiameter,
                                    MaxDiameter = maxDiameter,
                                    MeanCurvature = meanCurvature,
                                    SDCurvature = sDCurvature,
                                    StapleLength = stapleLength,
                                    MinStaple = minStaple,
                                    MaxStaple = maxStaple,
                                    Fibers_Hist = fibers_Hist
                                }
                            };
                        foreach (var col in wool)
                        {
                            System.Console.WriteLine("INSEWRT STATEMENT");
                            string commandtext = ($"'{col.SampleID}', '{col.SampleLog}', '{col.SampleName}', '{date}', '{col.CompanyName}', '{col.CustomerName}', " +
                                $"'{col.TotalFiberCount}', '{col.MeanDiameter}', '{col.MinDiameter}', '{col.MaxDiameter}', '{col.STD}', '{col.VariationCoefficient}', " +
                                $"'{col.CEM}', '{col.ComfortFactor}', '{col.MeanCurvature}', '{col.SDCurvature}', '{col.StapleLength}', '{col.MinStaple}', '{col.MaxStaple}', '{col.Fibers_Hist}'");
                            commandtext = "INSERT INTO SampleData_403(SampleID,SampleLog,SampleName,DateTime,CompanyName,CustomerName," +
                                "TotalFiberCount,MeanDiameter,MinDiameter,MaxDiameter,STD,VariationCoefficient,CEM,ComfortFactor,MeanCurvature,SDCurvature,StapleLength,MinStaple,MaxStaple,Fibers_Hist)" +
                                " VALUES(" + commandtext + ")";
                            cmd.CommandText = commandtext;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE SampleData_403 INNER JOIN CustomerName ON SampleData_403.CustomerName = CustomerName.name SET SampleData_403.CustomerID = CustomerName.id WHERE CustomerName.name = '" + customerName + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    else
                    {
                        var wool = new[]
                        {
                                new col {
                                    SampleID = sampleID.Replace("'","''"),
                                    DateTime = dateTime,
                                    CustomerName = customerName,
                                    SampleLog = sampleLog,
                                    CompanyName = companyName,
                                    SampleName = sampleName,
                                    TotalFiberCount = totalFiberCount,
                                    MeanDiameter = meanDiameter,
                                    STD = sTD,
                                    VariationCoefficient = variationCoefficient,
                                    CEM = cEM,
                                    ComfortFactor = comfortFactor,
                                    MinDiameter = minDiameter,
                                    MaxDiameter = maxDiameter,
                                    MeanCurvature = meanCurvature,
                                    SDCurvature = sDCurvature,
                                    Fibers_Hist = fibers_Hist
                                }
                            };
                        foreach (var col in wool)
                        {
                            string commandtext = ($"'{col.SampleID}', '{col.SampleLog}', '{col.SampleName}', '{date}', '{col.CompanyName}', '{col.CustomerName}', '{col.TotalFiberCount}', '{col.MeanDiameter}', '{col.MinDiameter}', '{col.MaxDiameter}', '{col.STD}', '{col.VariationCoefficient}', " +
                                $"'{col.CEM}', '{col.ComfortFactor}', '{col.MeanCurvature}', '{col.SDCurvature}', '{col.Fibers_Hist}'");
                            commandtext = "INSERT INTO SampleData_403(SampleID,SampleLog,SampleName,DateTime,CompanyName,CustomerName,TotalFiberCount,MeanDiameter,MinDiameter,MaxDiameter,STD,VariationCoefficient,CEM,ComfortFactor,MeanCurvature,SDCurvature,Fibers_Hist)" +
                                " VALUES(" + commandtext + ")";
                            cmd.CommandText = commandtext;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE SampleData_403 INNER JOIN CustomerName ON SampleData_403.CustomerName = CustomerName.name SET SampleData_403.CustomerID = CustomerName.id WHERE CustomerName.name = '" + customerName + "'";
                            cmd.ExecuteNonQuery();

                        }
                    }

                }
                counter++;
            }
            System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            return;
        }



    }
}
