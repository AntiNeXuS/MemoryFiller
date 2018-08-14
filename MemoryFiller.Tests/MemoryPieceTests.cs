using CpuMemStresser.Models;
using NUnit.Framework;

namespace CpuMemStresser.Tests
{
    [TestFixture]
    public class MemoryPieceTests
    {
        [Test]
        public void CreateTest()
        {
            var piece = new MemoryPiece(size: 1024, fill: false);
            Assert.NotNull(piece);
            piece.Dispose();
        }
    }
}
