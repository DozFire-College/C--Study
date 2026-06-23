using System.Collections.ObjectModel;
using System.Windows;

namespace ListViewExample
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> people;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            lvData.ItemsSource = people;
        }

        private void InitializeData()
        {
            people = new ObservableCollection<Person>
            {
                new Person { Id = 1, Name = "Иван Петров", City = "Москва" },
                new Person { Id = 2, Name = "Мария Иванова", City = "Санкт-Петербург" },
                new Person { Id = 3, Name = "Алексей Сидоров", City = "Казань" },
                new Person { Id = 4, Name = "Елена Смирнова", City = "Новосибирск" },
                new Person { Id = 5, Name = "Дмитрий Козлов", City = "Екатеринбург" }
            };
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItem != null)
            {
                Person selectedPerson = (Person)lvData.SelectedItem;
                label1.Content = $"ID: {selectedPerson.Id}";
                label2.Content = $"Имя: {selectedPerson.Name}";
                label3.Content = $"Город: {selectedPerson.City}";

                people.Remove(selectedPerson);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для переноса!",
                               "Предупреждение",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
            }
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}