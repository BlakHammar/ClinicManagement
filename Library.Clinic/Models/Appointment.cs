using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public int PhysicianId { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Patient Patient { get; set; }
        public Physician Physician { get; set; }

        public override string ToString()
        {
            return $"{StartTime}, Patient: {Patient}, Physician: {Physician}";
        }

        public Appointment() 
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Patient = new Patient();
            Physician = new Physician();

        }
    }
}
