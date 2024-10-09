using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;

            Physicians = new List<Physician>
            {
                new Physician {Id = 1, Name = "Kobe Myers", LicenseNumber = "555", GraduationDate = new DateTime(2025, 01, 15), Specilizations = "N/A" }
                , new Physician {Id = 2, Name = "Lucas Myers", LicenseNumber = "444", GraduationDate = new DateTime(2025, 01, 20), Specilizations = "N/A"}
            };
        }
        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private List<Physician> physicians;
        public List<Physician> Physicians
        {
            get
            {
                return physicians;
            }
            private set
            {
                if (physicians != value)
                {
                    physicians = value;
                }
            }
        }

        public void AddOrUpdatePhysician(Physician physician)
        {
            bool isAdd = false;
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Physicians.Add(physician);
            }
        }

        public void DeletePhysician(int id)
        {
            var PhysicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);
            if (PhysicianToRemove != null)
            {
                Physicians.Remove(PhysicianToRemove);
            }
        }
    }
}
