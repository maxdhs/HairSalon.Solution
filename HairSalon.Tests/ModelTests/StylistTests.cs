using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
 
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
      Client.ClearAll();
      Specialty.ClearAllStylists();
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

     [TestMethod]
        public void Save_DatabaseAssignsIdToStylist_Id()
        {
            //Arrange
            Stylist testStylist = new Stylist("Stylist 1");
            testStylist.Save();

            //Act
            Stylist savedStylist = Stylist.GetAll()[0];

            int result = savedStylist.GetId();
            int testId = testStylist.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Save_SavesStylistToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Stylist 1");
            testStylist.Save();

            //Act
            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist> { testStylist };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

           [TestMethod]
        public void GetAll_ReturnsAllStylistObjects_StylistList()
        {
            //Arrange
            string name01 = "stylist1";
            string name02 = "stylist2";
            Stylist newStylist1 = new Stylist(name01);
            newStylist1.Save();
            Stylist newStylist2 = new Stylist(name02);
            newStylist2.Save();
            List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

            //Act
            List<Stylist> result = Stylist.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

            [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
        {
            //Arrange, Act
            Stylist firstStylist = new Stylist("stylist1");
            Stylist secondStylist = new Stylist("stylist1");

            //Assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void GetAll_StylistsEmptyAtFirst_List()
        {
            //Arrange, Act
            int result = Stylist.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void DeleteStylist_DeletesAStylist_Void()
        {
          Stylist newStylist = new Stylist("max");
          newStylist.Save();
          newStylist.DeleteStylist();
          List<Stylist> emptyList = new List<Stylist> ();
          Assert.AreEqual(emptyList.Count, Stylist.GetAll().Count);
        }

        [TestMethod]
        public void EditName_EditsName_String()
        {
          Stylist newStylist = new Stylist("Frank");
          newStylist.Save();
          newStylist.EditName("Max");
          Assert.AreEqual("Max", newStylist.GetName());
        }

        [TestMethod]
        public void AddSpecialty_AddsaSpecialtytoAStylist()
        {
          Stylist newStylist = new Stylist("Frank");
          newStylist.Save();
          Specialty newSpecialty = new Specialty("Military");
          newSpecialty.Save();
          newStylist.AddSpecialty(newSpecialty);
          Assert.AreEqual(newSpecialty, newStylist.GetSpecialties()[0]);
        }
  }
}
