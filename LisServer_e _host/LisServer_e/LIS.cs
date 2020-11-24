using System;

namespace LisServer_e
{
    public class LIS
    {
        
        public string Status { get; set; }

        //Ear Tag # / Sample ID > H
        public string SampleID { get; set; }

        //Sample Log > H
        public string SampleLog { get; set; }

        //SampleName > H
        public string SampleName { get; set; }

        //Date/Time  H
        public string Date_Time { get; set; }

        //Company Name > H
        public string CompanyName { get; set; }

        //Customer Name > H
        public string CustomerName { get; set; }
        
        //Customer ID > H
        public int CustomerID { get; set; }

        //Total Fiber Count
        public int TotalFiberCount { get; set; }

        //Mean FIber Diameter > H
        public double MeanDiameter { get; set; }

        //Min Fiber Diameter
        public double MinDiameter { get; set; }

        //Max Fiber Diameter
        public double MaxDiameter { get; set; }

        //Standard Deviation
        public double STD { get; set; }

        //Variation Coefficient
        public double VariationCoefficient { get; set; }

        //CEM
        public double CEM { get; set; }

        //Comfort Factor H
        public double ComfortFactor { get; set; }

        //Mean Curvature
        public double MeanCurvature { get; set; }

        //SD of Curvature 
        public double SDCurvature { get; set; }

        //Staple Length
        public double StapleLength { get; set; }

        //Min Staple
        public double MinStaple { get; set; }

        //Max Staple
        public double MaxStaple { get; set; }
        
        //FibersHist
        public string FibersHist { get; set; }

        //MES Request
        public string MESRequest { get; set; }



    }
}
