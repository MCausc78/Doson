namespace Doson;

public class JsonLong : IDosonObject
{
	public long Data
	{
		get;
		set;
	}
	public JsonLong() =>
		this.Data = 0;
	public JsonLong(long data) =>
		this.Data = data;
	public string Build() =>
		this.Data.ToString();		
	public IDosonObject Copy() =>
		new JsonLong(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.LONG);
		bw.Write((long) this.Data);
	}
}
