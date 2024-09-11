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
        public DateTime AppointmentDate { get; set; }
        public Patient Patient { get; set; }
        public Physician Physician { get; set; }

        public override string ToString()
        {
            return $"{AppointmentDate}, Patient: {Patient}, Physician: {Physician}";
        }

        public Appointment() 
        {
            AppointmentDate = DateTime.Now;
            Patient = new Patient();
            Physician = new Physician();

        }
    }
}
