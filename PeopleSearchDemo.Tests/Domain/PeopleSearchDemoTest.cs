using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchDemo.DataAccess;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.Domain;
using System.Drawing;
using System.IO;
using PeopleSearchDemo.Tests.Constant;
using Moq;
using System.Data.Entity;

namespace PeopleSearchDemo.Tests.Domain
{
    [TestClass]
    public class PeopleSearchDemoTest
    {
        [TestMethod]
        public void TestCreateMethod_PeopleWithoutImage()
        {
            // Arrange
            People inputPeople = new People();
            inputPeople.FirstName = Constant.Constant.Firstname;
            inputPeople.LastName = Constant.Constant.LastName;
            inputPeople.Address = Constant.Constant.Address;
            inputPeople.Gender = Constant.Constant.Gender;
            inputPeople.Interests = Constant.Constant.Interests;
            inputPeople.DateOfBirth = DateTime.Now;

            var mockSet = new Mock<DbSet<People>>();
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            service.Create(inputPeople);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<People>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }

        [TestMethod]
        public void TestCreateMethod_PeopleWithNullObject()
        {
            // Arrange
            People inputPeople = null;
            var mockSet = new Mock<DbSet<People>>();
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            service.Create(inputPeople);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<People>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void TestCreateMethod_PeopleWithEmptyInformation()
        {
            // Arrange
            People inputPeople = new People();
            inputPeople.FirstName = string.Empty;
            inputPeople.LastName = string.Empty;
            inputPeople.Address = string.Empty;
            inputPeople.Gender = string.Empty;
            inputPeople.Interests = string.Empty;
            inputPeople.DateOfBirth = DateTime.Now;
            inputPeople.Image = null;

            var mockSet = new Mock<DbSet<People>>();
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            service.Create(inputPeople);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<People>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());

        }

        [TestMethod]
        public void TestCreateMethod_CreateFourPeoples()
        {
            // Arrange
            People inputPeople1 = new People();
            inputPeople1.FirstName = "Harry";
            inputPeople1.LastName = "Porter";
            inputPeople1.Address = Constant.Constant.Address;
            inputPeople1.Gender = Constant.Constant.Gender;
            inputPeople1.Interests = Constant.Constant.Interests;
            inputPeople1.DateOfBirth = DateTime.Now;

            People inputPeople2 = new People();
            inputPeople2.FirstName = "Harriet";
            inputPeople2.LastName = "Huang";
            inputPeople2.Address = Constant.Constant.Address;
            inputPeople2.Gender = Constant.Constant.Gender;
            inputPeople2.Interests = Constant.Constant.Interests;
            inputPeople2.DateOfBirth = DateTime.Now;


            People inputPeople3 = new People();
            inputPeople3.FirstName = "Hunter";
            inputPeople3.LastName = "Yin";
            inputPeople3.Address = Constant.Constant.Address;
            inputPeople3.Gender = Constant.Constant.Gender;
            inputPeople3.Interests = Constant.Constant.Interests;
            inputPeople3.DateOfBirth = DateTime.Now;

            People inputPeople4 = new People();
            inputPeople4.FirstName = "Peter";
            inputPeople4.LastName = "P.";
            inputPeople4.Address = Constant.Constant.Address;
            inputPeople4.Gender = Constant.Constant.Gender;
            inputPeople4.Interests = Constant.Constant.Interests;
            inputPeople4.DateOfBirth = DateTime.Now;


            var mockSet = new Mock<DbSet<People>>();

            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.Peoples).Returns(mockSet.Object);


            // Act
            var service = new PeopleService(mockContext.Object);
            service.Create(inputPeople1);
            service.Create(inputPeople2);
            service.Create(inputPeople3);
            service.Create(inputPeople4);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<People>()), Times.AtLeast(4));
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeast(4));
        }

        [TestMethod]
        public async Task TestSearchMethodWithSearchString()
        {
            var data = new List<People>
            {
                new People { FirstName = "Hellen" ,LastName="Wang" },
                new People { FirstName = "Erin",LastName="Zhang" },
                new People { FirstName = "Lin",LastName="Wang" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<People>>();
            mockSet.As<IQueryable<People>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<People>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<People>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<People>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.Peoples).Returns(mockSet.Object);

            var service = new PeopleService(mockContext.Object);

            List<People> searchresults = await service.Search("a");

            Assert.AreEqual(3, searchresults.Count());
            Assert.AreEqual("Hellen", searchresults[0].FirstName);
            Assert.AreEqual("Erin", searchresults[1].FirstName);
            Assert.AreEqual("Lin", searchresults[2].FirstName);
        }

        [TestMethod]
        public async Task TestSearchMethodWithEmptySearchString()
        {
            // Arrange
            var data = new List<People>().AsQueryable();

            var mockSet = new Mock<DbSet<People>>();
            mockSet.As<IQueryable<People>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<People>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<People>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<People>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            string searchStr = string.Empty;
            List<People> searchresults = await service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);
        }

        [TestMethod]
        public async Task TestSearchMethodWithNullAsSearchString()
        {
            // Arrange
            var data = new List<People>().AsQueryable();

            var mockSet = new Mock<DbSet<People>>();
            mockSet.As<IQueryable<People>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<People>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<People>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<People>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            string searchStr = null;
            List<People> searchresults = await service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);
        }

        [TestMethod]
        public async Task TestSearchMethodWithSpecialCharsAsSearchString()
        {
            // Arrange
            var data = new List<People>().AsQueryable();

            var mockSet = new Mock<DbSet<People>>();
            mockSet.As<IQueryable<People>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<People>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<People>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<People>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            string searchStr = "*>>>>{}/";
            List<People> searchresults = await service.Search(searchStr);

            // Assert
            Assert.AreEqual(0, searchresults.Count);

        }

        [TestMethod]
        public void TestCreateMethod_PeopleWithImage()
        {
            // Arrange
            People inputPeople = new People();
            inputPeople.FirstName = Constant.Constant.Firstname;
            inputPeople.LastName = Constant.Constant.LastName;
            inputPeople.Address = Constant.Constant.Address;
            inputPeople.Gender = Constant.Constant.Gender;
            inputPeople.Interests = Constant.Constant.Interests;
            inputPeople.DateOfBirth = DateTime.Now;


            Image img = System.Drawing.Image.FromFile("../../Content/Images/HarryPotter.jpg");
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            inputPeople.Image = ms.ToArray();

            var mockSet = new Mock<DbSet<People>>();
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.Peoples).Returns(mockSet.Object);

            // Act
            var service = new PeopleService(mockContext.Object);
            service.Create(inputPeople);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<People>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }
    }
}
