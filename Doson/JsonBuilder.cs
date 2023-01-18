using System.Text;
using Doson.Grammar;

namespace Doson;

public class JsonBuilder : IDosonObject
{
	public Dictionary<string, IDosonObject> Dict
	{
		get;
		set;
	}
	public JsonBuilder()
	{
		this.Dict = new Dictionary<string, IDosonObject>();
	}
	public string Build()
	{
		StringBuilder sb = new();
		sb.Append(Token.OPEN_OBJECT);
		foreach (KeyValuePair<string, IDosonObject> pair in Dict)
		{
			if (!Token.OPEN_OBJECT.Equals(sb.ToString()))
			{
				sb.Append(Token.SEPARATOR);
			}
			sb
				.Append(new JsonString(pair.Key).Build())
				.Append(Token.PAIR_PART)
				.Append(pair.Value?.Build()
					?? "null");
		}
		return sb
			.Append(Token.CLOSE_OBJECT)
			.ToString();
	}
	public JsonBuilder Put(string k, bool b)
	{
		this.Dict[k] = new JsonBoolean(b);
		return this;
	}
	public JsonBuilder Put(string k, char c)
	{
		this.Dict[k] = new JsonCharacter(c);
		return this;
	}
	public JsonBuilder Put(string k, double d)
	{
		this.Dict[k] = new JsonDouble(d);
		return this;
	}
	public JsonBuilder Put(string k, float f)
	{
		this.Dict[k] = new JsonFloat(f);
		return this;
	}
	public JsonBuilder Put(string k, string s)
	{
		this.Dict[k] = new JsonString(s);
		return this;
	}
	public JsonBuilder Put(string k, IDosonObject o)
	{
		this.Dict[k] = o;
		return this;
	}
	public IDosonObject Copy()
	{
		JsonBuilder jb = new();
		foreach (KeyValuePair<string, IDosonObject> pair in Dict)
		{
			jb.Put(pair.Key, pair.Value?.Copy());
		}
		return jb;
	}

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.OBJECT);
		bw.Write(this.Dict.Count);
		foreach (KeyValuePair<string, IDosonObject> pair in this.Dict)
		{
			byte[] key = Encoding.UTF8.GetBytes(pair.Key);
			bw.Write(key.Length);
			bw.Write(key, 0, key.Length);
			if (pair.Value is null)
			{
				bw.Write(ObjectType.NULL);
			}
			else
			{
				pair.Value.WriteTo(bw);
			}
		}
	}
}
