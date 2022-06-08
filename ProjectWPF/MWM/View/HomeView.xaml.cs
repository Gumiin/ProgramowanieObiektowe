using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLitePCL;

namespace ProjectWPF.MWM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DownloadCitiesData();
            UpdateCombobox();
        }

        public class WeatherData
        {
            [JsonPropertyName("list")]
            public List<WeatherInfoormaion.FullWeather> List { get; set; }
        }
        public class WeatherInfoormaion
        {
            public class FullWeather
            {
                [JsonPropertyName("dt_txt")]
                public string DateTxt { get; set; }

                [JsonPropertyName("main")]
                public Main Main { get; set; }

                [JsonPropertyName("weather")]
                public List<Weather> Weather { get; set; }

            }
            public class Main
            {
                [JsonPropertyName("temp")]
                public double Temp { get; set; }

                [JsonPropertyName("pressure")]
                public double Pressure { get; set; }

                [JsonPropertyName("humidity")]
                public double Humidity { get; set; }
            }

            public class Weather
            {
                [JsonPropertyName("main")]
                public string Main { get; set; }

                [JsonPropertyName("description")]
                public string Description { get; set; }

                [JsonPropertyName("icon")]
                public string Icon { get; set; }
            }
        }

        Dictionary<string, City> Cities = new Dictionary<string, City>();
        public class CitiesTable
        {
            [JsonPropertyName("geonames")]
            public List<City> Geonames { get; set; }
        }

        public class City
        {
            [JsonPropertyName("capital")]
            public string Capital { get; set; }
        }
        private void DownloadCitiesData()
        {
            string json = new WebClient().DownloadString("http://api.geonames.org/countryInfoJSON?formatted=true&username=guminn99");

            CitiesTable cities = JsonSerializer.Deserialize<CitiesTable>(json);

            foreach (City c in cities.Geonames)
            {
                if (!Cities.ContainsKey(c.Capital))
                    Cities.Add(c.Capital, c);
            }

        }



        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(enterX.Text, out double x) && double.TryParse(enterY.Text, out double y))
                UpdateWeatherByCoordinates(x, y);
            else
            {
                SelectCity.Text = "Wrong Format of X or Y\n Pleas insert correct format of data";
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            string chosenCity = (string)SelectCity.SelectedItem;

            UpdateWeatherByCity(chosenCity);
        }

        private void UpdateWeatherByCity(string selectedCity)
        {
            WeatherData weatherTable = DownloadWeatherByCity(selectedCity);

            CurrentCity.Text = selectedCity;
            UpdateWeather(weatherTable);
        }

        private void UpdateWeatherByCoordinates(double x, double y)
        {
            WeatherData weatherTable = DownloadWeatherByCoordinates(x, y);

            CurrentCity.Text = $"Coordinates x: {x}, y: {y}";

            UpdateWeather(weatherTable);
        }

        private void UpdateWeather(WeatherData weatherTable)
        {
            DateTime dateNow = Convert.ToDateTime(weatherTable.List[0].DateTxt);
            var nextDay = dateNow.AddDays(1);
            var dataDictionary = new Dictionary<string, dynamic>();
            foreach (var element in weatherTable.List)
            {
                DateTime date = Convert.ToDateTime(element.DateTxt);
                if (date.Date == nextDay.Date)
                {
                    dataDictionary.Add(element.DateTxt, element);
                }
            }
            var dateList = dataDictionary.Values.ToList();
            Date0.Text = weatherTable.List[0].DateTxt;
            Date1.Text = weatherTable.List[1].DateTxt;
            Date2.Text = dateList[3].DateTxt;
            Date3.Text = dateList[5].DateTxt;
            Date4.Text = dateList[7].DateTxt;

            Image0.Source = ImgAdd(weatherTable.List[0].Weather[0].Icon);
            Image1.Source = ImgAdd(weatherTable.List[1].Weather[0].Icon);
            Image2.Source = ImgAdd(dateList[3].Weather[0].Icon);
            Image3.Source = ImgAdd(dateList[5].Weather[0].Icon);
            Image4.Source = ImgAdd(dateList[7].Weather[0].Icon);

            Status0.Text = weatherTable.List[0].Weather[0].Main.ToString();
            Status1.Text = weatherTable.List[1].Weather[0].Main.ToString();
            Status2.Text = dateList[3].Weather[0].Main.ToString();
            Status3.Text = dateList[5].Weather[0].Main.ToString();
            Status4.Text = dateList[7].Weather[0].Main.ToString();

            Temperature0.Text = weatherTable.List[0].Main.Temp.ToString() + "°C";
            Temperature1.Text = weatherTable.List[1].Main.Temp.ToString() + "°C";
            Temperature2.Text = dateList[3].Main.Temp.ToString() + "°C";
            Temperature3.Text = dateList[5].Main.Temp.ToString() + "°C";
            Temperature4.Text = dateList[7].Main.Temp.ToString() + "°C";
        }



        private static BitmapImage ImgAdd(string id)
        {
            var images = @$"http://openweathermap.org/img/wn/{id}@2x.png";

            BitmapImage bitMap = new BitmapImage();
            bitMap.BeginInit();
            bitMap.UriSource = new Uri(images, UriKind.Absolute);
            bitMap.EndInit();

            return bitMap;
        }

        private void UpdateCombobox()
        {
            foreach (string city in Cities.Keys)
            {
                if (city != "" && city != null)
                {
                    SelectCity.Items.Add(city);
                }
            }

            SelectCity.SelectedIndex = 8;
            string chosenCity = (string)SelectCity.SelectedItem;
            UpdateWeatherByCity(chosenCity);
        }

        private static WeatherData DownloadWeatherByCity(string chosenCity)
        {
            WebClient client = new WebClient();
            string cityApi = $"http://api.openweathermap.org/data/2.5/forecast?q={chosenCity}&appid=fa1186561a4ddbcc983bb31e06264c44&units=metric";
            string json = client.DownloadString(cityApi);

            WeatherData weatherTable = JsonSerializer.Deserialize<WeatherData>(json);
            return weatherTable;
        }

        private static WeatherData DownloadWeatherByCoordinates(double x, double y)
        {
            WebClient client = new WebClient();
            string apiCity = $"http://api.openweathermap.org/data/2.5/forecast?lat={x}&lon={y}&appid=fa1186561a4ddbcc983bb31e06264c44&units=metric";
            string json = client.DownloadString(apiCity);

            WeatherData weatherTable = JsonSerializer.Deserialize<WeatherData>(json);
            return weatherTable;
        }

        
    }
}
