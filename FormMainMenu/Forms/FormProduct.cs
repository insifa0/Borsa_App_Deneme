using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormMainMenu.Forms
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }

        // SQL bağlantısını kurma
        private void Connection()
        {
            // SQL Server veritabanına bağlantı dizesi
            string connectionString = @"Data Source=DESKTOP-32GSEVA\SQLEXPRESS;Initial Catalog=denemeApp;Integrated Security=True";

            // SqlConnection nesnesi oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // Bağlantı başarılı
                    //MessageBox.Show("Bağlantı başarılı!");

                    // listView1'i temizle
                    listView1.Items.Clear();

                    // SQL sorgusunu çalıştır
                    SqlCommand komut = new SqlCommand("SELECT * FROM denemeStocks", connection);
                    SqlDataReader reader = komut.ExecuteReader();

                    // Verileri listView1'e ekle
                    while (reader.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = reader["Stock name"].ToString();
                        ekle.SubItems.Add(reader["Stock Price"].ToString());
                        ekle.SubItems.Add(reader["Hisse adeti"].ToString());
                        listView1.Items.Add(ekle);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    // Bağlantı sırasında bir hata oluştu
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
        }

        private void justOpenConnection(){

            string connectionString = @"Data Source=DESKTOP-32GSEVA\SQLEXPRESS;Initial Catalog=denemeApp;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Bağlantıyı aç
                connection.Open();
                MessageBox.Show("Bağlantı başarılı!");

                // Parametreli SQL sorgusunu kullan
                string insertQuery = "INSERT INTO denemeStocks ([Stock name], [Stock Price], [Hisse adeti]) VALUES (@StockName, @StockPrice, @HisseAdeti)";
                
                using (SqlCommand komut = new SqlCommand(insertQuery, connection))
                {
                    // Parametre değerlerini ata
                    komut.Parameters.AddWithValue("@StockName", textBox1.Text);
                    komut.Parameters.AddWithValue("@StockPrice", textBox2.Text);
                    komut.Parameters.AddWithValue("@HisseAdeti", textBox3.Text);

                    // Sorguyu çalıştır
                    komut.ExecuteNonQuery();
                }

                Connection(); // Veriyi güncelle
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                // Bağlantı sırasında bir hata oluştu
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
        }
        


        private void refleshStocks_Click(object sender, EventArgs e)
        {
            Connection();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler buraya eklenebilir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ekleme butonu
            justOpenConnection();

        }
    }
}
