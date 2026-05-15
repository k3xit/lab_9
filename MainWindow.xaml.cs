using lab_9;
using System;
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


namespace lab_9
{
    public partial class MainWindow : Window
    {
        private Time _currentTime;

        public MainWindow()
        {
            InitializeComponent();
            _currentTime = new Time();
            UpdateUI();
        }

        private void UpdateUI(string message = "Готов к работе",
            bool isError = false)
        {
            LblCurrentTime.Text = _currentTime.ToString();
            TxtStatus.Text = message;
            TxtStatus.Foreground = isError ? Brushes.Red : Brushes.DimGray;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (byte.TryParse(TxtHours.Text, out byte h)
                    && byte.TryParse(TxtMinutes.Text, out byte m))
                {
                    if (h < 24 && m < 60)
                    {
                        _currentTime = new Time(h, m);
                        UpdateUI("Время успешно установлено");
                    }
                    else
                    {
                        UpdateUI("Ошибка: Часы 0-23, Минуты 0-59", true);
                    }
                }
                else
                {
                    UpdateUI("Ошибка: Введите числовые значения", true);
                }
            }
            catch (Exception ex)
            {
                UpdateUI(ex.Message, true);
            }
        }

        private void BtnInc_Click(object sender, RoutedEventArgs e)
        {
            _currentTime++;
            UpdateUI("Добавлена 1 минута");
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            _currentTime--;
            UpdateUI("Вычтена 1 минута");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (uint.TryParse(TxtValue.Text, out uint mins))
            {
                _currentTime += mins;
                UpdateUI($"Добавлено минут: {mins}");
            }
            else
            {
                UpdateUI("Некорректное число минут", true);
            }
        }

        private void BtnSub_Click(object sender, RoutedEventArgs e)
        {
            if (uint.TryParse(TxtValue.Text, out uint mins))
            {
                _currentTime -= mins;
                UpdateUI($"Вычтено минут: {mins}");
            }
            else
            {
                UpdateUI("Некорректное число минут", true);
            }
        }

        private void BtnToByte_Click(object sender, RoutedEventArgs e)
        {
            byte h = (byte)_currentTime;
            MessageBox.Show($"Значение часов (явное приведение):" +
                $" {h}", "Инфо");
        }

        private void BtnToBool_Click(object sender, RoutedEventArgs e)
        {
            bool notZero = _currentTime;
            string msg = notZero ? "Время не 00:00"
                : "На часах полночь (00:00)";
            MessageBox.Show(msg, "Проверка (bool)");
        }
    }
}