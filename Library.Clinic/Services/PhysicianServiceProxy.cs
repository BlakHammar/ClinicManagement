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
        public List<Physician> Physicians { get; private set; } = new List<Physician>();

        public void AddPhysician(Physician physician)
        {
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
            }
            Physicians.Add(physician);
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
