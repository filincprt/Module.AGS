using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Module.AGS
{
    public partial class MainWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private int currentStationID = 0;

        public MainWindow()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            var stations = new List<int>() { 1, 2, 3, 5, 6, 7, 8, 9, 10}; // пример значений station_id
            foreach (var station in stations)
            {
                cmbStationID.Items.Add(station);
            }
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            int stationID = int.Parse(cmbStationID.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM gas_stations WHERE station_id = @stationID";
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@stationID", stationID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtAddress.Text = reader["address"].ToString();
                            txtPriceAI92.Text = reader["price_AI92"].ToString();
                            txtPriceAI95.Text = reader["price_AI95"].ToString();
                            txtPriceAI98.Text = reader["price_AI98"].ToString();
                            txtPriceDiesel.Text = reader["price_diesel"].ToString();
                            txtRemainderAI92.Text = reader["remainder_AI92"].ToString();
                            txtRemainderAI95.Text = reader["remainder_AI95"].ToString();
                            txtRemainderAI98.Text = reader["remainder_AI98"].ToString();
                            txtRemainderDiesel.Text = reader["remainder_diesel"].ToString();
                            currentStationID = stationID;
                        }
                        else
                        {
                            MessageBox.Show("Station not found");
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = "UPDATE ags_stations SET address=@address, price_AI92=@price_AI92, price_AI95=@price_AI95, price_AI98=@price_AI98, price_diesel=@price_diesel, remainder_AI92=@remainder_AI92, remainder_AI95=@remainder_AI95, remainder_AI98=@remainder_AI98, remainder_diesel=@remainder_diesel WHERE station_id=@stationID";
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@address", txtAddress.Text);
                    command.Parameters.AddWithValue("@price_AI92", float.Parse(txtPriceAI92.Text));
                    command.Parameters.AddWithValue("@price_AI95", float.Parse(txtPriceAI95.Text));
                    command.Parameters.AddWithValue("@price_AI98", float.Parse(txtPriceAI98.Text));
                    command.Parameters.AddWithValue("@price_diesel", float.Parse(txtPriceDiesel.Text));
                    command.Parameters.AddWithValue("@remainder_AI92", int.Parse(txtRemainderAI92.Text));
                    command.Parameters.AddWithValue("@remainder_AI95", int.Parse(txtRemainderAI95.Text));
                    command.Parameters.AddWithValue("@remainder_AI98", int.Parse(txtRemainderAI98.Text));
                    command.Parameters.AddWithValue("@remainder_diesel", int.Parse(txtRemainderDiesel.Text));
                    command.Parameters.AddWithValue("@stationID", currentStationID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Changes saved");
                    }
                    else
                    {
                        MessageBox.Show("No changes were made");
                    }
                }
            }
        }
    }
}