using System.Text;
using Doson.Grammar;

namespace Doson;

public class JsonString : IDosonObject
{
	public string Data
	{
		get;
		set;
	}
	public JsonString() =>
		this.Data = "";
	public JsonString(string data) =>
		this.Data = data;
	public string Build()
	{
		StringBuilder sb = new(this.Data
			.Replace("\\", "\\\\")
			.Replace("\"", "\\\"")
			.Replace("\b", "\\b")
			.Replace("\f", "\\f")
			.Replace("\n", "\\n")
			.Replace("\r", "\\r")
			.Replace("\t", "\\t"));
		for (int i = 0; i < sb.Length; ++i)
		{
			if (sb[i] < ' ')
			{
				char ch = sb[i];
				sb
					.Remove(i, 1)
					.Insert(i, $"\\x{ch:0<2X}");
			}
		}
		return new StringBuilder(Token.START_STRING)
			.Append(sb)
			.Append(Token.END_STRING)
			.ToString();
	}
	public IDosonObject Copy() =>
		new JsonString(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.STRING);
		byte[] bytes = Encoding.UTF8.GetBytes(this.Data);
		bw.Write(bytes.Length);
		bw.Write(bytes, 0, bytes.Length);
	}
}
