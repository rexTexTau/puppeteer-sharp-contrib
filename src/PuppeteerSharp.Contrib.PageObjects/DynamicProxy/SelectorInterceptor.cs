using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace PuppeteerSharp.Contrib.PageObjects.DynamicProxy
{
    [Serializable]
    internal class SelectorInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (!invocation.HasValidReturnType())
            {
                invocation.ReturnValue = null;
                return;
            }

            var tcsType = typeof(TaskCompletionSource<>).MakeGenericType(invocation.Method.ReturnType.GetGenericArguments()[0]);
            var tcs = Activator.CreateInstance(tcsType);
            invocation.ReturnValue = tcsType.GetProperty("Task").GetValue(tcs, null);

#pragma warning disable CA2008 // Do not create tasks without passing a TaskScheduler
            InterceptAsync(invocation).ContinueWith(_ =>
            {
                tcsType.GetMethod("SetResult").Invoke(tcs, new[] { invocation.ReturnValue });
            });
#pragma warning restore CA2008 // Do not create tasks without passing a TaskScheduler
        }

        private async Task InterceptAsync(IInvocation invocation)
        {
            if (invocation.IsGetterPropertyWithAttribute<SelectorAttribute>())
            {
                var attribute = invocation.GetAttribute<SelectorAttribute>();

                if (invocation.InvocationTarget is PageObject pageObject)
                {
                    var result = await invocation.GetReturnValue(pageObject, attribute).ConfigureAwait(false);

                    invocation.ReturnValue = result;
                    return;
                }
                if (invocation.InvocationTarget is ElementObject elementObject)
                {
                    var result = await invocation.GetReturnValue(elementObject, attribute).ConfigureAwait(false);

                    invocation.ReturnValue = result;
                    return;
                }
            }

            invocation.ReturnValue = null;
        }
    }
}