using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFilePath = string.Empty;
        private string currentFileContent = string.Empty;
        private Encoding currentEncoding = Encoding.UTF8;

        public MainWindow()
        {
            InitializeComponent();

            // Разрешаем перетаскивание файлов на окно
            this.AllowDrop = true;
            this.DragEnter += MainWindow_DragEnter;
            this.Drop += MainWindow_Drop;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите текстовый файл",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
                LoadFile(openFileDialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text.Trim();

            if (string.IsNullOrEmpty(filePath) || filePath.Contains("Выберите текстовый файл"))
            {
                MessageBox.Show("Пожалуйста, выберите файл для загрузки.",
                              "Предупреждение",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл не найден:\n{filePath}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return;
            }

            LoadFile(filePath);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = "Здесь будет отображаться содержимое файла...";
            FilePathTextBox.Text = "Выберите текстовый файл или перетащите его сюда...";
            currentFileContent = string.Empty;
            currentFilePath = string.Empty;
            this.Title = "Просмотр текстовых файлов";
        }

        private void LoadFile(string filePath)
        {
            try
            {
                currentEncoding = DetectEncoding(filePath);

 
                currentFileContent = File.ReadAllText(filePath, currentEncoding);


                TextDisplay.Text = currentFileContent;
                currentFilePath = filePath;

                UpdateStats();

                this.Title = $"Просмотр текстовых файлов - {Path.GetFileName(filePath)}";

                TextDisplay.ScrollToHome();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Нет доступа к файлу. Проверьте права доступа.",
                              "Ошибка доступа",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка при чтении файла:\n{ex.Message}",
                              "Ошибка ввода-вывода",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка:\n{ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private Encoding DetectEncoding(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath, Encoding.Default, true))
                {
                    reader.Peek(); 
                    return reader.CurrentEncoding;
                }
            }
            catch
            {
                return Encoding.UTF8;
            }
        }

        private void UpdateStats()
        {
            

            int charCount = currentFileContent.Length;
            int lineCount = currentFileContent.Split('\n').Length;
            int wordCount = currentFileContent.Split(new[] { ' ', '\n', '\r', '\t' },
                                                    StringSplitOptions.RemoveEmptyEntries).Length;

            long fileSize = 0;
            if (File.Exists(currentFilePath))
            {
                FileInfo fileInfo = new FileInfo(currentFilePath);
                fileSize = fileInfo.Length;
            }

            string sizeText = FormatFileSize(fileSize);

            
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;     
                FilePathTextBox.Text = "Отпустите файл для загрузки...";
                FilePathTextBox.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(108, 99, 255));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void MainWindow_DragLeave(object sender, DragEventArgs e)
        { 
            if (string.IsNullOrEmpty(currentFilePath))
            {
                FilePathTextBox.Text = "Выберите текстовый файл или перетащите его сюда...";
            }
            else
            {
                FilePathTextBox.Text = currentFilePath;
            }
            FilePathTextBox.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromRgb(205, 214, 244));
        }

        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length > 0)
                {
                    string filePath = files[0];

                    if (IsTextFile(filePath))
                    {
                        FilePathTextBox.Text = filePath;
                        LoadFile(filePath);
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, перетащите текстовый файл.\n" +
                                      "Поддерживаемые форматы: .txt, .log, .csv, .xml, .json, .html и др.",
                                      "Неверный формат файла",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Warning);
                        FilePathTextBox.Text = "Выберите текстовый файл или перетащите его сюда...";
                    }
                }
            }
        }

        private bool IsTextFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            string[] textExtensions = {
                ".txt", ".log", ".csv", ".xml", ".json", ".html",
                ".htm", ".css", ".js", ".cs", ".py", ".java",
                ".cpp", ".h", ".ini", ".cfg", ".config", ".md",
                ".sql", ".php", ".asp", ".aspx", ".xaml", ".svg",
                ".yaml", ".yml", ".properties", ".bat", ".sh", ".ps1"
            };

            return Array.Exists(textExtensions, ext => ext == extension);
        }
    }
}