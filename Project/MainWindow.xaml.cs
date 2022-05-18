using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputCurrencyCode.Items.Add("USD");
            InputCurrencyCode.Items.Add("PLN");
            InputCurrencyCode.Items.Add("EUR");
            ResultCurrencyCode.Items.Add("USD");
            ResultCurrencyCode.Items.Add("PLN");
            ResultCurrencyCode.Items.Add("EUR");
            InputCurrencyCode.SelectedIndex = 1;
            ResultCurrencyCode.SelectedIndex = 0;
        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            string inputCode = (string)InputCurrencyCode.SelectedItem;
            string resultCode = (string)ResultCurrencyCode.SelectedItem;
            string amountStr = InputValue.Text;
            MessageBox.Show($"Wybrany kod wejściowy {inputCode}\nwybrany kod wyjściowy {resultCode}\nKwota: {amountStr}");
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.EndsWith(","))
            {
                string ee = e.Text + "0";
                e.Handled = !decimal.TryParse(ee, out decimal value);
            }
            else
            {
                e.Handled = !decimal.TryParse(e.Text, out decimal value);
            }
        }
    }
}
