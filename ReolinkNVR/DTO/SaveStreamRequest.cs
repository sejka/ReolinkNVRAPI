using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReolinkNVR.DTO
{
    public class SaveStreamRequest
    {
        public string cmd { get; set; } = "NvrDownload";
        public int action { get; set; } = 1;
        public SaveStreamParam param { get; set; }
    }

    public class SaveStreamParam
    {
        public Nvrdownload NvrDownload { get; set; }
    }

    public class Nvrdownload
    {
        public int channel { get; set; }
        public string streamType { get; set; } = "main";
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }
    }

    public class Time
    {
        public int year { get; set; }
        public int mon { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int min { get; set; }
        public int sec { get; set; }

        public Time(DateTime dateTime)
        {
            year = dateTime.Year;
            mon = dateTime.Month;
            day = dateTime.Day;
            hour = dateTime.Hour;
            min = dateTime.Minute;
        }
    }
}