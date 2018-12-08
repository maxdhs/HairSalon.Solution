using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
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

  }
}
