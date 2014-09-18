using System;
using System.Activities.Statements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecepiesApp.Services.Controllers;

namespace RecepiesApp.Services.Tests
{
    [TestClass]
    public class UserInfoControllerTests
    {
        [TestMethod]
        public void Add()
        {
            using (new TransactionScope())
            {
                UserInfoController controller = new UserInfoController();

                //controller.Add();
            }
        }
        [TestMethod]
        public void Select()
        {
            using (new TransactionScope())
            {
                UserInfoController controller = new UserInfoController();

                //controller.Select();
            }
        }
        [TestMethod]
        public void All()
        {
            using (new TransactionScope())
            {
                UserInfoController controller = new UserInfoController();

                //controller.All();
            }
        }
        [TestMethod]
        public void Delete()
        {
            using (new TransactionScope())
            {
                UserInfoController controller = new UserInfoController();

                //controller.Delete();
            }
        }
        [TestMethod]
        public void Edit()
        {
            using (new TransactionScope())
            {
                UserInfoController controller = new UserInfoController();

                //controller.Edit();
            }
        }
    }
}
