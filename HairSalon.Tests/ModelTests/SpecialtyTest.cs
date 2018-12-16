using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class SpecialtyTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
      Stylist.ClearAll();
      Specialty.ClearAll();
      Specialty.ClearAllStylists();
    }

    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=maxwell_dubin_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllSpecialties_List()
    {
        Specialty newSpecialty = new Specialty("Rocker");
        Assert.AreEqual(1, Specialty.GetAll().Count);
    }

    [TestMethod]
    public void AddStylist_AddsAStylistToASpecialty()
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
