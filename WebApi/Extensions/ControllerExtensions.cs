using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi.Extensions
{
    public static class ControllerExtensions
    {
        [NonAction]
        public static StatusCodeResult NotModified(this ControllerBase controller)
            => controller.StatusCode(304);

        public static ObjectResult NotModified(this ControllerBase controller, [ActionResultObjectValue] object? value)
            => controller.StatusCode(304, value);
    }
}
