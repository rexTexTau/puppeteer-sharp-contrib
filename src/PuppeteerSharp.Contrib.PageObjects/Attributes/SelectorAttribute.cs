using System;

namespace PuppeteerSharp.Contrib.PageObjects
{
    /// <summary>
    /// Represents a selector for a property on a <see cref="PageObject" /> or <see cref="ElementObject" />.
    ///
    /// Properties decorated with a <see cref="SelectorAttribute" /> must be a:
    /// <list type="bullet">
    /// <item><description>public</description></item>
    /// <item><description>virtual</description></item>
    /// <item><description>getter</description></item>
    /// </list>
    /// that returns a <see cref="System.Threading.Tasks.Task{TResult}" /> of:
    /// <list type="bullet">
    /// <item><description><see cref="ElementHandle" />,</description></item>
    /// <item><description><see cref="ElementHandle" />[],</description></item>
    /// <item><description><see cref="ElementObject" /> or</description></item>
    /// <item><description><see cref="ElementObject" />[]</description></item>
    /// </list>
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// [Selector("#foo")]
    /// public virtual Task<ElementHandle> SelectorForElementHandle { get; }
    ///
    /// [Selector(".bar")]
    /// public virtual Task<ElementHandle[]> SelectorForElementHandleArray { get; }
    ///
    /// [Selector("#foo")]
    /// public virtual Task<FooElementObject> SelectorForElementObject { get; }
    ///
    /// [Selector(".bar")]
    /// public virtual Task<BarElementObject[]> SelectorForElementObjectArray { get; }
    /// ]]>
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectorAttribute : Attribute
    {
        /// <summary>
        /// A selector to query a <see cref="Page" /> or <see cref="ElementHandle" /> for.
        /// </summary>
        public string Selector { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorAttribute" /> class.
        /// </summary>
        /// <param name="selector">A selector to query a <see cref="Page" /> or <see cref="ElementHandle" /> for.</param>
        public SelectorAttribute(string selector)
        {
            Selector = selector;
        }
    }
}
