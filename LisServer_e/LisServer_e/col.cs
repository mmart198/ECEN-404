using System;
using System.Collections.Generic;
using System.Text;

namespace LabInfoSys
{
    public class col
    {
        public string SampleID { get; set; }
        public string SampleLog { get; set; }
        public string SampleName { get; set; }
        public DateTime DateTime { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public int TotalFiberCount { get; set; }
        public double MeanDiameter { get; set; }
        public double MinDiameter { get; set; }
        public double MaxDiameter { get; set; }
        public double STD { get; set; }
        public double VariationCoefficient { get; set; }
        public double CEM { get; set; }
        public double ComfortFactor { get; set; }
        public double MeanCurvature { get; set; }
        public double SDCurvature { get; set; }
        public double StapleLength { get; set; }
        public double MinStaple { get; set; }
        public double MaxStaple { get; set; }
        public string Fibers_Hist { get; set; }
    }
}
