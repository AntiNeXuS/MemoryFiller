// \***************************************************************************/
// Solution:           MemoryFiller
// Project:            MemoryFiller.Tests
// Filename:           FillerTests.cs
// Created:            09.09.2017
// \***************************************************************************/

using NUnit.Framework;

namespace MemoryFiller.Tests
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