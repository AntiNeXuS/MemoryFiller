using System.Windows;
using CpuMemStresser.ViewModel;

namespace CpuMemStresser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVm();
        }
    }
}
