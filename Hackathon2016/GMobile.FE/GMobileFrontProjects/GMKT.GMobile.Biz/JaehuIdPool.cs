using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GMKT.GMobile.Data;
using GMKT.GMobile.Exceptions;

namespace GMKT.GMobile.Biz
{
	public sealed class JaehuIdPool
	{
		private static JaehuIdPool instance;

		private static TimeSpan updateTerm = TimeSpan.FromDays(1);
		private static Timer updateTimer;

		// data
		private List<JaehuT> jaehuIdList = null;

		#region [ Constructors ]
		private JaehuIdPool() { }

		private JaehuIdPool(List<JaehuT> incJaehuList)
		{
			this.jaehuIdList = incJaehuList;
		}
		#endregion

		#region [ Public Functions ]
		public bool IsValid(string jaehuid)
		{
			if (false == string.IsNullOrEmpty(jaehuid))
			{
				if (JaehuIdBiz.NAVER_MOBILE_JAEHUID.Equals(jaehuid))
				{
					return true;
				}
				else if (this.jaehuIdList != null)
				{
					return jaehuIdList.Exists(o => jaehuid.Equals(o.ContractCd));
				}
			}

			return false;
		}

		public string GetMobileJaehuId(string jaehuid)
		{
			if (false == string.IsNullOrEmpty(jaehuid))
			{
				string mobileJaehuId;
				if (JaehuIdBiz.PC_MOBILE_JAEHUID_DICTIONARY.TryGetValue(jaehuid, out mobileJaehuId))
				{
					return mobileJaehuId;
				}
				else
				{
					if (this.IsValid(jaehuid))
					{
						return jaehuid;
					}
				}
			}

			return JaehuIdBiz.DEFAULT_MOBILE_JAEHUID;
		}
		#endregion

		#region [ Functions for Update Pool Thread ]
		public static JaehuIdPool GetInstance()
		{
			if (instance == null)
			{
				lock (typeof(JaehuIdPool))
				{
					if (instance == null)
					{
						Thread thread = new Thread(UpdatePool);
						thread.Name = "JaehuIdPoolThread";
						thread.Start();
						thread.Join();
					}
				}
			}

			return instance;
		}

		private static void UpdatePool(object param)
		{
			if (updateTimer != null)
			{
				updateTimer.Dispose();
				updateTimer = null;
			}

			try
			{
				instance = GetPoolInternal();
			}
			catch (Exception ex)
			{
				if (!(ex is GMobileNotLoggingException))
				{
					ArcheFx.Diagnostics.Trace.WriteError(ex);
				}
			}

			updateTimer = new Timer(new TimerCallback(UpdatePool), null, updateTerm, TimeSpan.FromMilliseconds(-1));
		}

		private static JaehuIdPool GetPoolInternal()
		{
			List<JaehuT> list = new JaehuIdBiz().GetIncJaehuStringValueFromDB();

			if (list != null)
			{
				return new JaehuIdPool(list);
			}

			return instance;
		}
		#endregion
	}
}
