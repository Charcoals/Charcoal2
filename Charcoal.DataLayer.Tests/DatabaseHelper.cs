using Charcoal.Common.Config;

namespace Charcoal.DataLayer.Tests
{
    public class DatabaseHelper
    {
    	public static string GetConnectionString()
        {
					return CharcoalConfig.Instance.TestConnectionString;
        }

    }
}