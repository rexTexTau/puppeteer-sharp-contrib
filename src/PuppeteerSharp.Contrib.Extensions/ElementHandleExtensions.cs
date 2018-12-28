﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PuppeteerSharp.Contrib.Extensions
{
    public static class ElementHandleExtensions
    {
        // Exists

        public static bool Exists(this ElementHandle handle)
        {
            return handle != null;
        }

        public static bool Exists(this Task<ElementHandle> task)
        {
            return task.Result().Exists();
        }

        // InnerHtml

        public static async Task<string> InnerHtmlAsync(this ElementHandle handle)
        {
            return await handle.GetPropertyValueAsync("innerHTML").ConfigureAwait(false);
        }

        public static string InnerHtml(this ElementHandle handle)
        {
            return handle.InnerHtmlAsync().Result();
        }

        public static string InnerHtml(this Task<ElementHandle> task)
        {
            return task.Result().InnerHtml();
        }

        // OuterHtml

        public static async Task<string> OuterHtmlAsync(this ElementHandle handle)
        {
            return await handle.GetPropertyValueAsync("outerHTML").ConfigureAwait(false);
        }

        public static string OuterHtml(this ElementHandle handle)
        {
            return handle.OuterHtmlAsync().Result();
        }

        public static string OuterHtml(this Task<ElementHandle> task)
        {
            return task.Result().OuterHtml();
        }

        // TextContent

        public static async Task<string> TextContentAsync(this ElementHandle handle)
        {
            return await handle.GetPropertyValueAsync("textContent").ConfigureAwait(false);
        }

        public static string TextContent(this ElementHandle handle)
        {
            return handle.TextContentAsync().Result();
        }

        public static string TextContent(this Task<ElementHandle> task)
        {
            return task.Result().TextContent();
        }

        // InnerText

        public static async Task<string> InnerTextAsync(this ElementHandle handle)
        {
            return await handle.GetPropertyValueAsync("innerText").ConfigureAwait(false);
        }

        public static string InnerText(this ElementHandle handle)
        {
            return handle.InnerTextAsync().Result();
        }

        public static string InnerText(this Task<ElementHandle> task)
        {
            return task.Result().InnerText();
        }

        // HasContent

        public static async Task<bool> HasContentAsync(this ElementHandle handle, string content)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("(node, content) => node.textContent.includes(content)", content).ConfigureAwait(false);
        }

        public static bool HasContent(this ElementHandle handle, string content)
        {
            return handle.HasContentAsync(content).Result();
        }

        public static bool HasContent(this Task<ElementHandle> task, string content)
        {
            return task.Result().HasContent(content);
        }

        // ClassName

        public static async Task<string> ClassNameAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<string>("element => element.className").ConfigureAwait(false);
        }

        public static string ClassName(this ElementHandle handle)
        {
            return handle.ClassNameAsync().Result();
        }

        public static string ClassName(this Task<ElementHandle> task)
        {
            return task.Result().ClassName();
        }

        // ClassList

        public static async Task<string[]> ClassListAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            var json = await handle.EvaluateFunctionAsync<JObject>("element => element.classList").ConfigureAwait(false);
            var dictionary = json.ToObject<Dictionary<string, string>>();
            return dictionary.Values.ToArray();
        }

        public static string[] ClassList(this ElementHandle handle)
        {
            return handle.ClassListAsync().Result();
        }

        public static string[] ClassList(this Task<ElementHandle> task)
        {
            return task.Result().ClassList();
        }

        // HasClass

        public static async Task<bool> HasClassAsync(this ElementHandle handle, string className)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("(element, className) => element.classList.contains(className)", className).ConfigureAwait(false);
        }

        public static bool HasClass(this ElementHandle handle, string className)
        {
            return handle.HasClassAsync(className).Result();
        }

        public static bool HasClass(this Task<ElementHandle> task, string className)
        {
            return task.Result().HasClass(className);
        }

        // IsVisible

        public static async Task<bool> IsVisibleAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            // https://blog.jquery.com/2009/02/20/jquery-1-3-2-released/#visible-hidden-overhauled
            return await handle.EvaluateFunctionAsync<bool>("element => element.offsetWidth > 0 && element.offsetHeight > 0").ConfigureAwait(false);
        }

        public static bool IsVisible(this ElementHandle handle)
        {
            return handle.IsVisibleAsync().Result();
        }

        public static bool IsVisible(this Task<ElementHandle> task)
        {
            return task.Result().IsVisible();
        }

        // IsHidden

        public static async Task<bool> IsHiddenAsync(this ElementHandle handle)
        {
            return !await handle.IsVisibleAsync().ConfigureAwait(false);
        }

        public static bool IsHidden(this ElementHandle handle)
        {
            return handle.IsHiddenAsync().Result();
        }

        public static bool IsHidden(this Task<ElementHandle> task)
        {
            return task.Result().IsHidden();
        }

        // IsSelected

        public static async Task<bool> IsSelectedAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("element => element.selected").ConfigureAwait(false);
        }

        public static bool IsSelected(this ElementHandle handle)
        {
            return handle.IsSelectedAsync().Result();
        }

        public static bool IsSelected(this Task<ElementHandle> task)
        {
            return task.Result().IsSelected();
        }

        // IsChecked

        public static async Task<bool> IsCheckedAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("element => element.checked").ConfigureAwait(false);
        }

        public static bool IsChecked(this ElementHandle handle)
        {
            return handle.IsCheckedAsync().Result();
        }

        public static bool IsChecked(this Task<ElementHandle> task)
        {
            return task.Result().IsChecked();
        }

        // IsDisabled

        public static async Task<bool> IsDisabledAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("element => element.disabled").ConfigureAwait(false);
        }

        public static bool IsDisabled(this ElementHandle handle)
        {
            return handle.IsDisabledAsync().Result();
        }

        public static bool IsDisabled(this Task<ElementHandle> task)
        {
            return task.Result().IsDisabled();
        }

        // IsEnabled

        public static async Task<bool> IsEnabledAsync(this ElementHandle handle)
        {
            return !await handle.IsDisabledAsync().ConfigureAwait(false);
        }

        public static bool IsEnabled(this ElementHandle handle)
        {
            return handle.IsEnabledAsync().Result();
        }

        public static bool IsEnabled(this Task<ElementHandle> task)
        {
            return task.Result().IsEnabled();
        }

        // IsReadOnly

        public static async Task<bool> IsReadOnlyAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("element => element.readOnly").ConfigureAwait(false);
        }

        public static bool IsReadOnly(this ElementHandle handle)
        {
            return handle.IsReadOnlyAsync().Result();
        }

        public static bool IsReadOnly(this Task<ElementHandle> task)
        {
            return task.Result().IsReadOnly();
        }

        // HasFocus

        public static async Task<bool> HasFocusAsync(this ElementHandle handle)
        {
            handle.GuardFromNull();
            return await handle.EvaluateFunctionAsync<bool>("element => element === document.activeElement").ConfigureAwait(false);
        }

        public static bool HasFocus(this ElementHandle handle)
        {
            return handle.HasFocusAsync().Result();
        }

        public static bool HasFocus(this Task<ElementHandle> task)
        {
            return task.Result().HasFocus();
        }

        // GetPropertyValue

        private static async Task<string> GetPropertyValueAsync(this ElementHandle handle, string propertyName)
        {
            handle.GuardFromNull();
            var property = await handle.GetPropertyAsync(propertyName).ConfigureAwait(false);
            return await property.JsonValueAsync<string>().ConfigureAwait(false);
        }

        private static string GetPropertyValue(this ElementHandle handle, string propertyName)
        {
            return handle.GetPropertyValueAsync(propertyName).Result();
        }

        private static string GetPropertyValue(this Task<ElementHandle> task, string propertyName)
        {
            return task.Result().GetPropertyValue(propertyName);
        }
    }
}