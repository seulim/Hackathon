using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using GMKT.GMobile.Util;
using System.Collections;

namespace GMKT.GMobile.Biz
{
	public class CategoryBiz
	{
		public static CategoryInfoT GetCategoryInfo(string categoryID)
		{
			CategoryInfoT category = new CategoryInfoT();

			if (categoryID.Length != 9) return category;

			category.Id = categoryID;
			category.Name = CategoryUtil.GetCategoryName(categoryID);

			if (categoryID.StartsWith("1"))
			{
				category.Level = CategoryLevel.LargeCategory;
			}
			else if (categoryID.StartsWith("2"))
			{
				category.Level = CategoryLevel.MediumCategory;
			}
			else if (categoryID.StartsWith("3"))
			{
				category.Level = CategoryLevel.SmallCategory;
			}

			return category;
		}

		public List<CategoryInfo> GetCategory(string type, string code)
		{
			ArrayList categoryList = new ArrayList();
			List<CategoryInfo> cList = new List<CategoryInfo>();

			string categoryType = "";

			if (type == "G")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("T", "");
				categoryType = "L";
			}
			else if (type == "L")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("L", code);
				categoryType = "M";
			}
			else
			{
				string mCode = code;
				if (type == "S")
				{
					mCode = CategoryUtil.GetParentCategoryCode(code);
				}

				categoryList = CategoryUtil.GetXMLCategoryList("M", mCode);
				categoryType = "S";
			}

			string categoryCode = "", name = "";

			if (categoryList != null && categoryList.Count > 0)
			{
				for (int i = 0; i < categoryList.Count; i++)
				{
					if (i % 2 == 0)
					{
						categoryCode = categoryList[i].ToString();
					}
					else
					{
                        if ("200002474".Equals(categoryCode.ToString()) || "200002481".Equals(categoryCode.ToString())) continue;

						name = categoryList[i].ToString();
						CategoryInfo c = new CategoryInfo();
						c.Type = categoryType;
						c.Code = categoryCode;
						c.Name = name;
						cList.Add(c);
					}
				}
			}

			return cList;
		}

		public List<CategoryInfo> GetCategory(string gdlc, string gdmc, string gdsc)
		{
			ArrayList categoryList = new ArrayList();
			List<CategoryInfo> cList = new List<CategoryInfo>();

			string code = "", name = "";
			string categoryType = "";

			if (gdlc == "" && gdmc == "" && gdsc == "")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("T", "");
				categoryType = "L";
			}
			else if (gdlc != "" && gdmc == "" && gdsc == "")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("L", gdlc);
				categoryType = "M";
			}
			else
			{
				categoryList = CategoryUtil.GetXMLCategoryList("M", gdmc);
				categoryType = "S";
			}

			for (int i = 0; i < categoryList.Count; i++)
			{
				if (i % 2 == 0)
				{
					code = categoryList[i].ToString();
				}
				else
				{
					name = categoryList[i].ToString();
					CategoryInfo c = new CategoryInfo();
					c.Type = categoryType;
					c.Code = code;
					c.Name = name;
					cList.Add(c);
				}
			}

			return cList;
		}
	}
}
