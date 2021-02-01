using System;
using Xunit;

namespace LightningAlert.Tests
{
    public class TileSystemTests
    {
        [Fact]
        public void QuadKeyToTileXY_WithInvalidQuadKey_ShouldThrowArgumentException()
        {
            var expected = "Invalid QuadKey digit sequence.";

            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => TileSystem.QuadKeyToTileXY("ABC112303121"));

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public void QuadKeyToTileXY_ShouldReturnCorrectTileXYCoordinates()
        {
            var quadKey = "132303122000";
            (int tileX, int tileY, int levelOfDetail) expected = (3424, 1880, 12);

            (int tileX, int tileY, int levelOfDetail) actual = TileSystem.QuadKeyToTileXY(quadKey);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MapSize_ShouldReturnCorrectMapWidthAndHeightInPixels()
        {
            var levelOfDetail = 12;
            uint expected = 1048576;

            uint actual = TileSystem.MapSize(levelOfDetail);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TileXYToQuadKey_ShouldReturnCorrectQuadKey()
        {
            int tileX = 3424;
            int tileY = 1880;
            int levelOfDetail = 12;
            var expected = "132303122000";

            string actual = TileSystem.TileXYToQuadKey(tileX, tileY, levelOfDetail);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PixelXYToTileXY_ShouldReturnCorrectTileXYCoordinates()
        {
            int pileX = 876680;
            int pileY = 481296;
            (int tileX, int tileY) expected = (3424, 1880);

            (int tileX, int tileY) actual = TileSystem.PixelXYToTileXY(pileX, pileY);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LatLongToPixelXY_ShouldReturnCorrectPixelXYCoordinates()
        {
            double latitude = 14.5995;
            double longitude = 120.9842;
            int levelOfDetail = 12;
            (int pileX, int pileY) expected = (876680, 481296);

            (int pileX, int pileY) actual = TileSystem.LatLongToPixelXY(latitude, longitude, levelOfDetail);

            Assert.Equal(expected, actual);
        }
    }
}
