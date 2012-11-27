using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Heli.Scada.Entities;
using Heli.Scada.BL;
using Heli.Scada.dal;
using System.Collections.Generic;
using System.Linq;
using Heli.Scada.DalInterfaces;

namespace Heli.Scada.Unittests
{
    [TestClass]
    public class EngineerBLTest
    {
        IEngineerRepository<EngineerModel> mockerepo;
        ICustomerRepository<CustomerModel> mockcrepo;
        IInstallationRepository<InstallationModel> mockirepo;
       
        public EngineerBLTest()
        {
            Mock<ICustomerRepository<CustomerModel>> mockCustomerRepository = new Mock<ICustomerRepository<CustomerModel>>();
            Mock<IEngineerRepository<EngineerModel>> mockEngineerRepository = new Mock<IEngineerRepository<EngineerModel>>();
            Mock<IInstallationRepository<InstallationModel>> mockInstallationRepository = new Mock<IInstallationRepository<InstallationModel>>();
           
            List<EngineerModel> engineers = new List<EngineerModel>
            {
                new EngineerModel {engineerid=0, firstname="Karl", lastname="Maier", password="test123",email="k.maier@aon.at", username="karli"}
            };
            List<InstallationModel> installations = new List<InstallationModel>
            {
                new InstallationModel { installationid= 0, customerid=0, description="test", latitude=44, longitude=55, Measurement= new List<int>{0}, serialno="serial"}
            };
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel { customerid=0, engineerid=0, firstname="Anton", lastname="Huber", password="test",email="a.huber@aon.at", username="toni", Installation=new List<int> {0}}
            };

            mockCustomerRepository.Setup(mr => mr.GetAll()).Returns(customers);
            mockCustomerRepository.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int customerid) => customers.Where(customer => customer.customerid == customerid).Single());
            mockCustomerRepository.Setup(mr => mr.Add(It.IsAny<CustomerModel>())).Callback((CustomerModel customer) => customers.Add(customer));
            mockInstallationRepository.Setup(mr => mr.GetByCustomerId(It.IsAny<int>())).Returns((int customerid) => installations.Where(installation => installation.customerid == customerid).ToList<InstallationModel>());
            mockInstallationRepository.Setup(mr => mr.Add(It.IsAny<InstallationModel>())).Callback((InstallationModel installation) => installations.Add(installation));         
            mockEngineerRepository.Setup(mr => mr.GetMyCustomers(It.IsAny<int>())).Returns((int id) => customers.Where(customer => customer.engineerid == id).ToList<CustomerModel>());
            
            this.mockcrepo = mockCustomerRepository.Object;
            this.mockirepo = mockInstallationRepository.Object;
            this.mockerepo = mockEngineerRepository.Object;
        }

      

        [TestMethod]
        public void TestcreateCustomer()
        {
            EngineerBL ebl = new EngineerBL(mockcrepo, mockerepo,mockirepo);
            CustomerModel mycustomer = new CustomerModel{
                customerid=1, firstname="new", lastname="user", username="newuser", email="newuser@aon.at",
                engineerid=0, Installation=new List<int>{0,1}, password="test123"
            };
            InstallationModel myinstallation = new InstallationModel{
                installationid=1, customerid=1, description="testinstallation",
                 serialno="testser", latitude=33, longitude=44, Measurement=new List<int>{0}
            };
            ebl.createCustomer(mycustomer, myinstallation);
            Assert.AreEqual(2, mockcrepo.GetAll().Count);
            Assert.AreEqual(2, mockcrepo.GetAll().Count);
        }

        [TestMethod]
        public void TestshowMyCustomers()
        {
            EngineerBL ebl = new EngineerBL(mockcrepo, mockerepo, mockirepo);
            List<CustomerModel> clist= ebl.showMyCustomers(0);
            Assert.IsNotNull(clist);
            Assert.AreEqual(1, clist.Count);
            Assert.AreEqual(0, clist.First().customerid);
        }
    }
}
