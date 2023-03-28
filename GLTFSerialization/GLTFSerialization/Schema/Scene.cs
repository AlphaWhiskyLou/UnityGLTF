using Newtonsoft.Json;
using System.Collections.Generic;

namespace GLTF.Schema
{
	public class Scene : GLTFChildOfRootProperty
	{
		public List<NodeId> Nodes;

		public static Scene Deserialize(GLTFRoot root, JsonReader reader)
		{
			Scene scene = new Scene();
			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				if (reader.Value.ToString() == "nodes")
					scene.Nodes = NodeId.ReadList(root, reader);
				else
					scene.DefaultPropertyDeserializer(root, reader);
			}
			return scene;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();
			if (this.Nodes != null && this.Nodes.Count > 0)
			{
				writer.WritePropertyName("nodes");
				writer.WriteStartArray();
				foreach (NodeId node in this.Nodes)
					writer.WriteValue(node.Id);
				writer.WriteEndArray();
			}
			base.Serialize(writer);
			writer.WriteEndObject();
		}
	}
}
