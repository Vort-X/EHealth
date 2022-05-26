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
        [HttpGet]
        [Route("get-doctors")]
        public async Task<IEnumerable<object>> GetDoctorsList()
        {
            await Task.CompletedTask;
            return Array.Empty<object>();
        }

        [HttpGet]
        [Route("get-doctor/{id}")]
        public async Task<object> GetDoctor(int id)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}
