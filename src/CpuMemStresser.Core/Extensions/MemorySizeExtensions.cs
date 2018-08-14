namespace CpuMemStresser.Core.Extensions
{
    public static class MemorySizeExtensions
    {
        public static double ToKb(this double sizeInBytes)
        {
            return sizeInBytes / 1024;
        }

        public static long ToKb(this long sizeInBytes)
        {
            return sizeInBytes / 1024;
        }

        public static double ToMb(this double sizeInBytes)
        {
            return sizeInBytes / 1024 / 1024;
        }

        public static long ToMb(this long sizeInBytes)
        {
            return sizeInBytes / 1024 / 1024;
        }

        public static double ToGb(this double sizeInBytes)
        {
            return sizeInBytes / 1024 / 1024 / 1024;
        }

        public static long ToGb(this long sizeInBytes)
        {
            return sizeInBytes / 1024 / 1024 / 1024;
        }
    }
}