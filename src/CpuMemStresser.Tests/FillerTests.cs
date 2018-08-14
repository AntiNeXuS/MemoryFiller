using NUnit.Framework;

namespace CpuMemStresser.Tests
{
    [TestFixture]
    public class FillerTests
    {
        [TestCase(10, 1, 10)]
        [TestCase(1024, 10, 102)]
        public void SizeToPieceCountTest(long size, int pieceSize, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Filler.SizeToPieceCount(size, pieceSize));
        }

        [Test]
        public void FillAsyncTest()
        {
            var filler = new Filler(1024);

            filler.FillAsync(2048);

        }
    }
}