﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Models.Attributumos.Attri;

namespace Project.Models
{
    [ToExport("Manager")]
    public class Manager
    {

        [StringLength(60)]
        public string Name { get; set; }
        [Key]
        public string ManagerId { get; set; }
        public int BirthYear { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public bool HasMBA { get; set; }
    }
}
