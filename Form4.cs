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
    public partial class Form4 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        DataTable tablo;
        public Form4()
        {
            InitializeComponent();
        }
        void MusteriGetir()
        {
            baglanti = new SqlConnection("server =.; Initial Catalog = Opel Garage; Integrated Security = SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM CAR ", baglanti);
            tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO CAR(Plate, Model, Brand, Engine, Trunk, Wheels, Glasses, Doors, Buffers, Color) VALUES (@Plate, @Model, @Brand, @Engine, @Trunk, @Wheels, @Glasses, @Doors, @Buffers, @Color)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Plate", textBox1.Text);
            komut.Parameters.AddWithValue("@Model", textBox2.Text);
            komut.Parameters.AddWithValue("@Brand", textBox3.Text);
            komut.Parameters.AddWithValue("@Engine", textBox4.Text);
            komut.Parameters.AddWithValue("@Trunk", textBox5.Text);
            komut.Parameters.AddWithValue("@Wheels", textBox6.Text);
            komut.Parameters.AddWithValue("@Glasses", textBox7.Text);
            komut.Parameters.AddWithValue("@Doors", textBox8.Text);
            komut.Parameters.AddWithValue("@Buffers", textBox9.Text);
            komut.Parameters.AddWithValue("@Color", textBox10.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM CAR WHERE Plate = @Plate";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Plate", Convert.ToString(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE CAR SET Plate = @Plate, Model = @Model, Brand = @Brand, Engine = @Engine, Trunk = @Trunk, Wheels = @Wheels, Glasses = @Glasses, Doors = @Doors, Buffers = @Doors, Color = @Color WHERE Plate = @Plate";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Plate", textBox1.Text);
            komut.Parameters.AddWithValue("@Model", textBox2.Text);
            komut.Parameters.AddWithValue("@Brand", textBox3.Text);
            komut.Parameters.AddWithValue("@Engine", textBox4.Text);
            komut.Parameters.AddWithValue("@Trunk", textBox5.Text);
            komut.Parameters.AddWithValue("@Wheels", textBox6.Text);
            komut.Parameters.AddWithValue("@Glasses", textBox7.Text);
            komut.Parameters.AddWithValue("@Doors", textBox8.Text);
            komut.Parameters.AddWithValue("@Buffers", textBox9.Text);
            komut.Parameters.AddWithValue("@Color", textBox10.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "Plate Like '" + textBox11.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
