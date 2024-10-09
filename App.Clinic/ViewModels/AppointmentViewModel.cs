using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    class AppointmentViewModel
    {
        private Patient selectedPatient;
        private Physician selectedPhysician;
        private DateTime appointmentDate;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Physician> Physicians { get; set; }

        public Appointment? Model { get; set; }
        
        public int Id
        {
            get
            {
                if (Model == null)
                {
                    return -1;
                }

                return Model.Id;
            }

            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
            }
        }
        public DateTime AppointmentDate
        {
            get => Model?.AppointmentDate ?? DateTime.MinValue;
            set
            {
                if (Model != null)
                {
                    Model.AppointmentDate = value;
                }
            }
        }
        public Patient Patient
        {
            get => Model?.Patient ?? new Patient();
            set
            {
                if (Model != null)
                {
                    Model.Patient = value;
                }
            }
        }
        public Physician Physician
        {
            get => Model?.Physician ?? new Physician();
            set
            {
                if (Model != null)
                {
                    Model.Physician = value;
                }
            }
        }
        public AppointmentViewModel()
        {
            Model = new Appointment();

            Patients = new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
        }

        public AppointmentViewModel(Appointment? _model)
        {
            Model = _model;

            Patients = new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                AppointmentServiceProxy
                .Current
                .AddOrUpdateAppointment(Model);
            }

            Shell.Current.GoToAsync("//Appointments");
        }

        public bool IsAvailable(Appointment appointment)
        {
          return AppointmentServiceProxy.Current.IsAvailable(appointment);
        }
    }
}

