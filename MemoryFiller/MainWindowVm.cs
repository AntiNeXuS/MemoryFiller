// /****************************** MemoryFiller ******************************\
// Project:            MemoryFiller
// Filename:           MainWindowVm.cs
// Created:            14.02.2017
// 
// <summary>
// 
// </summary>
// \***************************************************************************/

using System;
using System.Diagnostics;
using MemoryFiller.ViewModel;

namespace MemoryFiller
{
    public class MainWindowVm : MainViewModel
    {
        private int _availableRam;
        private int _totalRam;
        private int _useRamPercentage;
        private PerformanceCounter _ramCounter;

        public MainWindowVm()
        {
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

            UseRamPercentage = 35;
            _availableRam = Convert.ToInt32(_ramCounter.NextValue());
            _totalRam = (int) new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024;
            
            UseRamPercentage = _availableRam / _totalRam * 100;
        }

        public string AvailableRam
        {
            get { return $"{_availableRam} Mb"; }
            set {  }
        }

        public string TotalRam
        {
            get { return $"{_totalRam} Mb"; }
            set {  }
        }

        public int UseRamPercentage
        {
            get { return _useRamPercentage; }
            set { _useRamPercentage = value; }
        }
    }
}