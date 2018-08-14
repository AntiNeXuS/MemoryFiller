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

            var memoryFiller = new Core.Memory.MemoryFiller(pieceSize: 64 * 1024 * 1024);

            DataContext = new MainWindowVm(memoryFiller);
        }
    }
}
