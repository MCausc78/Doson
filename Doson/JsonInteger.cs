namespace Doson;

public class JsonInteger : IDosonObject
{
	public int Data
	{
		get;
		set;
	}
	public JsonInteger() =>
		this.Data = 0;
	public JsonInteger(int data) =>
		this.Data = data;
	public string Build() =>
		this.Data.ToString();		
	public IDosonObject Copy() =>
		new JsonInteger(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.INTEGER);
		bw.Write((int) this.Data);
	}
}
