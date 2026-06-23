using System.Collections.Generic;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private HashSet<string> deletedItems = new HashSet<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string newItem = InputTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newItem))
            {
                MessageBox.Show("Введите текст для добавления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (deletedItems.Contains(newItem))
            {
                MessageBox.Show($"Элемент \"{newItem}\" был ранее удален и не может быть добавлен снова!",
                    "Нельзя", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (MainListBox.Items.Contains(newItem))
            {
                MessageBox.Show("Такой элемент уже существует в списке!",
                    "Дубликат", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MainListBox.Items.Add(newItem);
            InputTextBox.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для удаления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedItem = MainListBox.SelectedItem.ToString();
            MainListBox.Items.Remove(selectedItem);
            deletedItems.Add(selectedItem);
        }
    }
}