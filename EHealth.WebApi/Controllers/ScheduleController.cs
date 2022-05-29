using EHealth.Identity;
using EHealth.Services;
using EHealth.WebApi.Mappers;
using EHealth.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Roles = UserRoles.Doctor)]
    public class ScheduleController : ControllerBase
    {
        private readonly IIdentityService<IAuthorizable> identityService;
        private readonly IAppointmentService appointmentService;

        public ScheduleController(IIdentityService<IAuthorizable> identityService, IAppointmentService appointmentService)
        {
            this.identityService = identityService;
            this.appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("add-appointment-time")]
        public async Task AddAppointmentTime([FromBody] AppointmentTimeViewModel viewModel)
        {
            var doctorsName = await identityService.GetFullName(User?.Identity?.Name);
            await appointmentService.AddAppointmentTimeToDoctorAsync(doctorsName, new(
                year: viewModel.Year,
                month: viewModel.Month,
                day: viewModel.Day,
                hour: viewModel.Hour,
                minute: viewModel.Minute,
                second: 0
                ));
        }

        [HttpGet]
        [Route("get-schedule")]
        public async Task<IEnumerable<HistoryViewModel>> GetDoctorsSchedule()
        {
            var doctorsName = await identityService.GetFullName(User?.Identity?.Name);
            var history = await appointmentService.GetHistoryForDoctorAsync(doctorsName);
            var historyViewModel = history.Select(h => h.ToViewModel());

            return historyViewModel;
        }

        [HttpPost]
        [Route("complete-appointment")]
        public async Task<IActionResult> CompleteAppointment([FromBody] CompleteAppointmentViewModel viewModel)
        {
            var isCompleted = await appointmentService.CompleteAppointmentAsync(
                id: viewModel.Id,
                status: viewModel.StatusId,
                diagnosis: viewModel.Diagnosis
            );

            return isCompleted ? Ok() : BadRequest();
        }
    }
}
