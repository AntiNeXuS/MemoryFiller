using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CpuMemStresser.ViewModel
{
    public class MainWindowVm : ViewModelBase
    {
        private readonly double _totalRam;
        private double _availableRam;
        private int _useRamPercentage;
        private readonly PerformanceCounter _ramCounter;

        private readonly Timer _timer;

        private Filler _filler;

        const int gbSize = 1024 * 1024 * 1024;
        const int oteSize = 128 * 1024 * 1024;

        public MainWindowVm()
        {
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

            _totalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024d / 1024d;

            _filler = new Filler(pieceSize:64*1024*1024);
            _timer = new Timer(500);
            _timer.Elapsed += (sender, args) => { UpdateUi(); };
            _timer.Start();

            UpdateUi();
        }

        private void UpdateUi()
        {
            _availableRam = Convert.ToInt32(_ramCounter.NextValue());
            RaisePropertyChanged(() => TotalRam);
            RaisePropertyChanged(() => AvailableRam);
            RaisePropertyChanged(() => ApplicationMemory);
        }

        public string AvailableRam
        {
            get { return $"{_availableRam :F2} Mb / {_availableRam / 1024d :F2} Gb"; }
            set { }
        }

        public string TotalRam
        {
            get { return $"{_totalRam :F2} Mb / {_totalRam / 1024d :F2} Gb"; }
            set { }
        }

        public string ApplicationMemory
        {
            get { return $"{_filler.UsedMemory / 1024d / 1024d :F2} Mb / {_filler.UsedMemory / 1024d / 1024d / 1024d :F2} Gb"; }
            set { }
        }

        public bool FillMemory
        {
            get { return _filler.Fill; }
            set { _filler.Fill = value; }
        }

        public ICommand IncreaseOnOneGbCommand => new RelayCommand(IncreaseOnOneGbExecute, IncreaseOnOneGbCanExecute);

        private bool IncreaseOnOneGbCanExecute()
        {
            return _filler.CanFill && _availableRam > 1024;
        }

        private void IncreaseOnOneGbExecute()
        {
            _filler.FillAsync(gbSize);
        }

        public ICommand IncreaseOn128MbCommand => new RelayCommand(IncreaseOn128MbExecute, IncreaseOn128MbCanExecute);

        private bool IncreaseOn128MbCanExecute()
        {
            return _filler.CanFill && _availableRam > 128;
        }

        private void IncreaseOn128MbExecute()
        {
            _filler.FillAsync(oteSize);
        }

        public ICommand DecreaseOnOneGbCommand => new RelayCommand(DecreaseOnOneGbExecute, DecreaseCanExecute);

        private void DecreaseOnOneGbExecute()
        {
            _filler.Empty(gbSize);
        }

        public ICommand DecreaseOn128MbCommand => new RelayCommand(DecreaseOn128MbExecute, DecreaseCanExecute);

        private void DecreaseOn128MbExecute()
        {
            _filler.Empty(oteSize);
        }

        public ICommand FreeAllCommand => new RelayCommand(FreeAllExecute, DecreaseCanExecute);

        private void FreeAllExecute()
        {
            _filler.Empty();
        }

        private bool DecreaseCanExecute()
        {
            return !_filler.IsEmpty;
        }
    }
}