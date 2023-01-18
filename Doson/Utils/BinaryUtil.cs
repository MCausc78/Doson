using Doson;
using System.Text;

namespace Doson.Utils;

public static class BinaryUtil
{
	public static IDosonObject? ReadFrom(BinaryReader br)
	{
		byte type = br.ReadByte();
		int count;
		switch (type)
		{
			case ObjectType.ARRAY:
				JsonArray ja = new();
				count = br.ReadInt32();
				for (int i = 0; i < count; ++i)
				{
					ja.Append(ReadFrom(br));
				}
				return ja;
			case ObjectType.BOOLEAN:
				return new JsonBoolean(br.ReadByte() switch
				{
					0 => Boolean.False,
					1 => Boolean.True,
					_ => Boolean.None
				});
			case ObjectType.CHARACTER:
				return new JsonCharacter(br.ReadChar());
			case ObjectType.DECIMAL:
				return new JsonDecimal(br.ReadDecimal());
			case ObjectType.DOUBLE:
				return new JsonDouble(br.ReadDouble());
			case ObjectType.FLOAT:
				return new JsonFloat(br.ReadSingle());
			case ObjectType.INTEGER:
				return new JsonInteger(br.ReadInt32());
			case ObjectType.LONG:
				return new JsonLong(br.ReadInt64());
			case ObjectType.NULL:
				return null;
			case ObjectType.OBJECT:
				JsonBuilder jb = new();
				count = br.ReadInt32();
				for (int i = 0; i < count; ++i)
				{
					int len = br.ReadInt32();
					jb.Put(Encoding.UTF8.GetString(
						br.ReadBytes(len)), ReadFrom(br));
				}
				return jb;
			case ObjectType.STRING:
				count = br.ReadInt32();
				return new JsonString(Encoding.UTF8.GetString(br.ReadBytes(count)));
		}
		return null;
	}
}
