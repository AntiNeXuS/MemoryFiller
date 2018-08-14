using CpuMemStresser.Core.Memory;
using NUnit.Framework;

namespace CpuMemStresser.Tests
{
    [TestFixture]
    public class PieceOfMemoryTests
    {
        [Test]
        public void CreateTest()
        {
            var piece = new PieceOfMemory(size: 1024, fill: false);
            Assert.NotNull(piece);
            piece.Dispose();
        }
    }
}
