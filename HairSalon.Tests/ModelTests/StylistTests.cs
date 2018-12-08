using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=maxwell_dubin_test;";
    }

    public void Dispose()
    {
    Stylist.ClearAll();
    }

     [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    
    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Category";
      Stylist newStylist = new Stylist(name);

      //Act
      string result = newStylist.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

     [TestMethod]
    public void GetId_ReturnsCategoryId_Int()
    {
      //Arrange
      string name = "Test Category";
      Stylist newStylist = new Stylist(name);

      //Act
      int result = newStylist.GetId();

      //Assert
      Assert.AreEqual(0, result);
    }
 
  }
}
