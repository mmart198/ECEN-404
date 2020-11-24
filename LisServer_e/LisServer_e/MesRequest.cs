using System;
using System.Collections.Generic;

namespace LisServer_e
{
    public class MesRequest
    {
        
        public List<string> mesFile { get; set; }

        public MesRequest() { }

        public MesRequest(List<string> mesFile)
        {
            this.mesFile = mesFile;
        }
    }
}
