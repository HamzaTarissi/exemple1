﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBmovie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string chaine = @"Data Source=PC-HAMZA;Initial Catalog=Movies;Integrated Security=True";
        static SqlConnection cnx = new SqlConnection(chaine);
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        private void buttinsert_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection("Data Source=PC-HAMZA;Initial Catalog=Movies;Integrated Security=True");
            cnx.Open();
            SqlCommand cmd = new SqlCommand("insert into Movie values (@id,@nom)", cnx);
            cmd.Parameters.AddWithValue("@id", int.Parse(textid_movie.Text));
            cmd.Parameters.AddWithValue("@Nom", (textnom_movie.Text));
            cmd.ExecuteNonQuery();
            cnx.Close();
            MessageBox.Show("Successfully Saved");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillcombobox();
        }
        private void fillcombobox()
        {
            combobox.Items.Clear();
            cnx.Open(); 
            SqlCommand cmd = new SqlCommand();
            cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select nom from Movie";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                combobox.Items.Add(dr["nom"].ToString());
            }
            cnx.Close();
        }

        private void buttupdate_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Movies set nom='"+ textnom_movie + "' where id='"+ textid_movie +"'";
            cmd.ExecuteNonQuery();
            cnx.Close();
            combobox.Text = "";
            textid_movie.Text = "";
            textnom_movie.Text = "";
        }

        private void buttdelete_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Movie where id='"+textid_movie+"'";
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttvalidate_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Movie values (@id,@nom)";
            cmd.Parameters.AddWithValue("@id", int.Parse(textid_movie.Text));
            cmd.Parameters.AddWithValue("@Nom", (textnom_movie.Text));
            cmd.CommandText = "delete from Movie where id='" + textid_movie + "'";
            cmd.CommandText = "update Movies set nom='" + textnom_movie + "' where id='" + textid_movie + "'";
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

        private void buttsearch_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType= CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter("Select From Movie where nom='"+textnom_movie+"'",cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textnom_movie.Text = dt.Rows[0][1].ToString();
            cnx.Close();
        }

        private void buttcancel_Click(object sender, EventArgs e)
        {
            textid_movie.Text = "";
            textnom_movie.Text = "";
            combobox.Text = "";
        }
    }
}