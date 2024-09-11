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
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();

        public void AddAppointment(Appointment appointment)
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

        
    }
}
