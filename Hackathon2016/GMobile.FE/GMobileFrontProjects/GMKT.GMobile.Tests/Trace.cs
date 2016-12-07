using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using SysDiag = System.Diagnostics;
using System.Net;
using System.Web;
using SysTrace = System.Diagnostics.Trace;

namespace GMobile.Service.Tests
{
	public static class Trace
	{
		private const int LIST_MAX_SIZE = 6;

		public static string FormatTitleText(string title)
		{
			const string splitChars = "--------------------";
			string tileText = string.Format(
					"\r\n{0} {1} {2}",
					splitChars,
					title,
					splitChars);

			return tileText;
		}

		public static string GetNestedExceptionMessage(Exception ex)
		{
			string message = string.Format("{0}:{1}", ex.GetType().FullName, ex.Message);

			if (ex.InnerException != null)
			{
				message = string.Format(
				"{0}\r\n <- {1}",
				message,
				Trace.GetNestedExceptionMessage(ex.InnerException));
			}

			return message;
		}

		//public static string GetNestedExceptionMessageWithCategoryAndCode(Exception ex)
		//{
		//    string message = string.Format("{0}:{1}", ex.GetType().FullName, ex.Message);
		//    GmktException gmktEx = ex as GmktException;
		//    if (gmktEx != null)
		//    {
		//        message = string.Format("Category:{0}\r\nCode:{1}\r\n{2}",
		//            gmktEx.Category,
		//            gmktEx.Code,
		//            message);
		//    }

		//    if (ex.InnerException != null)
		//    {
		//        message = string.Format(
		//        "{0}\r\n <- {1}",
		//        message,
		//        Trace.GetNestedExceptionMessageWithCategoryAndCode(ex.InnerException));
		//    }

		//    return message;
		//}

		public static void Indent()
		{
			SysTrace.Indent();
		}

		public static void Unindent()
		{
			SysTrace.Unindent();
		}

		public static void Write(string text)
		{
			SysTrace.Write(text);
		}

		public static void WriteLine(string text)
		{
			SysTrace.WriteLine(text);
		}

		//public static void Write(TimeMark timeMark)
		//{
		//    string title = timeMark.Title;
		//    if (title == string.Empty)
		//    {
		//        SysDiag.StackFrame stackFrame = new SysDiag.StackFrame(1);
		//        title = stackFrame.GetMethod().Name;
		//    }

		//    string text = string.Format("[ElapedTime] {0} : {1}", title, timeMark.ElapsedTime.TotalMilliseconds);
		//    Trace.WriteLine(text);
		//}

		public static void DumpException(Exception ex)
		{
			Trace.WriteLine(Trace.GetNestedExceptionMessage(ex));
		}

		//public static void DumpExceptionEx(Exception ex)
		//{
		//    Trace.WriteLine(Trace.GetNestedExceptionMessageWithCategoryAndCode(ex));
		//}

		public static void DumpObjectValues(object obj)
		{
			DumpObjectValues(obj, null);
		}

		public static void DumpObjectValues(object obj, string title)
		{
			Type objectType = obj.GetType();
			if (string.IsNullOrEmpty(title) == true)
			{
				title = objectType.FullName;
			}

			Trace.WriteLine(Trace.FormatTitleText("[DUMP ObjectValues] " + title));

			FieldInfo[] fieldInfos = objectType.GetFields(
					BindingFlags.Instance
					| BindingFlags.Public);
			if (fieldInfos.Length > 0)
			{
				Trace.WriteLine("[Fields]");
			}
			try
			{
				Trace.Indent();

				foreach (FieldInfo fieldInfo in fieldInfos)
				{
					string name = fieldInfo.Name;
					string value = null;

					try
					{
						value = fieldInfo.GetValue(obj).ToString();
					}
					catch (Exception ex)
					{
						value = Trace.GetNestedExceptionMessage(ex);
					}

					string text = string.Format("{0} : {1}", name, value);
					Trace.WriteLine(text);
				}
			}
			finally
			{
				Trace.Unindent();
			}

			PropertyInfo[] propertyInfos = objectType.GetProperties(
					BindingFlags.Instance
					| BindingFlags.Public);
			if (propertyInfos.Length > 0)
			{
				Trace.WriteLine("[Properties]");
			}

			try
			{
				Trace.Indent();

				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					string name = propertyInfo.Name;
					string value = string.Empty;

					try
					{
						value = propertyInfo.GetValue(obj, null).ToString();
					}
					catch (Exception ex)
					{
						value = Trace.GetNestedExceptionMessage(ex);
					}

					string text = string.Format("{0} : {1}", name, value);
					Trace.WriteLine(text);
				}
			}
			finally
			{
				Trace.Unindent();
			}
		}

