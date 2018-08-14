// \***************************************************************************/
// Solution:           MemoryFiller
// Project:            MemoryFiller
// Filename:           Filler.cs
// Created:            09.09.2017
// \***************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Linq;
using CpuMemStresser.Models;

namespace CpuMemStresser
{
    public class Filler
    {
        public int PieceSize { get; }

        public bool Fill
        {
            get { return _fill; }
            set
            {
                if (_fill == value) return;

                _fill = value;

                if (_fill == true)
                {
                    var usedMemory = UsedMemory;
                    Empty();
                    FillAsync(usedMemory);
                }
            }
        }

        public bool CanFill => _outOfMemory == false;
        public bool IsEmpty => _memory.IsEmpty;

        public long UsedMemory => _memory.Count * (long) PieceSize;

        private readonly ConcurrentStack<MemoryPiece> _memory = new ConcurrentStack<MemoryPiece>();
        private bool _outOfMemory;
        private bool _fill;

        public Filler(int pieceSize)
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
                    _memory.Push(new MemoryPiece(PieceSize, Fill));
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
                MemoryPiece piece;
                if (_memory.TryPop(out piece))
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
                MemoryPiece piece;
                if (_memory.TryPop(out piece))
                {
                    piece.Dispose();
                    _outOfMemory = false;
                }
            }
        }

        public static int SizeToPieceCount(long size, int pieceSize)
        {
            var count = Convert.ToInt32(size / pieceSize);

            return count;
        }
    }
}