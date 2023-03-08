using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Module.AGS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtIdStation.ToString());
            if (id == 0)
            {
                MessageBox.Show("Заполните поле Station Id!");
            }
            else
                _ = GetAndDisplayStationInfo(id);
            
        }

        private async Task GetAndDisplayStationInfo(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:50682/getStationinfo/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var stationInfo = JsonConvert.DeserializeObject<StationInfo>(jsonString);
                txtAddress.Text = stationInfo.Address;
                txtPriceAI92.Text = stationInfo.PriceAI92.ToString();
                txtPriceAI95.Text = stationInfo.PriceAI95.ToString();
                txtPriceAI98.Text = stationInfo.PriceAI98.ToString();
                txtPriceDiesel.Text = stationInfo.PriceDiesel.ToString();
                txtRemainderAI92.Text = stationInfo.RemainderAI92.ToString();
                txtRemainderAI95.Text = stationInfo.RemainderAI95.ToString();
                txtRemainderAI98.Text = stationInfo.RemainderAI98.ToString();
                txtRemainderDiesel.Text = stationInfo.RemainderDiesel.ToString();
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtIdStation.Text);

            StationInfo newStationInfo = new StationInfo
            {
                StationId = id,
                Address = txtAddress.Text,
                PriceAI92 = decimal.Parse(txtPriceAI92.Text),
                PriceAI95 = decimal.Parse(txtPriceAI95.Text),
                PriceAI98 = decimal.Parse(txtPriceAI98.Text),
                PriceDiesel = decimal.Parse(txtPriceDiesel.Text),
                RemainderAI92 = int.Parse(txtRemainderAI92.Text),
                RemainderAI95 = int.Parse(txtRemainderAI95.Text),
                RemainderAI98 = int.Parse(txtRemainderAI98.Text),
                RemainderDiesel = int.Parse(txtRemainderDiesel.Text)
            };

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:50682/getStationinfo/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var originalStationInfo = JsonConvert.DeserializeObject<StationInfo>(jsonString);

                if (newStationInfo.Address != originalStationInfo.Address
                    || newStationInfo.PriceAI92 != originalStationInfo.PriceAI92
                    || newStationInfo.PriceAI95 != originalStationInfo.PriceAI95
                    || newStationInfo.PriceAI98 != originalStationInfo.PriceAI98
                    || newStationInfo.PriceDiesel != originalStationInfo.PriceDiesel
                    || newStationInfo.RemainderAI92 != originalStationInfo.RemainderAI92
                    || newStationInfo.RemainderAI95 != originalStationInfo.RemainderAI95
                    || newStationInfo.RemainderAI98 != originalStationInfo.RemainderAI98
                    || newStationInfo.RemainderDiesel != originalStationInfo.RemainderDiesel)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(newStationInfo), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"http://localhost:50682/SetStation/{id}", content);
                    await GetAndDisplayStationInfo(id);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addStation = new StationInfo()
            {
                Address = txtAddress.Text,
                PriceAI92 = decimal.Parse(txtPriceAI92.Text),
                PriceAI95 = decimal.Parse(txtPriceAI95.Text),
                PriceAI98 = decimal.Parse(txtPriceAI98.Text),
                PriceDiesel = decimal.Parse(txtPriceDiesel.Text),
                RemainderAI92 = int.Parse(txtRemainderAI92.Text),
                RemainderAI95 = int.Parse(txtRemainderAI95.Text),
                RemainderAI98 = int.Parse(txtRemainderAI98.Text),
                RemainderDiesel = int.Parse(txtRemainderDiesel.Text)
            };

            string url = "http://localhost:50682/AddStation";
           
            
        }
    }
}