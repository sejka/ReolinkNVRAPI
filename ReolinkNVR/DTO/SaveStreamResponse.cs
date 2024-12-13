using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReolinkNVR.DTO
{
    public class SaveStreamResponse
    {
        public string cmd { get; set; }
        public int code { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public int fileCount { get; set; }
        public Filelist[] fileList { get; set; }
    }

    public class Filelist
    {
        public string fileName { get; set; }
        public string fileSize { get; set; }
    }
}