using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Calculator
{
    public partial class CalculationHistoryWindow : Window
    {
        public ObservableCollection<Calculation> CalculationHistory { get; } = new ObservableCollection<Calculation>();
        public Calculation CurrentCalculation { get; set; } = new Calculation();

        public CalculationHistoryWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class Calculation
        {
            public int Counter { get; set; }
            public string Result { get; set; }
        }

        public void AddCalculation(Calculation calculation)
        {
            CalculationHistory.Add(calculation);

            if (CalculationHistory.Count > 5)
            {
                CalculationHistory.RemoveAt(0);
            }

            CurrentCalculation = calculation;
        }

        public void UpdateData(ObservableCollection<Calculation> calculations)
        {
            CalculationHistory.Clear();

            int counter = 1;
            foreach (var calculation in calculations)
            {
                calculation.Counter = counter++;
                CalculationHistory.Add(calculation);
            }
        }
    }
}

