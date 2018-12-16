using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {
        private int _id;
        private string _name;

        public Specialty(string name, int id = 0)
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
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@ClientName);";
            cmd.Parameters.AddWithValue("@ClientName" , this._name);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialtys = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                
                Specialty newSpecialty = new Specialty(name);
                newSpecialty.SetId(id);
                allSpecialtys.Add(newSpecialty);
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allSpecialtys;
        }


        public static void ClearAll()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialties;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
        conn.Dispose();
        }
        }

        public static void ClearAllStylists()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialties_stylists;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
        conn.Dispose();
        }
        }

       public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty)otherSpecialty;
                bool idEquality = this.GetId() == newSpecialty.GetId();
                bool nameEquality = this.GetName() == newSpecialty.GetName();
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }    

        public static Specialty Find(int id)
         {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int SpecialtyId = 0;
            string SpecialtyName = "";
            
            while(rdr.Read())
            {
                SpecialtyId = rdr.GetInt32(0);
                SpecialtyName = rdr.GetString(1);
            }

            Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSpecialty;
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN specialties_stylists ON (specialties.id = specialties_stylists.specialty_id)
                JOIN stylists ON (specialties_stylists.stylist_id = stylists.id)
                WHERE specialties.id = @specialtiesId;";
            MySqlParameter specialtiesIdParameter = new MySqlParameter();
            specialtiesIdParameter.ParameterName = "@specialtiesId";
            specialtiesIdParameter.Value = _id;
            cmd.Parameters.Add(specialtiesIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> Stylists = new List<Stylist>{};
            while(rdr.Read())
            {
            int StylistId = rdr.GetInt32(0);
            string StylistName = rdr.GetString(1);
            Stylist newStylist = new Stylist(StylistName, StylistId);
            Stylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return Stylists;
        }

    }
}