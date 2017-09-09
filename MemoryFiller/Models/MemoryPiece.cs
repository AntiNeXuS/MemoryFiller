// \***************************************************************************/
// Solution:           MemoryFiller
// Project:            MemoryFiller
// Filename:           MemoryPiece.cs
// Created:            09.09.2017
// \***************************************************************************/

using System;
using System.IO;
using System.Threading.Tasks;

namespace MemoryFiller.Models
{
    public class MemoryPiece : IDisposable
    {
        public int Size { get; }
        public bool Fill { get; }

        private readonly MemoryStream _memory;
        private readonly Task _fillTask;

        public MemoryPiece(int size, bool fill)
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