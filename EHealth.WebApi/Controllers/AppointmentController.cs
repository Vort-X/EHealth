using EHealth.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = UserRoles.User)]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        [Route("get-history/{id}")]
        public async Task<IEnumerable<object>> GetHistory(int id)
        {
            await Task.CompletedTask;
            return Array.Empty<object>();
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<bool> Schedule(object scheduleViewModel)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
