using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool isContinue = true;
            do
            {
                Console.WriteLine("Welcome to the Medical Management System!");
                Console.WriteLine("A: Add a Patient ");
                Console.WriteLine("B: Add a Physician");
                Console.WriteLine("C: Schedule Appointment");
                Console.WriteLine("E: List Patients");
                Console.WriteLine("F: List Physicians");
                Console.WriteLine("G: List Appointments");
                Console.WriteLine("Q: Quit");

                string input = Console.ReadLine() ?? string.Empty;


                if (char.TryParse(input, out char choice))
                {
                    switch (choice)
                    {
                        case 'a':
                        case 'A':
                            Console.WriteLine("Enter the name: ");
                            var namePatient = Console.ReadLine();

                            Console.WriteLine("Enter the address: ");
                            var address = Console.ReadLine();

                            DateTime birthdate;
                            Console.WriteLine("Please enter the birthdate (MM/DD/YYYY):");
                            string dateBirth = Console.ReadLine();

                            if (DateTime.TryParse(dateBirth, out birthdate))
                            {
                                Console.WriteLine($"Your birthdate is: {birthdate.ToShortDateString()}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                                return;
                            }

                            Console.WriteLine("Enter the race: ");
                            var race = Console.ReadLine();

                            Console.WriteLine("Enter the gender: ");
                            var gender = Console.ReadLine();

                            Console.WriteLine("Enter the diagnoses: ");
                            var diagnoses = Console.ReadLine();

                            Console.WriteLine("Enter the prescriptions: ");
                            var prescriptions = Console.ReadLine();

                            var newPatient = new Patient
                            {
                                Name = namePatient ?? string.Empty,
                                Address = address ?? string.Empty,
                                Birthday = birthdate,
                                Race = race ?? string.Empty,
                                Gender = gender ?? string.Empty,
                                Diagnoses = diagnoses ?? string.Empty,
                                Prescription = prescriptions ?? string.Empty
                            };

                            PatientServiceProxy.Current.AddOrUpdatePatient(newPatient);
                            break;


                        case 'b':
                        case 'B':
                            Console.WriteLine("Enter the name: ");
                            var namePhysician = Console.ReadLine();

                            Console.WriteLine("Enter license number: ");
                            var licenseNumber = Console.ReadLine();

                            DateTime graduationDate;
                            Console.WriteLine("Enter graduation date: ");
                            string dateGrad = Console.ReadLine();

                            if (DateTime.TryParse(dateGrad, out graduationDate))
                            {
                                Console.WriteLine($"Your graduation date is: {graduationDate.ToShortDateString()}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                                return;
                            }

                            Console.WriteLine("Enter any specilizations: ");
                            var specilizations = Console.ReadLine();

                            var newPhysician = new Physician
                            {
                                Name = namePhysician ?? string.Empty,
                                LicenseNumber = licenseNumber ?? string.Empty,
                                GraduationDate = graduationDate,
                                Specilizations = specilizations ?? string.Empty
                            };

                            PhysicianServiceProxy.Current.AddPhysician(newPhysician);

                            break;

                        case 'c':
                        case 'C':
                            Console.WriteLine("Enter Patient Name:");
                            var patientName = Console.ReadLine();

                            var patient = PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Name.Equals(patientName));

                            if (patient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                return;
                            }

                            Console.WriteLine("Enter Physician Name: ");
                            var physicianName = Console.ReadLine();

                            var physician = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Name.Equals(physicianName));
                            
                            if (physician == null)
                            {
                                Console.WriteLine("Patient not found.");
                                return;
                            }

                            DateTime appointmentDate;
                            Console.WriteLine("Enter Appointment Date and Time (MM/dd/yyyy HH:mm):");
                            string dateAppoint = Console.ReadLine();

                            if (DateTime.TryParse(dateAppoint, out appointmentDate))
                            {
                                Console.WriteLine($"Your appointment date is: {appointmentDate.ToString("MM/dd/yyyy HH:mm")}");

                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                                return;
                            }

                            var newAppointment = new Appointment
                            {
                                AppointmentDate = appointmentDate,
                                Patient = patient,
                                Physician = physician,
                            };

                            if (AppointmentServiceProxy.Current.IsAvailable(newAppointment))
                            {
                                AppointmentServiceProxy.Current.AddAppointment(newAppointment);
                                Console.WriteLine("Appointment successfully scheduled.");
                            }
                            else
                            {
                                Console.WriteLine("The physician is either double-booked or the time is outside business hours.");
                            }


                            break;


                        case 'd':
                        case 'D':
                            PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}.{x.Name}"));
                            int selectedPatient= int.Parse(Console.ReadLine() ?? "-1" );
                            PatientServiceProxy.Current.DeletePatient(selectedPatient);

                            break;
                        case 'e':
                        case 'E':
                            PatientServiceProxy.Current.Patients.ForEach(Console.WriteLine);
                            break;

                        case 'f':
                        case 'F':
                            PhysicianServiceProxy.Current.Physicians.ForEach(Console.WriteLine);
                            break;

                        case 'g':
                        case 'G':
                            AppointmentServiceProxy.Current.Appointments.ForEach(Console.WriteLine);
                            break;

                        case 'q':
                        case 'Q':
                            isContinue = false;
                            break;
                        default:
                            Console.WriteLine("That is an invalid choice!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{choice} is not a char");
                }


            } while ( isContinue );


        }
    }
}
