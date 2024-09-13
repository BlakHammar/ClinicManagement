using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Patient
    {
        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
        public int Id { get; set; }

        private string? name;
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }

            set
            {
                name = value;
            }
        }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Diagnoses { get; set; }
        public string Prescription { get; set; }

        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            Race = string.Empty;
            Gender = string.Empty;
            Diagnoses = string.Empty;
            Prescription = string.Empty;
        }


    }
}
