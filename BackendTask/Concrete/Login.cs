using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Concrete
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        public int LoginTime { get; set; }
        public DateTime LoginDate { get; set; }

    }
}
