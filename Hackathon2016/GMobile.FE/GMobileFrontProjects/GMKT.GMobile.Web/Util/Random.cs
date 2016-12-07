using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace GMKT.GMobile.Web.Util
{
	public static class RandomUtil
	{
		public static string GetString( int size )
		{
			Random random = new Random( (int)DateTime.Now.Ticks );//thanks to McAden
			StringBuilder builder = new StringBuilder();
			char ch;
			for( int i = 0; i < size; i++ )
			{
				ch = Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * random.NextDouble() + 65 ) ) );
				builder.Append( ch );
			}

			return builder.ToString();
		}
	}
}
