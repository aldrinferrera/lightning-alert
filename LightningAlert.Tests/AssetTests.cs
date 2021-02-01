using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace LightningAlert.Tests
{
    public class AssetTests
    {
        [Fact]
        public void RaiseLightningAlert_CalledTwice_ShouldWriteOneAlertOnly()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var asset = new Asset()
            {
                AssetName = "testing",
                AssetOwner = "1234"
            };

            string expected = $"lightning alert for {asset.AssetOwner}:{asset.AssetName}{Environment.NewLine}";

            asset.RaiseLightningAlert();
            asset.RaiseLightningAlert();

            string actual = output.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RaiseLightningAlert_SecondCallWithForceArgument_ShouldWriteDuplicateAlert()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var asset = new Asset()
            {
                AssetName = "testing",
                AssetOwner = "1234"
            };
            
            string expected = $"lightning alert for {asset.AssetOwner}:{asset.AssetName}{Environment.NewLine}" +
                $"lightning alert for {asset.AssetOwner}:{asset.AssetName}{Environment.NewLine}";

            asset.RaiseLightningAlert();
            asset.RaiseLightningAlert(force: true);

            string actual = output.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RaiseLightningAlert_FirstCallWithForceArgument_ShouldWriteOneAlertOnly()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var asset = new Asset()
            {
                AssetName = "testing",
                AssetOwner = "1234"
            };

            string expected = $"lightning alert for {asset.AssetOwner}:{asset.AssetName}{Environment.NewLine}";

            asset.RaiseLightningAlert(force: true);
            asset.RaiseLightningAlert();

            string actual = output.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssetName_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var asset = "{\"quadKey\":\"023112133002\",\"assetOwner\":\"02115\"}";
            var expectedProperty = "AssetName";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Asset>(asset));

            Assert.Contains(expectedProperty, ex.Message);
        }

        [Fact]
        public void QuadKey_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var asset = "{\"assetName\":\"Mayer Park\",\"assetOwner\":\"02115\"}";
            var expectedProperty = "QuadKey";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Asset>(asset));

            Assert.Contains(expectedProperty, ex.Message);
        }

        [Fact]
        public void AssetOwner_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var asset = "{\"assetName\":\"Mayer Park\",\"quadKey\":\"023112133002\"}";
            var expectedProperty = "AssetOwner";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Asset>(asset));

            Assert.Contains(expectedProperty, ex.Message);
        }
    }
}
