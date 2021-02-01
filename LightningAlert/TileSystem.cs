using System;
using System.Text;

namespace LightningAlert
{
    public static class TileSystem
    {
        private const double MIN_LATITUDE = -85.05112878;
        private const double MAX_LATITUDE = 85.05112878;
        private const double MIN_LONGITUDE = -180;
        private const double MAX_LONGITUDE = 180;

        public static (int tileX, int tileY, int levelOfDetail) QuadKeyToTileXY(string quadKey)
        {
            var tileX = 0;
            var tileY = 0;
            int levelOfDetail = quadKey.Length;

            for (int i = levelOfDetail; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (quadKey[levelOfDetail - i])
                {
                    case '0':
                        break;

                    case '1':
                        tileX |= mask;
                        break;

                    case '2':
                        tileY |= mask;
                        break;

                    case '3':
                        tileX |= mask;
                        tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }

            return (tileX, tileY, levelOfDetail);
        }

        public static uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        public static (int pixelX, int pixelY) LatLongToPixelXY(double latitude, double longitude, int levelOfDetail)
        {
            latitude = Math.Clamp(latitude, MIN_LATITUDE, MAX_LATITUDE);
            longitude = Math.Clamp(longitude, MIN_LONGITUDE, MAX_LONGITUDE);

            double x = (longitude + 180) / 360;
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            uint mapSize = MapSize(levelOfDetail);
            int pixelX = (int)Math.Clamp(x * mapSize + 0.5, 0, mapSize - 1);
            int pixelY = (int)Math.Clamp(y * mapSize + 0.5, 0, mapSize - 1);

            return (pixelX, pixelY);
        }

        public static (int tileX, int tileY) PixelXYToTileXY(int pixelX, int pixelY)
        {
            int tileX = pixelX / 256;
            int tileY = pixelY / 256;

            return (tileX, tileY);
        }

        public static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            var quadKey = new StringBuilder();
            for (int i = levelOfDetail; i > 0; i--)
            {
                var digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }
    }
}
