using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PuppeteerSharp.Contrib.Extensions;

namespace PuppeteerSharp.Contrib.Tests.Extensions
{
    public class PageExtensionsTests : PuppeteerPageBaseTest
    {
        protected override async Task SetUp() => await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

        private async Task RemoveFoo() => await Page.SetContentAsync(@"
<html>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

        [Test]
        public async Task QuerySelectorWithContentAsync_should_return_the_first_element_that_match_the_selector_and_has_the_content()
        {
            var foo = await Page.QuerySelectorWithContentAsync("div", "Foo");
            Assert.AreEqual("foo", await foo.IdAsync());

            var bar = await Page.QuerySelectorWithContentAsync("div", "Ba.");
            Assert.AreEqual("bar", await bar.IdAsync());

            var flags = await Page.QuerySelectorWithContentAsync("div", "foo", "i");
            Assert.AreEqual("foo", await flags.IdAsync());

            var missing = await Page.QuerySelectorWithContentAsync("div", "Missing");
            Assert.Null(missing);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).QuerySelectorWithContentAsync("", ""));
        }

        [Test]
        public async Task QuerySelectorAllWithContentAsync_should_return_all_elements_that_match_the_selector_and_has_the_content()
        {
            var divs = await Page.QuerySelectorAllWithContentAsync("div", "Foo");
            Assert.AreEqual(new[] { "foo" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));

            divs = await Page.QuerySelectorAllWithContentAsync("div", "Ba.");
            Assert.AreEqual(new[] { "bar", "baz" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));

            var flags = await Page.QuerySelectorAllWithContentAsync("div", "foo", "i");
            Assert.AreEqual(new[] { "foo" }, await Task.WhenAll(flags.Select(x => x.IdAsync())));

            var missing = await Page.QuerySelectorAllWithContentAsync("div", "Missing");
            Assert.IsEmpty(missing);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).QuerySelectorAllWithContentAsync("", ""));
        }

        [Test]
        public async Task HasContentAsync_should_return_true_if_page_has_the_content()
        {
            Assert.True(await Page.HasContentAsync("Ba."));
            Assert.True(await Page.HasContentAsync("ba.", "i"));
            Assert.False(await Page.HasContentAsync("Missing"));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).HasContentAsync(""));
        }

        [Test]
        public async Task HasTitleAsync_should_return_true_if_page_has_the_title()
        {
            await Page.SetContentAsync(@"
<html>
 <head>
  <title>Foo Bar Baz</title>
 </head>
</html>");

            Assert.True(await Page.HasTitleAsync("Ba."));
            Assert.True(await Page.HasTitleAsync("ba.", "i"));
            Assert.False(await Page.HasTitleAsync("Missing"));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).HasTitleAsync(""));
        }

        [Test]
        public async Task HasUrlAsync_should_return_true_if_page_has_the_url()
        {
            Assert.True(await Page.HasUrlAsync("bla.")); // about:blank
            Assert.True(await Page.HasUrlAsync("Bla.", "i"));
            Assert.False(await Page.HasUrlAsync("Missing"));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).HasUrlAsync(""));
        }

        [Test]
        public async Task WaitForElementsRemovedFromDOMAsync_should_throw_WaitTaskTimeoutException_if_element_is_present_in_DOM()
        {
            Assert.ThrowsAsync<WaitTaskTimeoutException>(async () => await Page.WaitForElementsRemovedFromDOMAsync("#foo", 1));
        }

        [Test]
        public async Task WaitForElementsRemovedFromDOMAsync_should_return_if_page_has_no_elements_matching_the_selector()
        {
            await RemoveFoo();
            await Page.WaitForElementsRemovedFromDOMAsync("#foo");
            Assert.Null(await Page.QuerySelectorWithContentAsync("div", "Foo"));
        }

        [Test]
        public async Task WaitForElementsRemovedFromDOMAsync_should_return_if_elements_matching_the_selector_are_removed_from_page()
        {
            Task.WaitAll(Page.WaitForElementsRemovedFromDOMAsync("#foo"), RemoveFoo());
            Assert.Null(await Page.QuerySelectorWithContentAsync("div", "Foo"));
        }
    }
}
