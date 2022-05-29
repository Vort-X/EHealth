using EHealth.Model;
using EHealth.Services;
using EHealth.WebApi.Controllers;
using EHealth.WebApi.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.WebApi.Testing
{
    public class AppointmentControllerTests
    {
        private AppointmentController stubbedController;
        private IAppointmentService appointmentService;

        [SetUp]
        public void Setup()
        {
            appointmentService = new StubAppointmentService();
            stubbedController = new(appointmentService, new StubIdentityService());
        }

        [Test]
        public async Task GetHistory()
        {
            string patientName = "Mihailo P";
            IEnumerable<HistoryViewModel> viewModels;
            IEnumerable<HistoryModel> models;

            viewModels = await stubbedController.GetHistory();
            models = await appointmentService.GetHistoryForPatientAsync(patientName);

            foreach (var model in models)
            {
                var viewModel = viewModels.FirstOrDefault(vm => 
                    Equals(vm.AppointmentDateTime, model.AppointmentDateTime) &&
                    Equals(vm.Diagnosis, model.Diagnosis) &&
                    Equals(vm.DoctorFullName, model.DoctorFullName) &&
                    Equals(vm.Status, model.Status) 
                    );

                Assert.That(viewModel, Is.Not.Null);
            }
        }

        [Test]
        public async Task Schedule_StubbedValues_ReturnsStubbed()
        {
            AppointmentViewModel viewModel = new()
            {
                DoctorId = default,
                AppointmentTimeId = default
            };
            bool isScheduled;

            isScheduled = await stubbedController.Schedule(viewModel);

            Assert.IsTrue(isScheduled);
        }
    }
}