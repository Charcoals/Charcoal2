using Charcoal.Common.Config;
using NUnit.Framework;

namespace Charcoal.Core.Tests {
	[TestFixture]
	public class ConfigReaderTest {

		[Test]
		public void CanReadValidConfig() {
			const string config = @"
testconnection: yoyo
prodconnection: jawn";

			ConfigReader.Parse(config);

			Assert.AreEqual("yoyo", CharcoalConfig.Instance.TestConnectionString);
			Assert.AreEqual("jawn", CharcoalConfig.Instance.ProductionConnectionString);
		}

	}
}