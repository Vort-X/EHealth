using EHealth.Services;
using EHealth.WebApi.Mappers;
using EHealth.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        [HttpGet]
        [Route("get-doctors")]
        public async Task<IEnumerable<DoctorViewModel>> GetDoctorsList()
        {
            var doctorsList = await doctorsService.GetDoctorsListAsync();
            return doctorsList.Select(d => d.ToViewModel());
        }

        [HttpGet]
        [Route("get-doctor/{id}")]
        public async Task<DoctorViewModel> GetDoctor(int id)
        {
            var doctor = await doctorsService.GetDoctorAsync(id);
            return doctor?.ToViewModel();
        }

        [HttpGet]
        [Route("get-occupations")]
        public async Task<IEnumerable<KeyValuePair<int, string>>> GetOccupations()
        {
            return await doctorsService.GetOccupations();
        }
    }
}
