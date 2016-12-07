using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Biz
{
	public static class GenericUtil
	{
		public static List<T> AtLeastReturnEmptyList<T>(IEnumerable<T> input)
		{
			if(input == null)
			{
				return new List<T>();
			}
			else
			{
				return input.ToList();
			}
		}

		public static T AtLeastReturnNewObjct<T>(T input) where T : new()
		{
			if(input == null)
			{
				return new T();
			}
			else
			{
				return input;
			}
		}
	}
}
