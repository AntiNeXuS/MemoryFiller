using CpuMemStresser.Core.Memory;
using NUnit.Framework;

namespace CpuMemStresser.Tests
{
    [TestFixture]
    public class MemoryFillerTests
    {
        private MemoryFiller _filler;

        [SetUp]
        public void SetUp()
        {
            _filler = new MemoryFiller(1024);
        }

        [TestCase(10, 1, 10)]
        [TestCase(1024, 10, 102)]
        public void SizeToPieceCountTest(long size, int pieceSize, int expectedResult)
        {
            Assert.AreEqual(expectedResult, MemoryFiller.SizeToPieceCount(size, pieceSize));
        }

        [Test]
        public void FillAsyncTest()
        {
            _filler.FillAsync(2048);
        }
    }
}