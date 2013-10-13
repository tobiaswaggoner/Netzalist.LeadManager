using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models
{
    public class Test
    {
        [Key]
        public int TestPK { get; set; }
        public String Name { get; set; }
    }
}