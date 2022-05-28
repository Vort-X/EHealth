using EHealth.Identity;
using EHealth.Services;
using EHealth.WebApi.Mappers;
using EHealth.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAppointmentService appointmentService;
        private readonly IIdentityService<IAuthorizable> identityService;

        public AppointmentController(IAppointmentService appointmentService, IIdentityService<IAuthorizable> identityService)
        {
            this.appointmentService = appointmentService;
            this.identityService = identityService;
        }

        [HttpGet]
        [Route("get-history")]
        public async Task<IEnumerable<HistoryViewModel>> GetHistory()
        {
            var patientName = await identityService.GetFullName(User?.Identity?.Name);
            var history = await appointmentService.GetHistoryForPatientAsync(patientName);
            var historyViewModel = history.Select(h => h.ToViewModel());

            return historyViewModel;
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<bool> Schedule(AppointmentViewModel scheduleViewModel)
        {
            var patientName = await identityService.GetFullName(User?.Identity?.Name);
            var appointmentModel = scheduleViewModel.ToModel(patientName);
            var isScheduled = await appointmentService.ScheduleAppointmentAsync(appointmentModel);

            return isScheduled;
        }
    }
}
