﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber {  get; set; }
        public DateTime GraduationDate { get; set; }
        public string Specilizations { get; set; }

        public override string ToString()
        {
            return $"{Name}, Id: {Id}";
        }

        public Physician() 
        {
            Name = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specilizations = string.Empty;
        }
    }
}
