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
    public class CustomerBLTest
    {
       
        ICustomerRepository<CustomerModel> mockcrepo;
        IInstallationRepository<InstallationModel> mockirepo;

        public void Init()
        {
            Mock<ICustomerRepository<CustomerModel>> mockCustomerRepository = new Mock<ICustomerRepository<CustomerModel>>();
            Mock<IInstallationRepository<InstallationModel>> mockInstallationRepository = new Mock<IInstallationRepository<InstallationModel>>();
           
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
           
            this.mockcrepo = mockCustomerRepository.Object;
            this.mockirepo = mockInstallationRepository.Object;
        }

       
        

        [TestMethod]
        public void TestcreateCustomer()
        {
            Init();
            CustomerBL cbl = new CustomerBL(mockcrepo, mockirepo);
            CustomerModel customer = new CustomerModel();
            customer.engineerid = 0;
            customer.firstname = "Max";
            customer.lastname = "Mustermann";
            customer.username = "Maxi";
            customer.password = "strenggeheim";
            customer.email = "max.mustermann@aon.at";
            cbl.createCustomer(customer);
            Assert.AreEqual(mockcrepo.GetAll().Count, 2);
        }

        [TestMethod]
        public void TestgetCustomer()
        {
            Init();
            CustomerBL cbl = new CustomerBL(mockcrepo, mockirepo);
            CustomerModel customer = cbl.getCustomer(0);
            Assert.AreEqual(customer.username, "toni");
        }

        [TestMethod]
        public void TestgetInstallations()
        {
            Init();
            CustomerBL cbl = new CustomerBL(mockcrepo, mockirepo);
            CustomerModel cmodel = cbl.getCustomer(0);
            List<InstallationModel> ilist = cbl.getInstallations(cmodel);
            Assert.AreEqual(ilist.Count, 1);

        }
    }
}
