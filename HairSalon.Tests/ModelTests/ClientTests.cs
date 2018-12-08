using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
      Stylist.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=maxwell_dubin_test;";
    }

     [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("test", 2);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Client newClient = new Client(description, 2);

      //Act
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

     [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("Mow the lawn", 2);
      Client secondClient = new Client("Mow the lawn", 2);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

      [TestMethod]
        public void GetAll_ReturnsClients_ClientList()
        {
            //Arrange
            string name01 = "client1";
            string name02 = "client2";
            Client newClient1 = new Client(name01, 1);
            newClient1.Save();
            Client newClient2 = new Client(name02, 1);
            newClient2.Save();
            List<Client> newList = new List<Client> { newClient1, newClient2 };

            //Act
            List<Client> result = Client.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

         [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("savannah", 1);

            //Act
            testClient.Save();
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client> { testClient };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //Arrange
            Client testClient = new Client("savannah", 1);

            //Act
            testClient.Save();
            Client savedClient = Client.GetAll()[0];

            int result = savedClient.GetId();
            int testId = testClient.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

  }
}
