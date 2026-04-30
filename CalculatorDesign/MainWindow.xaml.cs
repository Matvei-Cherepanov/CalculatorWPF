using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorDesign
{
    public partial class MainWindow : Window
    {
        Calculator calc = new Calculator();
        public MainWindow()
        {
            InitializeComponent();
            ResultText.Text = "0";
            HistoryText.Text = string.Empty;
        }
        private string delete_symbol(string text)
        {
            string CurrentText = text;
            string NewText = "";
            for (int i = 0; i < CurrentText.Length - 1; i++)
                NewText += CurrentText[i];
            return NewText;
        }

        private void WriteButton(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (ResultText.Text == "0")
                ResultText.Text = btn.Content.ToString();
            else if (!calc.operators.Contains(btn.Content.ToString()))
            {
                ResultText.Text += btn.Content.ToString();
            }
            else
            {
                int len = ResultText.Text.Length;
                if (calc.operators.Contains(ResultText.Text[len - 1].ToString()))
                {
                    string NewText = delete_symbol(ResultText.Text) + btn.Content.ToString();
                    ResultText.Text = NewText;
                }
                else
                {
                    ResultText.Text += btn.Content.ToString();
                }
            }
        }

        private void ClaerButton(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "0";
            HistoryText.Text = String.Empty;
        }

        private void DelButton(object sender, RoutedEventArgs e)
        {
            if(ResultText.Text != "0")
            {
                if (ResultText.Text.Length == 1)
                    ResultText.Text = "0";
                else
                    ResultText.Text = delete_symbol(ResultText.Text);
            }
        }

        private void EqualButton(object sender, RoutedEventArgs e)
        {
            string ans = calc.Calculate(ResultText.Text).ToString();
            HistoryText.Text = ResultText.Text;
            ResultText.Text = ans;
        }
    }
}