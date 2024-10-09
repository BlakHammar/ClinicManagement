using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel
    {
        public Physician? Model { get; set; }

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

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        public string LicenseNumber
        {
            get => Model?.LicenseNumber ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.LicenseNumber = value;
                }
            }
        }

        public DateTime GraduationDate
        {
            get => Model?.GraduationDate ?? DateTime.MinValue;
            set
            {
                if (Model != null)
                {
                    Model.GraduationDate = value;
                }
            }
        }

        public string Specilizations
        {
            get => Model?.Specilizations ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.Specilizations = value;
                }
            }
        }

        public PhysicianViewModel()
        {
            Model = new Physician();
        }

        public PhysicianViewModel(Physician? _model)
        {
            Model = _model;
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PhysicianServiceProxy
                .Current
                .AddOrUpdatePhysician(Model);
            }

            Shell.Current.GoToAsync("//Physicians");
        }
    }
}