		//public static void DumpCookies(string title, CookieCollection cookies)
		//{
		//    Console.WriteLine(FormatTitleText(title));
		//    foreach (Cookie cookie in cookies)
		//    {
		//        Console.Write(HttpUtility.UrlDecode(cookie.Name) + ":");
		//        Console.WriteLine(HttpUtility.UrlDecode(cookie.Value));
		//    }
		//}

		public static void DumpNameValueCollection(string title, NameValueCollection nvColl)
		{
			Console.WriteLine(FormatTitleText(title));
			foreach (string key in nvColl.AllKeys)
			{
				Console.WriteLine("{0} : {1}", key, nvColl[key]);
			}
		}

		public static void DumpBusinessEntity(object entity)
		{
			Trace.DumpBusinessEntity(string.Empty, entity, false, false);
		}

		private static void DumpList(IList list)
		{
			try
			{
				Trace.Indent();

				bool writtenSkipChars = false;
				int headSize = (int)Math.Round(LIST_MAX_SIZE / 2.0);
				int tailSize = (int)Math.Truncate(LIST_MAX_SIZE / 2.0);

				for (int index = 0; index < list.Count; index++)
				{
					StringBuilder lineText = new StringBuilder();

					if (index > headSize - 1 && index < list.Count - tailSize)
					{
						if (writtenSkipChars == false)
						{
							WriteLine("... Skip ...");
							writtenSkipChars = true;
							continue;
						}
						else
						{
							continue;
						}
					}

					object item = list[index];

					Type objectType = item.GetType();

					PropertyInfo[] propertyInfos = objectType.GetProperties(
							BindingFlags.Instance
							| BindingFlags.Public);

					string text = string.Format("[{0}]{{{1}}} ",
							index,
							objectType.FullName);
					lineText.Append(text);
					//                    Trace.Write(text);

					int propIndex = 0;
					foreach (PropertyInfo propertyInfo in propertyInfos)
					{
						string name = propertyInfo.Name;
						object value = null;
						string valueString = string.Empty;

						if (name.StartsWith("_") == true)
						{
							continue;
						}
						Type propertyType = propertyInfo.PropertyType;

						TypeCode typeCode = Type.GetTypeCode(propertyType);
						if (typeCode == TypeCode.Object)
						{
							continue;
						}

						if (propertyType.IsValueType == true)
						{
							try
							{
								PropertyInfo specifiedProp = objectType.GetProperty(
										string.Format("__{0}Specified", name));
								if (specifiedProp != null)
								{
									if ((bool)specifiedProp.GetValue(item, null) != true)
									{
										continue;
									}
								}
							}
							catch
							{
							}
						}

						try
						{
							value = propertyInfo.GetValue(item, null);
							valueString = value.ToString();
						}
						catch
						{
							continue;
						}
						string format = ", {0}={1}";
						if (propIndex < 1)
						{
							format = "{0}={1}";
						}
						text = string.Format(format,
								name,
								valueString);
						lineText.Append(text);
						//                        Trace.Write(text);
						propIndex++;
					}
					Trace.WriteLine(lineText.ToString());
				}
			}
			finally
			{
				Trace.Unindent();
			}
		}

