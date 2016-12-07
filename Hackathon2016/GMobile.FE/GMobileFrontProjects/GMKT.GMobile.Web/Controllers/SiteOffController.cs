using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Text;
using System.Globalization;
using GMKT.GMobile.Web.Models;

namespace GMKT.GMobile.Web.Controllers
{
	public class SiteOffController : Controller
	{
		public ActionResult Index()
		{
			string template =
				"<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
				"<siteOff mode=\"{0}\" redirectUrl=\"{1}\" siteOffTime=\"{2}\" siteOnTime=\"{3}\" />";

			MaintenanceConfig config = ConfigManager.MaintenanceConfig;

			if (config.schedule == null) config.schedule = new maintenanceSchedule();

			string content = String.Format(template,
				config.IsInMaintenance() ? "on" : "off",
				config.targetUrl + "?dt=" + config.schedule.end.ToString("HH:mm"),
				config.schedule.begin.ToString("M/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
				config.schedule.end.ToString("M/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));

			return Content(content, "text/html", Encoding.UTF8);
		}

		public ActionResult Notice()
		{
			SiteOffM model = new SiteOffM();

			MaintenanceConfig config = ConfigManager.MaintenanceConfig;

			if (config.schedule == null) config.schedule = new maintenanceSchedule();

			model.EndDate = config.schedule.end.ToString("HH:mm", CultureInfo.InvariantCulture);

			return View(model);
		}

		#region Inner classes

		static class ConfigLoader
		{
			public static T LoadConfig<T>() where T : class, new()
			{
				return LoadConfig<T>(null);
			}

			public static T LoadConfig<T>(string fileName) where T : class, new()
			{
				if (string.IsNullOrEmpty(fileName))
				{
					fileName = System.Web.HttpContext.Current.Server.MapPath(string.Concat("~/", typeof(T).Name, ".config"));
				}

				if (!Path.IsPathRooted(fileName))
				{
					fileName = System.Web.HttpContext.Current.Server.MapPath(fileName);
				}

				string cacheKey = fileName;
				T configObj = HttpRuntime.Cache[cacheKey] as T;
				if (configObj == null)// Here we try populate the config from cache
				{
					configObj = LoadFromXml<T>(fileName);
					if (configObj == null)
						// insert the config instance into cache use CacheDependency
						HttpRuntime.Cache.Insert(cacheKey, configObj, new System.Web.Caching.CacheDependency(fileName));
				}
				return configObj;
			}

			private static T LoadFromXml<T>(string fileName) where T : class, new()
			{
				try
				{
					using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
					{
						return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
					}
				}
				catch
				{
					return new T();
				}
			}
		}

		static class ConfigManager
		{
			public static MaintenanceConfig MaintenanceConfig
			{
				get
				{
					return ConfigLoader.LoadConfig<MaintenanceConfig>("~/pm.config");
				}
			}
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		[XmlRoot("maintenance", Namespace = "", IsNullable = false)]
		public partial class MaintenanceConfig
		{
			/// <remarks/>
			public maintenanceSchedule schedule { get; set; }

			/// <remarks/>
			public maintenanceBackdoor backdoor { get; set; }

			/// <remarks/>
			[XmlAttribute()]
			public long id { get; set; }

			/// <remarks/>
			[XmlAttribute()]
			public string name { get; set; }

			/// <remarks/>
			[XmlAttribute()]
			public string criterion { get; set; }

			/// <remarks/>
			[XmlAttribute()]
			public string targetUrl { get; set; }

			public bool IsInMaintenance()
			{
				switch (criterion)
				{
					case "InOperation": return false;
					case "InMaintenance": return true;
					case "BySchedule":
						DateTime begin = schedule.begin;
						DateTime end = schedule.end;
						DateTime now = DateTime.Now;
						return begin <= now && now < end;
					default: return false;
				}
			}
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		public partial class maintenanceSchedule
		{
			/// <remarks/>
			[XmlAttribute()]
			public DateTime begin { get; set; }

			/// <remarks/>
			[XmlAttribute()]
			public DateTime end { get; set; }
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		public partial class maintenanceBackdoor
		{
			/// <remarks/>
			[XmlArrayItem("add", IsNullable = false)]
			public IPRangeItem[] ipRanges { get; set; }

			/// <remarks/>
			[XmlArrayItem("add", IsNullable = false)]
			public PageItem[] pages { get; set; }

			/// <remarks/>
			[XmlArrayItem("add", IsNullable = false)]
			public CookieItem[] cookies { get; set; }
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		public partial class IPRangeItem
		{
			/// <remarks/>
			[XmlAttribute("cidr")]
			public string Cidr { get; set; }
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		public partial class PageItem
		{
			/// <remarks/>
			[XmlAttribute("page")]
			public string Page { get; set; }
		}

		/// <remarks/>
		[XmlType(AnonymousType = true)]
		public partial class CookieItem
		{
			/// <remarks/>
			[XmlAttribute("name")]
			public string Name { get; set; }

			/// <remarks/>
			[XmlAttribute("key")]
			public string Key { get; set; }

			/// <remarks/>
			[XmlAttribute("value")]
			public string Value { get; set; }
		}

		#endregion
	}
}
