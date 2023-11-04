using System.Threading.Tasks;
using PuppeteerSharp.Contrib.PageObjects;
using NUnit.Framework;
using System.Linq;

namespace PuppeteerSharp.Contrib.Tests.PageObjects
{
    
    public class ElementHandleExtensionsTests : PuppeteerPageBaseTest
    {
        private ElementHandle _elementHandle;

        protected override async Task SetUp()
        {
            await Page.SetContentAsync(Fake.Html);
            _elementHandle = await Page.QuerySelectorAsync("html");
        }

        [Test]
        public async Task QuerySelectorAllAsync_returns_proxies_of_type()
        {
            var result = await _elementHandle.QuerySelectorAllAsync<FakeElementObject>("div");
            Assert.IsNotEmpty(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(FakeElementObject));
            CollectionAssert.AllItemsAreNotNull(result.Select(x => x.Page));

            result = await _elementHandle.QuerySelectorAllAsync<FakeElementObject>(".missing");
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task QuerySelectorAsync_returns_proxy_of_type()
        {
            var result = await _elementHandle.QuerySelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
            Assert.NotNull(result.Page);

            result = await _elementHandle.QuerySelectorAsync<FakeElementObject>(".missing");
            Assert.Null(result);
        }

        [Test]
        public async Task XPathAsync_returns_proxies_of_type()
        {
            var result = await _elementHandle.XPathAsync<FakeElementObject>("//div");
            Assert.IsNotEmpty(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(FakeElementObject));
            CollectionAssert.AllItemsAreNotNull(result.Select(x => x.Page));

            result = await _elementHandle.XPathAsync<FakeElementObject>("//missing");
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task WaitForSelectorAsync_returns_proxy_of_type()
        {
            var result = await _elementHandle.WaitForSelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
            Assert.NotNull(result.Page);

            Assert.ThrowsAsync<WaitTaskTimeoutException>(async () => await _elementHandle.WaitForSelectorAsync<FakeElementObject>(".missing", new() { Timeout = 1 }));
        }
    }
}
