using System.Timers;
using System.Windows.Input;
using CpuMemStresser.Core;
using CpuMemStresser.Core.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CpuMemStresser.ViewModel
{
    public class MainWindowVm : ViewModelBase
    {
        private readonly double _totalRam;
        private double _availableRam;
        private int _useRamPercentage;

        private readonly Timer _timer;

        private readonly IComputerMemoryProvider _computerMemoryProvider;
        private readonly IMemoryFiller _memoryFiller;
        
        const int gbSize = 1024 * 1024 * 1024;
        const int oteSize = 128 * 1024 * 1024;

        public MainWindowVm(IComputerMemoryProvider computerMemoryProvider, IMemoryFiller memoryFiller)
        {
            _computerMemoryProvider = computerMemoryProvider;

            _totalRam = computerMemoryProvider.TotalMemorySize;

            _memoryFiller = memoryFiller;
            _timer = new Timer(500);
            _timer.Elapsed += (sender, args) => { UpdateUi(); };
            _timer.Start();

            UpdateUi();
        }

        private void UpdateUi()
        {
            _availableRam = _computerMemoryProvider.AvailableMemorySize;
            RaisePropertyChanged(() => TotalRam);
            RaisePropertyChanged(() => AvailableRam);
            RaisePropertyChanged(() => ApplicationMemory);
        }

        public string TotalRam
        {
            get { return $"{_totalRam.ToMb() :F2} Mb / {_totalRam.ToGb() :F2} Gb"; }
            set { }
        }

        public string AvailableRam
        {
            get { return $"{_availableRam.ToMb() :F2} Mb / {_availableRam.ToGb() :F2} Gb"; }
            set { }
        }

        public string ApplicationMemory
        {
            get { return $"{_memoryFiller.UsedMemory.ToMb() :F2} Mb / {_memoryFiller.UsedMemory.ToGb() :F2} Gb"; }
            set { }
        }

        public bool FillMemory
        {
            get { return _memoryFiller.IsFillNeeded; }
            set { _memoryFiller.IsFillNeeded = value; }
        }

        public ICommand IncreaseOnOneGbCommand => new RelayCommand(IncreaseOnOneGbExecute, IncreaseOnOneGbCanExecute);

        private bool IncreaseOnOneGbCanExecute()
        {
            return _memoryFiller.CanFill && _availableRam > 1024;
        }

        private void IncreaseOnOneGbExecute()
        {
            _memoryFiller.FillAsync(gbSize);
        }

        public ICommand IncreaseOn128MbCommand => new RelayCommand(IncreaseOn128MbExecute, IncreaseOn128MbCanExecute);

        private bool IncreaseOn128MbCanExecute()
        {
            return _memoryFiller.CanFill && _availableRam > 128;
        }

        private void IncreaseOn128MbExecute()
        {
            _memoryFiller.FillAsync(oteSize);
        }

        public ICommand DecreaseOnOneGbCommand => new RelayCommand(DecreaseOnOneGbExecute, DecreaseCanExecute);

        private void DecreaseOnOneGbExecute()
        {
            _memoryFiller.Empty(gbSize);
        }

        public ICommand DecreaseOn128MbCommand => new RelayCommand(DecreaseOn128MbExecute, DecreaseCanExecute);

        private void DecreaseOn128MbExecute()
        {
            _memoryFiller.Empty(oteSize);
        }

        public ICommand FreeAllCommand => new RelayCommand(FreeAllExecute, DecreaseCanExecute);

        private void FreeAllExecute()
        {
            _memoryFiller.Empty();
        }

        private bool DecreaseCanExecute()
        {
            return !_memoryFiller.IsEmpty;
        }
    }
}