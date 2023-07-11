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
using System.Data;
using System.Reflection.Emit;
using static Calculator.CalculationHistoryWindow;
using System.Collections.ObjectModel;

namespace Calculator
{
    public partial class MainWindow : Window
    {

        private CalculationHistoryWindow calculationHistoryWindow;
        private ObservableCollection<Calculation> calculations = new ObservableCollection<Calculation>();
        private int calculationCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement el in Calculator.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content;
                if (str == "Clear")
                    Lab1.Text = "";

                else if (str == "=")
                {
                    string data = new DataTable().Compute(Lab1.Text, null).ToString();
                    Lab1.Text = data;
                    UpdateCalculationHistory(data);
                }
                else if (str == "<<")
                {
                    if (Lab1.Text.Length > 0)
                        Lab1.Text = Lab1.Text.Substring(0, Lab1.Text.Length - 1);
                }
                else if (str == "History")
                    Lab1.Text = "";
                else
                    Lab1.Text += str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateCalculationHistory(string result)
        {
            Calculation calculation = new Calculation { Counter = calculationCounter, Result = result };
            calculations.Insert(0, calculation);

            if (calculations.Count > 5)
            {
                calculations.RemoveAt(5);
            }

            calculationCounter = Math.Min(calculationCounter + 1, 5);

            if (calculationHistoryWindow != null && calculationHistoryWindow.IsVisible)
            {
                calculationHistoryWindow.UpdateData(calculations);
            }
        }
        private void ShowCalculationHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (calculationHistoryWindow == null || !calculationHistoryWindow.IsVisible)
            {
                calculationHistoryWindow = new CalculationHistoryWindow();
                calculationHistoryWindow.UpdateData(calculations);
                calculationHistoryWindow.Show();
            }
        }
    }
}
