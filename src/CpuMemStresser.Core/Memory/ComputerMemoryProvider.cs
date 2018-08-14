using System;
using System.Diagnostics;

namespace CpuMemStresser.Core.Memory
{
    public class ComputerMemoryProvider : IComputerMemoryProvider
    {
        private readonly PerformanceCounter _ramCounter;

        public ComputerMemoryProvider()
        {
            _ramCounter = new PerformanceCounter("Memory", "Available Bytes", true);
            TotalMemorySize = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }

        public double TotalMemorySize { get; }

        public long AvailableMemorySize => Convert.ToInt64(_ramCounter.NextValue());
    }
}