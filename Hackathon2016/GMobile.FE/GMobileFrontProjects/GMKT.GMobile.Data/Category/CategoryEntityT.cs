using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
    public class GetGroupCategoryInfo : ApiResponseBase
    {
        public List<GroupCategoryInfo> Data { get; set; }
    }
    
    public class GroupCategoryInfo
    {
        public int Code { get; set; }
        public string Name { get; set; }
		public string CssClass { get; set; }
        public string LinkURL { get; set; }
        public string IconOnURL { get; set; }
        public string IconOffURL { get; set; }
        public List<CategoryInfo> LargeCategoryList { get; set; }
    }

    public class CategoryInfo
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LinkURL { get; set; }
    }
    
    public class CategoryItem
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }

        public List<CategoryItem> ChildCategoryList { get; set; }

        //public CategoryItem(string CategoryCode, string CategoryName)
        //{
        //    this.CategoryCode = CategoryCode;
        //    this.CategoryName = CategoryName;
        //}

        //public CategoryItem(string CategoryCode, string CategoryName, List<CategoryItem> ChildCategoryList)
        //{
        //    this.CategoryCode = CategoryCode;
        //    this.CategoryName = CategoryName;

        //    this.ChildCategoryList = ChildCategoryList;
        //}

        //public bool HaveChildCategoryList()
        //{
        //    if (this.ChildCategoryList != null && this.ChildCategoryList.Count > 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
