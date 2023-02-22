using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.Authorization.Service.Models
{
    public class Permission
    {
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("parentMenuId")]
		public Guid? ParentMenuId { get; set; }
		[JsonProperty("area")]
		public string Area { get; set; }
		[JsonProperty("controllerName")]
		public string ControllerName { get; set; }
		[JsonProperty("actionName")]
		public string ActionName { get; set; }
		[JsonProperty("isExternal")]
		public bool IsExternal { get; set; }
		[JsonProperty("externalUrl")]
		public string ExternalUrl { get; set; }
		[JsonProperty("target")]
		public string Target { get; set; }
		[JsonProperty("icon")]
		public string Icon { get; set; }
		[JsonProperty("displayOrder")]
		public int DisplayOrder { get; set; }
		[JsonProperty("visible")]
		public bool Visible { get; set; }
	}
}
