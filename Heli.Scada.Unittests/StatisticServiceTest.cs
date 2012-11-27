using System;
using System.Collections.Generic;
using Heli.Scada.dal;
using Heli.Scada.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Heli.Scada.BL;
using Microsoft.Practices.Unity;
using Heli.Scada.DalInterfaces;

namespace Heli.Scada.Unittests
{
    [TestClass]
    public class StatisticServiceTest
    {
        IMeasurementRepository<MeasurementModel> mockmrepo;
        IInstallationRepository<InstallationModel> mockirepo;
        IMeasurementTypeRepository<MeasurementTypeModel> mockmtrepo;

        public void Init()
        {
            
            Mock<IInstallationRepository<InstallationModel>> mockInstallationRepository = new Mock<IInstallationRepository<InstallationModel>>();
            Mock<IMeasurementTypeRepository<MeasurementTypeModel>> mockMeasurementTypeRepository = new Mock<IMeasurementTypeRepository<MeasurementTypeModel>>();
                     
            List<MeasurementTypeModel> measurementtypes = new List<MeasurementTypeModel>
            {
                new MeasurementTypeModel { typeid=0, Measurement = new List<int> {0}, description = "test", maxvalue = 0, minvalue =0, unit = "Grad Celsius"}
            };
            List<InstallationModel> installations = new List<InstallationModel>
            {
                new InstallationModel { installationid= 0, customerid=0, description="test", latitude=44, longitude=55, Measurement= new List<int>{0}, serialno="serial"}
            };
            mockInstallationRepository.Setup(mr => mr.GetByCustomerId(It.IsAny<int>())).Returns((int customerid) => installations.Where(installation => installation.customerid == customerid).ToList<InstallationModel>());
            mockMeasurementTypeRepository.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int id) => measurementtypes.Where(measurementtype => measurementtype.typeid == id).Single());

            this.mockmrepo = new MockMeasurementRepository();
            this.mockmtrepo = mockMeasurementTypeRepository.Object;
            this.mockirepo = mockInstallationRepository.Object;
        }

        [TestMethod]
        public void TestgetStatisticsperDay()
        {
            Init();
            StatisticService sservice = new StatisticService(mockirepo, mockmrepo, mockmtrepo);
            Dictionary<InstallationModel, List<Statistic>> slist = sservice.getStatisticPerDay(0, DateTime.Now);
            Assert.IsNotNull(slist);
            Assert.AreEqual(1, slist.Count);

        }

        [TestMethod]
        public void TestgetInstallationState()
        {
            Init();
            StatisticService sservice = new StatisticService(mockirepo, mockmrepo, mockmtrepo);
            Dictionary<InstallationModel, List<InstallationState>> ilist = sservice.getInstallationState(0);
            Assert.IsNotNull(ilist);
            Assert.AreEqual(1, ilist.Count);
        }

    }
}
