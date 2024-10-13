namespace FCArsenalFanPage.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    public class RedirectIfAuthenticatedAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("/");
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
    }
}
