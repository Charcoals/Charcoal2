using System.IO;
using YamlDotNet.RepresentationModel;
using System.Linq;
using System.Collections.Generic;

namespace Charcoal.Common.Config {
	public class ConfigReader {
		public static void Parse(string content) {
			var input = new StringReader(content);
			var yaml = new YamlStream();
			yaml.Load(input);

			var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
			
			foreach (var entry in mapping.Children) {
				if (((YamlScalarNode)entry.Key).Value.Equals("testconnection")) {
					CharcoalConfig.Instance.TestConnectionString = ((YamlScalarNode)entry.Value).Value;
				}

				if (((YamlScalarNode)entry.Key).Value.Equals("prodconnection")) {
					CharcoalConfig.Instance.ProductionConnectionString = ((YamlScalarNode)entry.Value).Value;
				}

			}
		}
	}
}