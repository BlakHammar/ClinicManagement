using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        private static object _lock = new object();
        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static AppointmentServiceProxy? instance;
        private AppointmentServiceProxy()
        {
            instance = null;

            var patient1 = PatientServiceProxy.Current.GetPatientById(1);
            var patient2 = PatientServiceProxy.Current.GetPatientById(2);
            var physician1 = PhysicianServiceProxy.Current.GetPhysicianById(1);
            var physician2 = PhysicianServiceProxy.Current.GetPhysicianById(2);

            Appointments = new List<Appointment>
            {
                new Appointment {Id = 1, StartTime = new DateTime(2026, 07, 21, 8, 00, 00), 
                    EndTime = new DateTime(2026, 07, 21, 8, 30, 00), Patient = patient1, Physician = physician1, PatientId = patient1.Id}

                , new Appointment {Id = 2, StartTime = new DateTime(2026, 07, 22, 8, 00, 00), 
                    EndTime = new DateTime(2026, 07, 21, 8, 30, 00), Patient = patient2, Physician = physician2, PatientId = patient2.Id}
            };
        }
        private int lastKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get
            {
                return appointments;
            }
            private set
            {
                if (appointments != value)
                {
                    appointments = value;
                }
            }
        }

        public void AddOrUpdateAppointment(Appointment appointment)
        {
            var isAvail = false;
            if (appointment.Id <= 0)
            {
                appointment.Id = lastKey + 1;
                isAvail = IsAvailable(appointment);
            }
            if (isAvail)
            {
                Appointments.Add(appointment);
            }
        }

        public bool IsAvailable(Appointment appointment)
        {
            // Check if the appointment time is within business hours (Monday to Friday, 8 AM to 5 PM)
            if (appointment.StartTime.Value.Hour < 8 || appointment.StartTime.Value.Hour >= 17 ||
                appointment.StartTime.Value.DayOfWeek == DayOfWeek.Saturday ||
                appointment.StartTime.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                return false; // Not within business hours
            }

            // Check if the physician is double-booked
            foreach (var existingAppointment in Appointments)
            {
                if (existingAppointment.Physician == appointment.Physician &&
                    existingAppointment.StartTime == appointment.StartTime)
                {
                    return false; // Physician is already booked at this time
                }
            }

            return true; // Appointment is available
        }
        public void DeleteAppointment(int id)
        {
            var appointmentToRemove = Appointments.FirstOrDefault(p => p.Id == id);

            if (appointmentToRemove != null)
            {
                Appointments.Remove(appointmentToRemove);
            }
        }

    }
}
