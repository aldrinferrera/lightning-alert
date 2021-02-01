using Newtonsoft.Json;
using System;

namespace LightningAlert
{
    public class Asset
    {
        [JsonProperty(Required = Required.Always)]
        public string AssetName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string QuadKey { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string AssetOwner { get; set; }

        private bool _assetWithAlert = false;

        public void RaiseLightningAlert(bool force = false)
        {
            if (force || !_assetWithAlert)
            {
                _assetWithAlert = true;
                Console.WriteLine($"lightning alert for {AssetOwner}:{AssetName}");
            }
        }
    }
}
