using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class StubDoctorsService : IDoctorsService
    {
        public async Task<DoctorModel> GetDoctorAsync(int id)
        {
            return await Task.FromResult(new DoctorModel()
            {
                Id = 0,
                FullName = "Vladimir H",
                Occupations = new string[] { "Therapist", "Ph.D" },
                AvailableAppointmentTime = new Dictionary<int, DateTime>()
                {
                    { 1, new DateTime(2022, 07, 01, 13, 00, 00) },
                    { 2, new DateTime(2022, 07, 01, 15, 00, 00) },
                    { 3, new DateTime(2022, 07, 01, 17, 00, 00) },
                }
            });
        }

        public async Task<IEnumerable<DoctorModel>> GetDoctorsListAsync()
        {
            return await Task.FromResult(new List<DoctorModel>()
            {
                new()
                {
                    Id = 0,
                    FullName = "Vladimir H",
                    Occupations = new string[] { "Therapist", "Ph.D" },
                    AvailableAppointmentTime = new Dictionary<int, DateTime>()
                    {
                        { 1, new DateTime(2022, 07, 01, 13, 00, 00) },
                        { 2, new DateTime(2022, 07, 01, 15, 00, 00) },
                        { 3, new DateTime(2022, 07, 01, 17, 00, 00) },
                    }
                },
                new()
                {
                    Id = 1,
                    FullName = "Robert D",
                    Occupations = new string[] { "Psychologist", "Neurologist" },
                    AvailableAppointmentTime = new Dictionary<int, DateTime>()
                    {
                        { 3, new DateTime(2022, 07, 01, 17, 00, 00) },
                        { 4, new DateTime(2022, 07, 01, 17, 30, 00) },
                        { 5, new DateTime(2022, 07, 01, 18, 00, 00) },
                        { 6, new DateTime(2022, 07, 01, 18, 30, 00) },
                    }
                },
                new()
                {
                    Id = 2,
                    FullName = "Ivan K",
                    Occupations = new string[] { "Therapist" },
                    AvailableAppointmentTime = new Dictionary<int, DateTime>()
                    {
                        { 7, new DateTime(2022, 07, 02, 13, 00, 00) },
                        { 8, new DateTime(2022, 07, 02, 15, 00, 00) },
                        { 9, new DateTime(2022, 07, 02, 17, 00, 00) },
                    }
                }
            });
        }

        public Task<IEnumerable<KeyValuePair<int, string>>> GetOccupations()
        {
            throw new NotImplementedException();
        }
    }
}
