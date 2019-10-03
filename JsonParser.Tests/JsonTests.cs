using System.IO;
using NUnit.Framework;

namespace JsonParser.Tests
{
    [TestFixture]
    public class JsonTests
    {
        private static string GetTestData() =>
            File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData.json"));

        [Test]
        public void Json_Parse_Test()
        {
            // Arrange
            var str = GetTestData();

            // Act
            var json = JsonEntity.Parse(str);

            // Assert
            Assert.That(json, Is.Not.Null);

            // - order
            Assert.That(json["order"], Is.Not.Null);
            Assert.That(json["order"]["complete"].GetValue<bool>(), Is.True);

            // - order.items
            Assert.That(json["order"]["items"], Is.Not.Null);

            // - order.items[0]
            Assert.That(json["order"]["items"][0], Is.Not.Null);
            Assert.That(json["order"]["items"][0]["name"].GetValue<string>(), Is.EqualTo("Egg"));
            Assert.That(json["order"]["items"][0]["qty"].GetValue<int>(), Is.EqualTo(2));
            Assert.That(json["order"]["items"][0]["price"].GetValue<double>(), Is.EqualTo(3.14));

            // - order.items[1]
            Assert.That(json["order"]["items"][1], Is.Not.Null);
            Assert.That(json["order"]["items"][1]["name"].GetValue<string>(), Is.EqualTo("Bottle of water"));
            Assert.That(json["order"]["items"][1]["qty"].GetValue<int>(), Is.EqualTo(1));
            Assert.That(json["order"]["items"][1]["price"].GetValue<double>(), Is.EqualTo(2.5));
        }
    }
}