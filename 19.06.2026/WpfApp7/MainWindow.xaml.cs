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

namespace WpfApp7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _currentValue = 0;
        private double _previousValue = 0;
        private string _currentOperation = "";
        private bool _isNewNumber = true;
        private bool _operationPerformed = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (_isNewNumber || textbox1.Text == "0")
            {
                if (number == "0" && _isNewNumber)
                    return;

                textbox1.Text = number;
                _isNewNumber = false;
            }
            else
            {
                if (textbox1.Text.Length < 12)
                    textbox1.Text += number;
            }

            _operationPerformed = false;
        }
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();

            if (!_operationPerformed)
            {
                if (_currentOperation != "" && !_isNewNumber)
                {
                    Calculate();
                }

                _previousValue = double.Parse(textbox1.Text);
            }

            _currentOperation = operation;
            _isNewNumber = true;
            _operationPerformed = true;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }
        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }
        private void Calculate()
        {
            _currentValue = double.Parse(textbox1.Text);

            switch (_currentOperation)
            {
                case "+":
                    _currentValue = _previousValue + _currentValue;
                    break;
                case "-":
                    _currentValue = _previousValue - _currentValue;
                    break;
                case "*":
                    _currentValue = _previousValue * _currentValue;
                    break;
                case "/":
                    if (_currentValue != 0)
                        _currentValue = _previousValue / _currentValue;
                    else
                    {
                        MessageBox.Show("Деление на ноль невозможно!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        ClearAll();
                        return;
                    }
                    break;
            }

            textbox1.Text = _currentValue.ToString();
        }
        private void Equally_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOperation != "" && !_isNewNumber)
            {
                Calculate();
                _currentOperation = "";
                _isNewNumber = true;
                _operationPerformed = false;
            }
        }
        private void ClearAll()
        {
            textbox1.Text = "0";
            _currentValue = 0;
            _previousValue = 0;
            _currentOperation = "";
            _isNewNumber = true;
            _operationPerformed = false;
        }
    }
}
