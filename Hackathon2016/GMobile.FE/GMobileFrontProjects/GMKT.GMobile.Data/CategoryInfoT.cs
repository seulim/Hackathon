using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	public enum CategoryLevel
	{
		GroupCategory = 0,
		LargeCategory = 1,
		MediumCategory = 2,
		SmallCategory = 3,
		DetailCategory = 4,
		None = 5
	}

	public class CategoryInfoT
	{
		private string id;
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int ItemCount { get; set; }

		private CategoryLevel level;
		public CategoryLevel Level
		{
			get { return level; }
			set { level = value; }
		}
	}
}
