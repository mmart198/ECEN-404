using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using CsvHelper;


namespace LabInfoSys
{
    class ReadFromFile1
    {
        static void Main(string[] args)
        {

            string MainFolder = @"C:\Users\sec\Desktop\ECEN 404\ECEN 403\2081_161021_014341";
            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine("sep=;");
            string csvpath = @"C:\Users\Public\Downloads\alldirectory.csv";

            Dictionary<string, int> hash = new Dictionary<string, int>();
            HashSet<string> name = new HashSet<string>();
            int id = 0;

            foreach (var filepath in Directory.GetFiles(MainFolder, "*.MES", SearchOption.AllDirectories))
            {
                List<string> lines = File.ReadAllLines(filepath).ToList(); //Read line, and split it by whitespace into an array of strings
                col newcol = new col();

                string line;
                int counter = 0;
                int curveindex = lines.IndexOf("**Curve");
                int stapleindex = lines.IndexOf("**Staple");
                int alongindex = lines.IndexOf("**Along");
                System.IO.StreamReader file = new System.IO.StreamReader(filepath);


                while ((line = file.ReadLine()) != null)
                {

                    if (line == "****")
                    {
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
                        int newalong = lines.IndexOf("**Along", alongindex);
                        alongindex = newalong + 1;

                        string line37 = lines[newalong - 1].Trim();
                        string[] entry1 = Regex.Split(line37, @"\s+");


                        //customer id generation using hash_map function
                        
                        if (!name.Contains(lines[counter + 4]))
                        {
                            name.Add(lines[counter + 4]);
                            hash.Add(lines[counter + 4], id);
                            id++;
                        }




                        //staple
                        if (lines.Contains("**Staple"))
                        {
                            int newstaple = lines.IndexOf("**Staple", stapleindex);
                            stapleindex = newstaple+1;

                            string line47 = lines[newstaple + 3].Trim();
                            string[] entry2 = Regex.Split(line47, @"\s+");

                            string line48 = lines[newstaple + 4].Trim();
                            string[] entry3 = Regex.Split(line48, @"\s+");
                            var wool = new[]
                            {
                                new col {
                                    SampleID = lines[counter+ 1].Trim(),
                                    DateTime = DateTime.Parse(lines[counter + 2] + " " + lines[counter + 3]),
                                    CustomerName = lines[counter + 4],
                                    CustomerID = hash[lines[counter+4]],
                                    SampleLog = lines[counter + 5],
                                    CompanyName = lines[counter + 6],
                                    SampleName = lines[counter + 7],
                                    TotalFiberCount = int.Parse(lines[counter + 9]),
                                    MeanDiameter = double.Parse(lines[counter + 10]),
                                    STD = double.Parse(lines[counter + 11]),
                                    VariationCoefficient = double.Parse(entry[0]),
                                    CEM = double.Parse(entry[1]),
                                    ComfortFactor = double.Parse(entry[2]),
                                    MinDiameter = double.Parse(entry0[0]),
                                    MaxDiameter = double.Parse(entry0[1]),
                                    MeanCurvature = double.Parse(entry1[0]),
                                    SDCurvature = double.Parse(entry1[1]),
                                    StapleLength = double.Parse(entry2[0]),
                                    MinStaple = double.Parse(entry3[0]),
                                    MaxStaple = double.Parse(entry3[1]),
                                    Fibers_Hist = hist1
                                }
                            };

                            using (var mem = new MemoryStream())
                            using (var writer = new StreamWriter(mem))
                            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                csvWriter.Configuration.Delimiter = ";";
                                csvWriter.Configuration.HasHeaderRecord = false;
                                csvWriter.Configuration.AutoMap<col>();

                                //csvWriter.WriteHeader<col>();
                                csvWriter.WriteRecords(wool);

                                writer.Flush();
                                var result = Encoding.UTF8.GetString(mem.ToArray());
                                Console.WriteLine(result);
                                csvcontent.AppendLine(result);
                            }
                        }
                        else
                        {
                            var wool = new[]
                            {
                                new col {
                                    SampleID = lines[counter+ 1].Trim(),
                                    DateTime = DateTime.Parse(lines[counter + 2] + " " + lines[counter + 3]),
                                    CustomerName = lines[counter + 4],
                                    CustomerID = hash[lines[counter+4]],
                                    SampleLog = lines[counter + 5],
                                    CompanyName = lines[counter + 6],
                                    SampleName = lines[counter + 7],
                                    TotalFiberCount = int.Parse(lines[counter + 9]),
                                    MeanDiameter = double.Parse(lines[counter + 10]),
                                    STD = double.Parse(lines[counter + 11]),
                                    VariationCoefficient = double.Parse(entry[0]),
                                    CEM = double.Parse(entry[1]),
                                    ComfortFactor = double.Parse(entry[2]),
                                    MinDiameter = double.Parse(entry0[0]),
                                    MaxDiameter = double.Parse(entry0[1]),
                                    MeanCurvature = double.Parse(entry1[0]),
                                    SDCurvature = double.Parse(entry1[1]),
                                    Fibers_Hist = hist1
                                }
                            };
                            using (var mem = new MemoryStream())
                            using (var writer = new StreamWriter(mem))
                            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                csvWriter.Configuration.Delimiter = ";";
                                csvWriter.Configuration.HasHeaderRecord = false;
                                csvWriter.Configuration.AutoMap<col>();

                                //csvWriter.WriteHeader<col>();
                                csvWriter.WriteRecords(wool);

                                writer.Flush();
                                var result = Encoding.UTF8.GetString(mem.ToArray());
                                Console.WriteLine(result);
                                csvcontent.AppendLine(result);

                            }
                        }

                    }
                    counter++;
                }

            }

            File.AppendAllText(csvpath, csvcontent.ToString());
            File.WriteAllLines(csvpath, File.ReadAllLines(csvpath).Where(l => !string.IsNullOrWhiteSpace(l)));
        }

    }
}


