using System.Collections.Generic;
using System.Linq;
using System.Net;
using HttpSpeak.IntegrationTests.Controllers;
using HttpSpeak.Serializers;
using NUnit.Framework;

namespace HttpSpeak.IntegrationTests
{
    [TestFixture]
    public class BasicHttpMethodsWithXmlSerialization : WithApi
    {
        protected override IHttpClient CreateClient()
        {
            var settings = HttpClientSettings.Default;
            settings.Serializers.Clear();
            settings.Serializers.Add(new DotNetXmlSerializer());

            return HttpClient.Create("http://localhost:1337/api", settings);
        }

        [Test]
        public void ShouldHaveCorrectDeserializerOnResponse()
        {
            var response = client.Get("products/1");

            Assert.That(response.Deserializer, Is.TypeOf<DotNetXmlSerializer>());
        }

        [Test]
        public void ShouldCreateNewProduct()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var isok = client.Post(product, "products").Is(HttpStatusCode.Created);

            Assert.That(isok);
        }

        [Test]
        public void ShouldGetCollection()
        {
            var products = client.Get("products").On(HttpStatusCode.OK).Unwrap<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }
    }
}