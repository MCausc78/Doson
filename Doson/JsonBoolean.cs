namespace Doson;

public class JsonBoolean : IDosonObject
{
	public Boolean Data
	{
		get;
		set;
	}
	public JsonBoolean() =>
		this.Data = Boolean.None;
	public JsonBoolean(bool val) =>
		this.Data = BooleanImpl.FromBoolean(val);

	public JsonBoolean(Boolean data) =>
		this.Data = data;

	public JsonBoolean(JsonBoolean jb) =>
		this.Data = jb.Data.Copy();
	public string Build() =>
		this.Data.ToString();

	public IDosonObject Copy() =>
		new JsonBoolean(this.Data.Copy());
	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.BOOLEAN);
		bw.Write((byte) (this.Data switch
		{
			Boolean.False => 0,
			Boolean.None => 255,
			Boolean.True => 1,
			_ => 128,
		}));
	}
}
