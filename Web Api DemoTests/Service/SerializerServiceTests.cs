using Web_Api_Demo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Web_Api_Demo.Service.Interface;
using Web_Api_Demo.Models.DTO;
using System;
using System.Diagnostics;
using System.IO;

namespace Web_Api_Demo.Service.Tests
{
    [TestClass()]
    public class SerializerServiceTests
    {
        [TestMethod()]
        public void CreateXmlTest()
        {
            string xmlName = "Opp";
            string xmlName1 = "AddOpp";
            var mock = new Mock<IOoportunityService>();
            mock.Setup(m => m.GetOpportunities()).Returns(new List<OpportunityDTO> {
                new OpportunityDTO
                {
                    OpportunityId = "1"
                }
            });
            SerializerService service = new SerializerService(mock.Object);
            Assert.IsTrue(service.CreateXml(xmlName));
            Assert.IsTrue(service.CreateXml(xmlName1));
        }

        [TestMethod()]
        public void GetXmlNameTest()
        {
            string xmlName = "Opp";
            string xmlName1 = "AddOpp";
            var mock = new Mock<IOoportunityService>();
            mock.Setup(m => m.GetOpportunities()).Returns(new List<OpportunityDTO> {
                new OpportunityDTO
                {
                    OpportunityId = "1"
                }
            });
            SerializerService service = new SerializerService(mock.Object);
            service.CreateXml(xmlName);
            service.CreateXml(xmlName1);
            Assert.AreEqual("AddOpp.xml", service.GetXmlName()[0]);
            Assert.AreEqual("Opp.xml", service.GetXmlName()[1]);
        }

        [TestMethod()]
        public void DeleteXmlTest()
        {
            string xmlName = "AddOpp.xml";
            string xmlName1 = "Opp.xml";
            ISerializerService service = new SerializerService(new OoportunityService());
            Assert.IsTrue(service.DeleteXml(xmlName));
            Assert.IsTrue(service.DeleteXml(xmlName1));
        }

        [TestMethod()]
        public void ReadXmlTest()
        {
            string xmlName = "Opp";
            string xmlName1 = "AddOpp";
            var mock = new Mock<IOoportunityService>();
            mock.Setup(m => m.GetOpportunities()).Returns(new List<OpportunityDTO> {
                new OpportunityDTO
                {
                    OpportunityId = "1"
                }
            });
            SerializerService service = new SerializerService(mock.Object);
            service.CreateXml(xmlName);
            service.CreateXml(xmlName1);
            Assert.AreEqual("", service.ReadXml(xmlName));
            Assert.AreEqual("", service.ReadXml(xmlName1));
        }

        [TestMethod()]
        public void GetXmlInfoTest()
        {
            string localPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Xml\";
            string xmlName = "Opp";
            string xmlName1 = "AddOpp";
            var mock = new Mock<IOoportunityService>();
            mock.Setup(m => m.GetOpportunities()).Returns(new List<OpportunityDTO> {
                new OpportunityDTO
                {
                    OpportunityId = "1"
                }
            });
            SerializerService service = new SerializerService(mock.Object);
            service.CreateXml(xmlName);
            service.CreateXml(xmlName1);
            Assert.AreEqual(localPath + xmlName1 + ".xml", service.GetXmlInfo()[0]);
            Assert.AreEqual(localPath + xmlName + ".xml", service.GetXmlInfo()[1]);
        }

        [TestMethod()]
        public void OpenPathTest()
        {
            string localPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Xml\";
            string xmlName = "Opp";
            string xmlName1 = "AddOpp";
            var mock = new Mock<IOoportunityService>();
            mock.Setup(m => m.GetOpportunities()).Returns(new List<OpportunityDTO> {
                new OpportunityDTO
                {
                    OpportunityId = "1"
                }
            });
            SerializerService service = new SerializerService(mock.Object);
            service.CreateXml(xmlName);
            service.CreateXml(xmlName1);
            service.OpenPath(localPath + xmlName + ".xml");
            service.OpenPath(localPath + xmlName1 + ".xml");
            Assert.IsNotNull(localPath + xmlName + ".xml");
        }

        [TestMethod()]
        public void UploadTest()
        {
            SerializerService service = new SerializerService(new OoportunityService());
            Assert.IsFalse(service.Upload());
        }

        [TestMethod()]
        public void GetUploadInfoTest()
        {
            SerializerService service = new SerializerService(new OoportunityService());
            Assert.AreEqual(new List<string> { "New Text Document (2).txt" }[0], service.GetUploadInfo()[0]);
        }

        [TestMethod()]
        public void DownLoadTest()
        {
            string fileName = "New Text Document (2).txt";
            SerializerService service = new SerializerService(new OoportunityService());
            service.DownLoad(fileName);
            Assert.IsNotNull(fileName);
        }

        [TestMethod()]
        public void FileDetailsTest()
        {
            string fileName = "New Text Document (2).txt";
            SerializerService service = new SerializerService(new OoportunityService());
            Assert.AreEqual(new List<string> { ".txt", "0KB", "11/7/2019 11:25:04 AM" }[0], service.FileDetails(fileName)[0]);
            Assert.AreEqual(new List<string> { ".txt", "0KB", "11/7/2019 11:25:04 AM" }[1], service.FileDetails(fileName)[1]);
            Assert.AreEqual(new List<string> { ".txt", "0KB", "11/7/2019 11:25:04 AM" }[2], service.FileDetails(fileName)[2]);
        }
    }
}