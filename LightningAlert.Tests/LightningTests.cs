using Newtonsoft.Json;
using System;
using Xunit;

namespace LightningAlert.Tests
{
    public class LightningTests
    {
        [Fact]
        public void FlashType_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var lightning = "{\"latitude\":33.7476109,\"longitude\":-96.7255643}";
            var expectedProperty = "FlashType";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Lightning>(lightning));

            Assert.Contains(expectedProperty, ex.Message);
        }

        [Fact]
        public void Latitude_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var lightning = "{\"flashType\":1,\"longitude\":-96.7255643}";
            var expectedProperty = "Latitude";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Lightning>(lightning));

            Assert.Contains(expectedProperty, ex.Message);
        }

        [Fact]
        public void Longitude_JsonPropertyRequiredAlways_ShouldThrowJsonSerializationException()
        {
            var lightning = "{\"flashType\":1,\"latitude\":33.7476109}";
            var expectedProperty = "Longitude";

            JsonSerializationException ex = Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Lightning>(lightning));

            Assert.Contains(expectedProperty, ex.Message);
        }

        [Fact]
        public void ToQuadKey_ShouldReturnCorrectQuadKey()
        {
            var lightning = "{\"flashType\":1,\"latitude\":14.5995,\"longitude\":120.9842}";
            var expected = "132303122000";

            string actual = JsonConvert.DeserializeObject<Lightning>(lightning).ToQuadKey();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsLightningStrike_HeartbeatFlashType_ShouldReturnFalse()
        {
            var lightning = "{\"flashType\":9,\"latitude\":14.5995,\"longitude\":120.9842}";
            var expected = false;

            bool actual = JsonConvert.DeserializeObject<Lightning>(lightning).IsLightningStrike();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsLightningStrike_AnyExceptHearbeatFlashType_ShouldReturnTrue()
        {
            var cloudToGround = "{\"flashType\":0,\"latitude\":14.5995,\"longitude\":120.9842}";
            var cloudToCloud = "{\"flashType\":1,\"latitude\":14.5995,\"longitude\":120.9842}";
            var unknownFlashType = "{\"flashType\":27,\"latitude\":14.5995,\"longitude\":120.9842}";
            var expected = true;

            bool actualCloudToGround = JsonConvert.DeserializeObject<Lightning>(cloudToGround).IsLightningStrike();
            bool actualCloudToCloud = JsonConvert.DeserializeObject<Lightning>(cloudToCloud).IsLightningStrike();
            bool actualUnknownFlashType = JsonConvert.DeserializeObject<Lightning>(unknownFlashType).IsLightningStrike();

            Assert.Equal(expected, actualCloudToGround);
            Assert.Equal(expected, actualCloudToCloud);
            Assert.Equal(expected, actualUnknownFlashType);
        }
    }
}
