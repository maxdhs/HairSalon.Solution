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

    }
}