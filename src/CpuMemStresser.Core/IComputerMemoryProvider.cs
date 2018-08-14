namespace CpuMemStresser.Core
{
    public interface IComputerMemoryProvider
    {
        /// <summary>
        /// Total RAM memory size in bytes.
        /// </summary>
        double TotalMemorySize { get; }

        /// <summary>
        /// Available RAM memory size in bytes.
        /// </summary>
        long AvailableMemorySize { get; }
    }
}