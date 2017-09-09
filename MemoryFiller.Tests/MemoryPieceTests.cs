using MemoryFiller.Models;
using NUnit.Framework;

namespace MemoryFiller.Tests
{
    [TestFixture]
    public class MemoryPieceTests
    {
        [Test]
        public void CreateTest()
        {
            var piece = new MemoryPiece(size: 1024, fill: false);
            Assert.NotNull(piece);
            if (piece != null)
                piece.Dispose();
        }
    }
}
