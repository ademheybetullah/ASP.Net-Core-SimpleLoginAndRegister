using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Models
{
    public class RegistrationReportModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RegistrationCount { get; set; }
    }
}
