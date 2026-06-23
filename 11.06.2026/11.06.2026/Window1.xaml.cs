using System.Windows;
using System.Windows.Input;

namespace ScreamerApp
{
    public partial class ScreamerWindow : Window
    {
        public ScreamerWindow()
        {
            InitializeComponent();  
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        { 
            this.Close();
        }
    }
}