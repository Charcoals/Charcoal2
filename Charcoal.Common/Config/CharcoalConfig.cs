using System.IO;

namespace Charcoal.Common.Config {
	public interface ICharcoalConfig {
		string ProductionConnectionString { get; set; }
		string TestConnectionString { get; set; }
		void Parse(string filePath = "user.yaml");
	}

	public class CharcoalConfig : ICharcoalConfig {
		
		private static ICharcoalConfig m_config = new CharcoalConfig();
		public static ICharcoalConfig Instance {
			get { return m_config; }
			set { m_config = value; }
		}
		public string ProductionConnectionString { get; set; }
		public string TestConnectionString { get; set; }

		internal CharcoalConfig() {
			ProductionConnectionString = TestConnectionString = string.Empty;
		}

		public void Parse(string filePath="user.yaml") {
			if(File.Exists(filePath)) {
				ConfigReader.Parse(File.ReadAllText(filePath));
			}
		}
	}
}