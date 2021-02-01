using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LightningAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            var assetsFile = "assets.json";
            var lightningFile = "lightning.json";

            IDictionary<string, List<Asset>> assets = LightningAlert.GenerateAssetsLookup(assetsFile);
            LightningAlert.Start(lightningFile, assets);
        }
    }

    public static class LightningAlert
    {
        public static IDictionary<string, List<Asset>> GenerateAssetsLookup(string assetsFile)
        {
            using (StreamReader sr = File.OpenText(assetsFile))
            {
                var serializer = new JsonSerializer();
                List<Asset> assets = (List<Asset>)serializer.Deserialize(sr, typeof(List<Asset>));

                foreach (var asset in assets)
                {
                    TileSystem.QuadKeyToTileXY(asset.QuadKey); // verify no malform quadkey
                }

                Dictionary<string, List<Asset>> assetsLookup =
                    assets.GroupBy(x => x.QuadKey).ToDictionary(y => y.Key, y => y.ToList());

                return assetsLookup;
            }
        }

        public static void Start(string lightningFile, IDictionary<string, List<Asset>> assets)
        {
            using (StreamReader sr = File.OpenText(lightningFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Lightning lightning;
                    try
                    {
                        lightning = JsonConvert.DeserializeObject<Lightning>(line);
                    }
                    catch (JsonReaderException) // malform json
                    {
                        continue; // streaming
                    }
                    catch (JsonSerializationException) // missing property with required always
                    {
                        continue; // streaming
                    }

                    if (!lightning.IsLightningStrike())
                    {
                        continue; // streaming
                    }

                    string quadKey = lightning.ToQuadKey();

                    try
                    {
                        List<Asset> assetsInQuadKey = assets[quadKey];
                        foreach(var asset in assetsInQuadKey)
                        {
                            asset.RaiseLightningAlert();
                        }
                    }
                    catch (KeyNotFoundException) // lightning strike without affected assets
                    {
                        continue; // streaming
                    }
                }
            }
        }
    }
}
