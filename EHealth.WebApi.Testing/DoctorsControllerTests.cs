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
    class DoctorsControllerTests
    {
        private DoctorsController stubbedController;
        private IDoctorsService doctorsService;

        [SetUp]
        public void Setup()
        {
            doctorsService = new StubDoctorsService();
            stubbedController = new DoctorsController(doctorsService);
        }

        [Test]
        public async Task GetDoctorsList_StubbedValues_ReturnsStubbed()
        {
            IEnumerable<DoctorViewModel> viewModels;
            IEnumerable<DoctorModel> models;

            viewModels = await stubbedController.GetDoctorsList();
            models = await doctorsService.GetDoctorsListAsync();

            foreach (var model in models)
            {
                var viewModel = viewModels.FirstOrDefault(vm => Equals(vm.Id, model.Id));

                Assert.That(viewModel, Is.Not.Null);
                Assert.That(viewModel.FullName, Is.EqualTo(model.FullName));
                Assert.That(viewModel.Occupations, Is.EqualTo(model.Occupations));
                Assert.That(viewModel.AvailableAppointmentTime, Is.EqualTo(model.AvailableAppointmentTime));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task GetDoctor_StubbedValues_ReturnsStubbed(int id)
        {
            DoctorModel model;
            DoctorViewModel viewModel;

            viewModel = await stubbedController.GetDoctor(id);
            model = await doctorsService.GetDoctorAsync(id);

            Assert.That(viewModel.Id, Is.EqualTo(model.Id));
            Assert.That(viewModel.FullName, Is.EqualTo(model.FullName));
            Assert.That(viewModel.Occupations, Is.EqualTo(model.Occupations));
            Assert.That(viewModel.AvailableAppointmentTime, Is.EqualTo(model.AvailableAppointmentTime));
        }
    }
}
