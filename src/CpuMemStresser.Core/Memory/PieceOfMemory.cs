using System;
using System.IO;
using System.Threading.Tasks;

namespace CpuMemStresser.Core.Memory
{
    public class PieceOfMemory : IDisposable
    {
        private readonly MemoryStream _memory;
        private readonly Task _fillTask;

        public int Size { get; }

        public bool Fill { get; }

        public PieceOfMemory(int size, bool fill)
        {
            Size = size;
            Fill = fill;

            _memory = new MemoryStream(size);
            if (fill)
            {
                _fillTask = new Task(() =>
                {
                    while (_memory.Length < size)
                    {
                        _memory.WriteByte(0xFF);
                    }
                });

                _fillTask.Start();
            }
        }

        public void Dispose()
        {
            _fillTask?.Dispose();
            _memory.Close();
            _memory.Dispose();
            GC.Collect();
        }
    }
}