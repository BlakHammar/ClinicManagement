using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    class AppointmentViewModel
    {
        private Patient selectedPatient;
        private Physician selectedPhysician;
        private DateTime appointmentDate;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Physician> Physicians { get; set; }

        public ICommand? DeleteCommand { get; set; }

        public ICommand? EditCommand { get; set; }

        public Appointment? Model { get; set; }

        public Patient? pModel { get; set; }
        
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

        public int PatientId 
        {
            get
            {
                if (pModel == null)
                {
                    return -1;
                }

                return pModel.Id;
            }

            set
            {
                if (pModel != null && pModel.Id != value)
                {
                    pModel.Id = value;
                }
            }
        }
        public DateTime StartTime
        {
            get => Model?.StartTime ?? DateTime.MinValue;
            set
            {
                if (Model != null)
                {
                    Model.StartTime = value;
                }
            }
        }

        public DateTime EndTime
        {
            get => Model?.EndTime ?? DateTime.MinValue;
            set
            {
                if (Model != null)
                {
                    Model.EndTime = value;
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

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
        }
        private void DoDelete()
        {
            if (Id > 0)
            {
                AppointmentServiceProxy.Current.DeleteAppointment(Id);
                Shell.Current.GoToAsync("//Appointments");
            }
        }

        private void DoEdit(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Id ?? 0;

            Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
        }

        public AppointmentViewModel()
        {
            Model = new Appointment();

            Patients = new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
            SetupCommands();
        }

        public AppointmentViewModel(Appointment? _model)
        {
            Model = _model;

            Patients = new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
            SetupCommands();
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

