using EHealth.Identity;
using EHealth.Services;
using EHealth.WebApi.ViewModel;
using EHealth.WebApi.ViewModel.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.WebApi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService<IAuthorizable> identityService;

        public AdminController(IAdminService adminService, IIdentityService<IAuthorizable> identityService)
        {
            this.adminService = adminService;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("add-doctor-to-occupation")]
        public async Task AddDoctorToOccupation([FromBody] DoctorOccupationViewModel viewModel)
        {
            await adminService.AddDoctorToOccupation(viewModel.DoctorId, viewModel.OccupationId);
        }

        [HttpPost]
        [Route("add-occupation")]
        public async Task AddOccupation([FromBody] string occupation)
        {
            await adminService.AddOccupationAsync(occupation);
        }

        [HttpPost]
        [Route("add-status")]
        public async Task AddStatus([FromBody] string status)
        {
            await adminService.AddStatusAsync(status);
        }

        [HttpPost]
        [Route("register-doctor")]
        public async Task RegisterDoctor([FromBody] RegisterViewModel viewModel)
        {
            await identityService.RegisterAsync(ur =>
            {
                ur.FullName = viewModel.FullName;
                ur.UserName = viewModel.UserName;
            },
            viewModel.Password, UserRoles.Doctor);
            await adminService.AddDoctor(viewModel.FullName);
        }
    }
}
