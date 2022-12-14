namespace VaccOps
{
    using Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccDb : IVaccOps
    {
        private Dictionary<string, Doctor> doctorsByName = new Dictionary<string, Doctor>();
        private Dictionary<string, Patient> patientsByName = new Dictionary<string, Patient>();

        public void AddDoctor(Doctor doctor)
        {
            if (this.doctorsByName.ContainsKey(doctor.Name))
            {
                throw new ArgumentException();
            }

            this.doctorsByName.Add(doctor.Name, doctor);
        }

        public void AddPatient(Doctor doctor, Patient patient)
        {
            if (!this.doctorsByName.ContainsKey(doctor.Name))
            {
                throw new ArgumentException();
            }
            this.patientsByName.Add(patient.Name, patient);
            this.doctorsByName[doctor.Name].Patients.Add(patient);
            patient.Doctor = doctor;
        }

        public void ChangeDoctor(Doctor oldDoctor, Doctor newDoctor, Patient patient)
        {
            if (!this.doctorsByName.ContainsKey(oldDoctor.Name) || !this.doctorsByName.ContainsKey(newDoctor.Name)
                || !this.patientsByName.ContainsKey(patient.Name))
            {
                throw new ArgumentException();
            }
            oldDoctor.Patients.Remove(patient);
            newDoctor.Patients.Add(patient);
            patient.Doctor = newDoctor;
        }

        public bool Exist(Doctor doctor)
        {
            return this.doctorsByName.ContainsKey(doctor.Name);
        }

        public bool Exist(Patient patient)
        {
            return this.patientsByName.ContainsKey(patient.Name);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return this.doctorsByName.Values;
        }

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
        {
           return this.doctorsByName.Values.Where(doc => doc.Popularity == populariry).ToList();
        }

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
        {
            return this.GetDoctors().OrderByDescending(doc => doc.Patients.Count).ThenBy(doc => doc.Name);
        }

        public IEnumerable<Patient> GetPatients()
        {
            return this.patientsByName.Values;
        }

        public IEnumerable<Patient> GetPatientsByTown(string town)
        {
            return this.patientsByName.Values.Where(t => t.Town == town).ToList();
        }

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
        {
            return this.patientsByName.Values.Where(pat => pat.Age <= hi && pat.Age >= lo);
        }

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
        {
            return this.patientsByName.Values.OrderBy(pat => pat.Doctor.Popularity)
                .ThenByDescending(pat => pat.Height)
                .ThenBy(pat => pat.Age);
        }

        public Doctor RemoveDoctor(string name)
        {
            if (!this.doctorsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            var doctor = this.doctorsByName[name];
            this.doctorsByName.Remove(name);
            foreach (var patient in doctor.Patients)
            {
                this.patientsByName.Remove(patient.Name);
            }

            return doctor;
        }
    }
}
