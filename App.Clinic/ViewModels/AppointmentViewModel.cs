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
        private Physician selectedPhysician;
        private DateTime appointmentDate;

        public Patient? SelectedPatient
        {
            get 
            {
                return Model?.Patient; 
            }
            set
            {
                var selectedPatient = value;
                if (Model != null)
                {
                    Model.Patient = selectedPatient;
                    Model.PatientId = selectedPatient?.Id ?? 0;
                }
            }
        }
        public Physician? SelectedPhysician
        {
            get
            {
                return Model?.Physician;
            }
            set
            {
                var selectedPhysician = value;
                if (Model != null)
                {
                    Model.Physician = selectedPhysician;
                    Model.PhysicianId = selectedPhysician?.Id ?? 0;
                }
            }
        }
        public ObservableCollection<Patient> Patients 
        {
            get
            {
                return new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            }
        }
        public ObservableCollection<Physician> Physicians 
        {
            get
            {
                return new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
            }
        }

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
                if (Model == null)
                {
                    return -1;
                }

                return Model.PatientId;
            }

            set
            {
                if (Model != null && Model.Patient.Id != value)
                {
                    Model.PatientId = value;
                }
            }
        }
        public int PhysicianId
        {
            get
            {
                if (Model == null)
                {
                    return -1;
                }

                return Model.PhysicianId;
            }

            set
            {
                if (Model != null && Model.Physician.Id != value)
                {
                    Model.PhysicianId = value;
                }
            }
        }
        public DateTime MinStartDate 
        {  
            get
            {
                return DateTime.Today;
            }

        }

        public void RefreshTime()
        {
            if (Model != null)
            {
                if (Model.StartTime != null)
                {
                    Model.StartTime = StartDate;
                    Model.StartTime = Model.StartTime.Value.Add(StartTime);
                }
            }
        }
        public DateTime StartDate 
        { 
            get
            {
                return Model?.StartTime?.Date ?? DateTime.Today;
            }
            set
            {
                if (Model != null)
                {
                    Model.StartTime = value;
                    RefreshTime();  
                }
            }
        }
        public TimeSpan StartTime { get; set; }

        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
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

            SetupCommands();
        }

        public AppointmentViewModel(Appointment? _model)
        {
            Model = _model;

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

