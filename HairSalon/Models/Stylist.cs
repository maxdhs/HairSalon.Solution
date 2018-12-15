using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
        private string _name;
        private int _id;
       

        public Stylist(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public void SetId(int id)
        {
          _id = id;  
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";
            cmd.Parameters.AddWithValue("@StylistName" , this._name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                newStylist.SetId(id);
                allStylists.Add(newStylist);
                
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static List<Stylist> Find(int stylistId)
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = " + stylistId + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                allStylists.Add(newStylist);
                
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

         public static void ClearAll()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
        conn.Dispose();
        }
        }

        public override bool Equals(System.Object otherStylist)
        {
        if (!(otherStylist is Stylist))
        {
            return false;
        }
        else
        {
            Stylist newStylist = (Stylist) otherStylist;
            bool idEquality = this.GetId().Equals(newStylist.GetId());
            bool nameEquality = this.GetName().Equals(newStylist.GetName());
            return (idEquality && nameEquality);
        }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public void DeleteStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE (id) = (@id);";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@id";
            id.Value = this.GetId();
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }    

    public void AddSpecialty(Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties_stylists (stylist_id, specialty_id) VALUES (@StylistsId, @SpecialtyId);";
            
            MySqlParameter Stylists_id = new MySqlParameter();
            Stylists_id.ParameterName = "@StylistsId";
            Stylists_id.Value = _id;
            cmd.Parameters.Add(Stylists_id);

            MySqlParameter Specialty_id = new MySqlParameter();
            Specialty_id.ParameterName = "@SpecialtyId";
            Specialty_id.Value = newSpecialty.GetId();
            cmd.Parameters.Add(Specialty_id);
        
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public List<Specialty> GetSpecialties()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
                JOIN specialties_stylists ON (stylists.id = specialties_stylists.stylist_id)
                JOIN specialties ON (specialties_stylists.specialty_id = specialties.id)
                WHERE stylists.id = @stylistsId;";
            MySqlParameter stylistsIdParameter = new MySqlParameter();
            stylistsIdParameter.ParameterName = "@stylistsId";
            stylistsIdParameter.Value = _id;
            cmd.Parameters.Add(stylistsIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Specialty> specialties = new List<Specialty>{};
            while(rdr.Read())
            {
            int SpecialtyId = rdr.GetInt32(0);
            string SpecialtyName = rdr.GetString(1);
            Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
            specialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return specialties;
        }

    }
}