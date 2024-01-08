using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using webUserLoginTest.Data;
using webUserLoginTest.Models.ViewModel;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace webUserLoginTest.CustomFilters;

public class UserAuthorizationFilterAttribute: ActionFilterAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.SingleOrDefault(x => x is AllowAnonymousAttribute) !=
            null) return;
        //foreach (var o in context.ActionDescriptor.EndpointMetadata)
        //{
        //    if (o is AllowAnonymousAttribute)
        //    {
        //        return;
        //    }
        //}

        string sessionid = context.HttpContext.Request.Cookies["sessionid"];
        int id = Sessions.GetUserId(Sessions.ConvertSessionIdToByte(sessionid));
        
        if (id == -1) context.Result = new RedirectToActionResult(actionName: "NotFound", controllerName: "Home", "");
    }
}