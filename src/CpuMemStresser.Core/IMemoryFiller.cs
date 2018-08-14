namespace CpuMemStresser.Core
{
    public interface IMemoryFiller
    {
        long UsedMemory { get; }

        bool IsFillNeeded { get; set; }

        bool CanFill { get; }

        bool IsEmpty { get; }

        void FillAsync(long size);

        void Empty(long size);

        void Empty();
    }
}