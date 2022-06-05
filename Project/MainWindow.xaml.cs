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
using System.Xml.Linq;

namespace App_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        record Rate()
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            [JsonPropertyName ("currency")]
            public string Currency { get; set; } 
            [JsonPropertyName ("bid")]
            public decimal Bid { get; set; } 
            [JsonPropertyName ("asks")]
            public decimal Ask { get; set; }

        };
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();

        class RateTable
        {
            [JsonPropertyName("table")]
            public string Table { get; set; }
            [JsonPropertyName("no")]
            public string Number { get; set; }
            [JsonPropertyName("tradingDate")]
            public DateTime tradingDate { get; set; }
            [JsonPropertyName("effectiveDate")]
            public DateTime efectiveDate { get; set; }
            [JsonPropertyName("rates")]
            public List<Rate> Rates { get; set; }
        }
        private void DownloadDateJson()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("https://api.nbp.pl/api/exchangerates/tables/C");
            RateTable rateTable = JsonSerializer.Deserialize<List<RateTable>>(json)[0];
            rateTable.Rates.Add(new Rate() {Code="PLN",Currency="złoty",Ask=1, Bid=1});
            foreach(Rate r in rateTable.Rates)
            {
                Rates.Add(r.Code, r);
            }
        }
        private void DownloadData()
        {
            //stworzyc kolekcje rates
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xml = client.DownloadString("https://api.nbp.pl/api/exchangerates/tables/C");
            XDocument doc = XDocument.Parse(xml);
            List<Rate> list = doc
                .Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(node => new Rate()
                {
                    Code = node.Element("Code").Value,
                    Currency = node.Element("Currency").Value,
                    Bid = decimal.Parse(node.Element("Bid").Value),
                    Ask = decimal.Parse(node.Element("Ask").Value)
                }).ToList();

            foreach (Rate r in list)
            {
                Rates.Add(r.Code, r);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DownloadDateJson();

            DownloadData();
            //dodawanie do listy
            foreach (string code in Rates.Keys)
            {
                InputCurrencyCode.Items.Add(code);
                ResultCurrencyCode.Items.Add(code);
            }
            //InputCurrencyCode.Items.Add("USD");
            //InputCurrencyCode.Items.Add("PLN");
            //InputCurrencyCode.Items.Add("EUR");
            //ResultCurrencyCode.Items.Add("USD");
            //ResultCurrencyCode.Items.Add("PLN");
            //ResultCurrencyCode.Items.Add("EUR");

            //ustawienie wartosci domyslnej
            ResultCurrencyCode.SelectedIndex = 0;
            InputCurrencyCode.SelectedIndex = 1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            //kod reagujacy na klikniecie
            //obliczenie kwoty dla ResultValue
            string inputCode = (string)InputCurrencyCode.SelectedItem;
            string resultCode = (string)ResultCurrencyCode.SelectedItem;
            string amountStr = InputValue.Text;
            if(decimal.TryParse(amountStr, out decimal amount))
            {
                ResultValue.Text = (amount * Rates[inputCode].Bid / Rates[resultCode].Bid).ToString("N2");
            }
            //MessageBox.Show($"Wybrany kod wejściowy: {inputCode}\rWybrany kod wyjściowy: {resultCode}\nKwota: {amountStr}");
        }

        private void InputValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(InputValue.Text + e.Text, out decimal value);
        }
    }
}
