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
    public partial class Form2 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        DataTable tablo;
        public Form2()
        {
            InitializeComponent();
        }

        void MusteriGetir()
        {
            baglanti = new SqlConnection("server =.; Initial Catalog = Opel Garage; Integrated Security = SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM CUSTOMER", baglanti);
            tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO CUSTOMER(SSN,Cname,Address,Phone,Plate) VALUES (@SSN,@Cname,@Address,@Phone,@Plate)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", textBox1.Text);
            komut.Parameters.AddWithValue("@Cname", textBox2.Text);
            komut.Parameters.AddWithValue("@Address", textBox3.Text);
            komut.Parameters.AddWithValue("@Phone", textBox4.Text);
            komut.Parameters.AddWithValue("@Plate", textBox5.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM CUSTOMER WHERE SSN = @SSN";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", Convert.ToInt32(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE CUSTOMER SET SSN = @SSN, Cname = @Cname, Address = @Address, Phone = @Phone, Plate = @Plate WHERE SSN = @SSN";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@SSN", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@Cname", textBox2.Text);
            komut.Parameters.AddWithValue("@Address", textBox3.Text);
            komut.Parameters.AddWithValue("@Phone", textBox4.Text);
            komut.Parameters.AddWithValue("@Plate", textBox5.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "Cname Like '" + textBox6.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
