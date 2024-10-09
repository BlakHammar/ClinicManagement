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

            Appointments = new List<Appointment>
            {
                new Appointment {Id = 1, AppointmentDate = new DateTime(2026, 07, 21), Patient = patient1}
                , new Appointment {Id = 2, AppointmentDate = new DateTime(2026, 07, 22), Patient = patient2}
            };
        }
        public int LastKey
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
            if (appointment.Id <= 0)
            {
                appointment.Id = LastKey + 1;
            }
            Appointments.Add(appointment);
        }

        public bool IsAvailable(Appointment appointment)
        {
            // Check if the appointment time is within business hours (Monday to Friday, 8 AM to 5 PM)
            if (appointment.AppointmentDate.Hour < 8 || appointment.AppointmentDate.Hour >= 17 ||
                appointment.AppointmentDate.DayOfWeek == DayOfWeek.Saturday ||
                appointment.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return false; // Not within business hours
            }

            // Check if the physician is double-booked
            foreach (var existingAppointment in Appointments)
            {
                if (existingAppointment.Physician == appointment.Physician &&
                    existingAppointment.AppointmentDate == appointment.AppointmentDate)
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
