using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models
{
    public class Wow
    {
        [Key]
        public int WowId { get; set; }
        public String Name { get; set; }
        public String Street { get; set; }
    }
}