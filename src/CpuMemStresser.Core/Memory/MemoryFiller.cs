using System;
using System.Collections.Concurrent;
using System.Linq;

namespace CpuMemStresser.Core.Memory
{
    public class MemoryFiller : IMemoryFiller
    {
        private readonly ConcurrentStack<PieceOfMemory> _memory = new ConcurrentStack<PieceOfMemory>();
        private bool _outOfMemory;
        private bool _isFillNeeded;

        public int PieceSize { get; }

        public bool CanFill => _outOfMemory == false;
        public bool IsEmpty => _memory.IsEmpty;

        public long UsedMemory => _memory.Count * (long)PieceSize;

        public bool IsFillNeeded
        {
            get { return _isFillNeeded; }
            set
            {
                if (_isFillNeeded == value) return;

                _isFillNeeded = value;

                if (_isFillNeeded)
                {
                    var usedMemory = UsedMemory;
                    Empty();
                    FillAsync(usedMemory);
                }
            }
        }

        public MemoryFiller(int pieceSize)
        {
            PieceSize = pieceSize;
        }

        public void FillAsync(long size)
        {
            var pieceCount = SizeToPieceCount(size, PieceSize);
            for (int i = 0; i < pieceCount; i++)
            {
                try
                {
                    _memory.Push(new PieceOfMemory(PieceSize, IsFillNeeded));
                }
                catch (OutOfMemoryException e)
                {
                    _outOfMemory = true;
                    break;
                }
            }
        }

        public void Empty(long size)
        {
            var pieceCount = SizeToPieceCount(size, PieceSize);
            while (_memory.Any() && pieceCount > 0)
            {
                if (_memory.TryPop(out var piece))
                {
                    piece.Dispose();
                    pieceCount--;
                    _outOfMemory = false;
                }
            }
        }

        public void Empty()
        {
            while (_memory.Any())
            {
                if (_memory.TryPop(out var piece))
                {
                    piece.Dispose();
                    _outOfMemory = false;
                }
            }
        }

        public static int SizeToPieceCount(long size, int pieceSize)
        {
            return Convert.ToInt32(size / pieceSize);
        }
    }
}