using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();
        public static PatientServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            instance = null;


            Patients = new List<Patient>
            {
                new Patient{Id = 1, Name = "John Doe", Address = "808 watt dr", 
                    Birthday = new DateTime(2000, 01, 05), Gender = "Male",
                Race = "Black", Diagnoses = "N/A", Prescription = "N/A"}
                , new Patient{Id = 2, Name = "Jane Doe", Address = "809 watt dr",
                    Birthday = new DateTime(2000, 01, 05), Gender = "Female",
                Race = "White", Diagnoses = "N/A", Prescription = "N/A"}
            };
        }
        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private List<Patient> patients;
        public List<Patient> Patients
        {
            get
            {
                return patients;
            }

            private set
            {
                if (patients != value)
                {
                    patients = value;
                }
            }
        }

        public void AddOrUpdatePatient(Patient patient)
        {
            bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
                isAdd = true;
            }
            if(isAdd)
            {
                Patients.Add(patient);
            }
        }

        public void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);

            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }
        }
        public Patient GetPatientById(int id)
        {
            return Patients.FirstOrDefault(p => p.Id == id);
        }
    }
}