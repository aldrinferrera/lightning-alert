using Newtonsoft.Json;

namespace LightningAlert
{
    public class Lightning
    {
        private static readonly int Heartbeat = 9;

        [JsonProperty(Required = Required.Always)]
        public int FlashType { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double Latitude { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double Longitude { get; set; }

        public double StrikeTime { get; set; }

        public int PeakAmps { get; set; }

        public string Reserved { get; set; }

        public int IcHeight { get; set; }

        public double ReceivedTime { get; set; }

        public int NumberOfSensors { get; set; }

        public int Multiplicity { get; set; }

        public bool IsLightningStrike()
        {
            return Heartbeat != FlashType;
        }

        public string ToQuadKey(int zoomLevel = 12)
        {
            (int pixelX, int pixelY) = TileSystem.LatLongToPixelXY(Latitude, Longitude, zoomLevel);

            (int tileX, int tileY) = TileSystem.PixelXYToTileXY(pixelX, pixelY);

            string quadKey = TileSystem.TileXYToQuadKey(tileX, tileY, zoomLevel);

            return quadKey;
        }
    }
}
