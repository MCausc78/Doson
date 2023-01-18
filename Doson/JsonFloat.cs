using System.Globalization;

namespace Doson;

public class JsonFloat : IDosonObject
{
	private static readonly CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
	public float Data
	{
		get;
		set;
	}
	public JsonFloat() =>
		this.Data = 0.0F;
	public JsonFloat(float data) =>
		this.Data = data;
	public string Build() =>
		this.Data.ToString("g", provider);
	
	public IDosonObject Copy() =>
		new JsonFloat(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.FLOAT);
		bw.Write((float) this.Data);
	}
}
