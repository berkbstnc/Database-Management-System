using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        DataTable tablo;
        public Form3()
        {
            InitializeComponent();
        }
        void MusteriGetir()
        {
            baglanti = new SqlConnection("server =.; Initial Catalog = Opel Garage; Integrated Security = SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM EMPLOYEE", baglanti);
            tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            MusteriGetir();
            comboBox1.Items.Add("Car Mechanic");
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Accountant");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO EMPLOYEE(SSN,Ename,Salary,Position) VALUES (@SSN,@Ename,@Salary,@Position)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", textBox1.Text);
            komut.Parameters.AddWithValue("@Ename", textBox2.Text);
            komut.Parameters.AddWithValue("@Salary", textBox3.Text);
            komut.Parameters.AddWithValue("@Position", comboBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM EMPLOYEE WHERE SSN = @SSN";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", Convert.ToInt32(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE EMPLOYEE SET SSN = @SSN, Ename = @Ename, Salary = @Salary, Position = @Position WHERE SSN = @SSN";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@Ename", textBox2.Text);
            komut.Parameters.AddWithValue("@Salary", textBox3.Text);
            komut.Parameters.AddWithValue("@Position", comboBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "Ename Like '" + textBox4.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
