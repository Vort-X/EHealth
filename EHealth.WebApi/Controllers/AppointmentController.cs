using EHealth.Identity.Default;
using EHealth.Services;
using EHealth.WebApi.Mappers;
using EHealth.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentController(IAppointmentService appointmentService, UserManager<ApplicationUser> userManager)
        {
            this.appointmentService = appointmentService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("get-history")]
        public async Task<IEnumerable<HistoryViewModel>> GetHistory()
        {
            var user = await userManager.GetUserAsync(User);
            var history = await appointmentService.GetHistoryForPatientAsync(user.FullName);
            return history.Select(h => h.ToViewModel());
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<bool> Schedule(AppointmentViewModel scheduleViewModel)
        {
            var user = await userManager.GetUserAsync(User);
            var isScheduled = await appointmentService.ScheduleAppointmentAsync(scheduleViewModel.ToModel(user.FullName));
            return isScheduled;
        }
    }
}