		private static void DumpBusinessEntityValues(object entity, bool displayNotSpecifiedValue, bool displayUnderscoreMember)
		{
			Type objectType = entity.GetType();

			PropertyInfo[] propertyInfos = objectType.GetProperties(
					BindingFlags.Instance
					| BindingFlags.Public);
			//if (propertyInfos.Length > 0)
			//{
			//    Trace.WriteLine("[Properties]");
			//}

			try
			{
				Trace.Indent();

				List<PropertyInfo> propInfoList = new List<PropertyInfo>();

				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					string name = propertyInfo.Name;
					object value = null;
					string valueString = string.Empty;

					// displayUnderscoreMember 값이 flase이고 속성명이 '_'문자로 시작하면
					// 표시하지 않도록 합니다.
					if (displayUnderscoreMember == false
							&& name.StartsWith("_") == true)
					{
						continue;
					}

					Type propertyType = propertyInfo.PropertyType;

					// displayNotSpecifiedValue 값이 false이고 __<속성명>Specified 값이 false이면 
					// 표시하지 않도록 합니다.
					if (displayNotSpecifiedValue == false
							&& propertyType.IsValueType == true)
					{
						try
						{
							PropertyInfo specifiedProp = objectType.GetProperty(
									string.Format("__{0}Specified", name));
							if (specifiedProp != null)
							{
								if ((bool)specifiedProp.GetValue(entity, null) != true)
								{
									continue;
								}
							}
						}
						catch
						{
						}
					}

					try
					{
						value = propertyInfo.GetValue(entity, null);
						if (value != null)
						{
							valueString = value.ToString();
						}
						else
						{
							valueString = "Null";
						}
					}
					catch (Exception ex)
					{
						valueString = Trace.GetNestedExceptionMessage(ex);
					}

					bool dumpChildEntity = false;
					TypeCode typeCode = Type.GetTypeCode(propertyType);
					if (typeCode == TypeCode.Object)
					{
						IList list = value as IList;
						//Type enumerable = propertyType.GetInterface(
						//    "System.Collections.IEnumerable");

						if (list != null)
						{
							propInfoList.Add(propertyInfo);
							continue;
						}
						else
						{
							dumpChildEntity = true;
						}
					}

					string text = string.Format("{0} : {1}", name, valueString);
					Trace.WriteLine(text);
					if (dumpChildEntity == true && value != null)
					{
						Trace.DumpBusinessEntityValues(value, displayNotSpecifiedValue, displayUnderscoreMember);
					}
				}

				foreach (PropertyInfo propertyInfo in propInfoList)
				{
					string name = propertyInfo.Name;
					object value = null;
					string valueString = string.Empty;

					try
					{
						value = propertyInfo.GetValue(entity, null);
						valueString = value.ToString();
					}
					catch (Exception ex)
					{
						valueString = Trace.GetNestedExceptionMessage(ex);
					}

					string text = string.Format("{0} : {1}", name, valueString);
					Trace.WriteLine(text);

					IList list = value as IList;
					if (list != null)
					{
						Trace.DumpList(list);
					}
				}

			}
			finally
			{
				Trace.Unindent();
			}
		}

		public static void DumpBusinessEntity(string title, object entity, bool displayNotSpecifiedValue, bool displayUnderscoreMember)
		{
			Type objectType = entity.GetType();
			if (string.IsNullOrEmpty(title) == true)
			{
				title = objectType.FullName;
			}

			Trace.WriteLine(Trace.FormatTitleText("[DUMP BusinessEntity] " + title));

			if (objectType.IsGenericType == true)
			{
				IList list = entity as IList;
				for (int i = 0; i < list.Count; i++)
				{
					Type listType = list[i].GetType();
					Trace.WriteLine(Trace.FormatTitleText("[DUMP BusinessEntity in List] " + listType.FullName));
					Trace.DumpBusinessEntityValues(list[i], displayNotSpecifiedValue, displayUnderscoreMember);
				}
			}
			else
			{
				Trace.DumpBusinessEntityValues(entity, displayNotSpecifiedValue, displayUnderscoreMember);
			}
		}
	}
}
