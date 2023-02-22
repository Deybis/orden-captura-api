using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.Authorization.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthControllerAttribute : Attribute
    {
        public string ControllerName { get; set; }
    }
}
