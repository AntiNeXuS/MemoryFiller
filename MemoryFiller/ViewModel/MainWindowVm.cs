using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MemoryFiller.ViewModel
{
    public class MainWindowVm : ViewModelBase
    {
        private readonly double _totalRam;
        private double _availableRam;
        private int _useRamPercentage;
        private readonly PerformanceCounter _ramCounter;
        private MemoryStream _memoryFiller;

        const int gbSize = 1024 * 1024 * 1024;

        public MainWindowVm()
        {
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

            UpdateAvailableRam();
            _totalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024d / 1024d;

            UseRamPercentage = Convert.ToInt32(_availableRam / _totalRam * 100);

            RaisePropertyChanged(() => TotalRam);
            RaisePropertyChanged(() => UseRamPercentage);
        }

        private void UpdateAvailableRam()
        {
            _availableRam = Convert.ToInt32(_ramCounter.NextValue());
            RaisePropertyChanged(() => AvailableRam);
        }

        public string AvailableRam
        {
            get { return $"{_availableRam :F2} Mb / {_availableRam / 1024d :F2} Gb"; }
            set {  }
        }

        public string TotalRam
        {
            get { return $"{_totalRam :F2} Mb / {_totalRam / 1024d :F2} Gb"; }
            set {  }
        }

        public int UseRamPercentage
        {
            get { return _useRamPercentage; }
            set
            {
                _useRamPercentage = value;
                RaisePropertyChanged(() => UseRamPercentage);
            }
        }

        public ICommand GetOneGbCommand => new RelayCommand(GetOneGbExecute, GetOneGbCanExecute);

        private bool GetOneGbCanExecute()
        {
            return _memoryFiller == null && _availableRam > 1024;
        }

        private void GetOneGbExecute()
        {
            _memoryFiller = new MemoryStream(gbSize);

            UpdateAvailableRam();
        }

        public ICommand FreeOneGbCommand => new RelayCommand(FreeOneGbExecute, FreeOneGbCanExecute);

        private bool FreeOneGbCanExecute()
        {
            return _memoryFiller != null;
        }

        private void FreeOneGbExecute()
        {
            _memoryFiller.Close();
            _memoryFiller.Dispose();
            _memoryFiller = null;
            GC.Collect();

            UpdateAvailableRam();
        }
    }
}