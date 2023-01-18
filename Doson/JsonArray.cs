using System.Text;
using Doson.Grammar;

namespace Doson;

public class JsonArray : IDosonObject
{
	public List<IDosonObject> Values
	{
		get;
		set;
	}
	public JsonArray()
	{
		this.Values = new List<IDosonObject>();
	}
	public string Build()
	{
		StringBuilder sb = new();
		sb.Append(Token.OPEN_ARRAY);
		foreach (IDosonObject obj in this.Values)
		{
			if (!Token.OPEN_ARRAY.Equals(sb.ToString()))
			{
				sb.Append(Token.SEPARATOR);
			}
			sb.Append(obj?.Build()
				?? "null");
		}
		return sb
			.Append(Token.CLOSE_ARRAY)
			.ToString();
	}
	public JsonArray Append(bool b)
	{
		this.Values.Add(new JsonBoolean(b));
		return this;
	}
	public JsonArray Append(char c)
	{
		this.Values.Add(new JsonCharacter(c));
		return this;
	}
	public JsonArray Append(double d)
	{
		this.Values.Add(new JsonDouble(d));
		return this;
	}
	public JsonArray Append(float f)
	{
		this.Values.Add(new JsonFloat(f));
		return this;
	}
	public JsonArray Append(string s)
	{
		this.Values.Add(new JsonString(s));
		return this;
	}
	public JsonArray Append(IDosonObject o)
	{
		this.Values.Add(o);
		return this;
	}
	public IDosonObject Copy()
	{
		JsonArray ja = new();
		foreach (IDosonObject obj in this.Values)
		{
			ja.Append(obj.Copy());
		}
		return ja;
	}

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.ARRAY);
		bw.Write(this.Values.Count);
		foreach (IDosonObject obj in this.Values)
		{
			if (obj is null)
			{
				bw.Write(ObjectType.NULL);
			}
			else
			{
				obj.WriteTo(bw);
			}
		}
	}
}
