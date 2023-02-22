using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Seje.Authorization.Service.Infrastructure;
using Seje.Authorization.Service.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.Authorization.Service.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        private readonly ConfigurationModel configurationModel;
        private readonly IPermissionService permissionService;

        public PermissionFilter(IPermissionService permissionService, IOptions<ConfigurationModel> options)
        {
            this.configurationModel = options.Value;
            this.permissionService = permissionService;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var hasAttribute = context.ActionDescriptor.EndpointMetadata.OfType<Attributes.SkipAuthorizeAttribute>().Any();
            if (hasAttribute) return base.OnActionExecutionAsync(context, next);

            var controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();
            var actionName = context.HttpContext.Request.RouteValues["action"].ToString();

            var controllerDesc = context.ActionDescriptor.EndpointMetadata.OfType<Attributes.AuthControllerAttribute>().FirstOrDefault();
            var actionDesc = context.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>().FirstOrDefault();

            if (controllerDesc != null)
                controllerName = controllerDesc.ControllerName ?? controllerName;

            if (actionDesc != null)
                actionName = actionDesc.Name ?? actionName;

            if (!(context.HttpContext.User.Identity.IsAuthenticated &&
                permissionService.ValidatePermission(context.HttpContext.User.Identity.Name, configurationModel.Component, controllerName, actionName).Result))
            {
                context.Result = new ForbidResult();
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
