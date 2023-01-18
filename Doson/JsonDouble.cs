using System.Globalization;

namespace Doson;

public class JsonDouble : IDosonObject
{
	private static readonly CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
	public double Data
	{
		get;
		set;
	}
	public JsonDouble() =>
		this.Data = 0.0D;
	public JsonDouble(double data) =>
		this.Data = data;
	public string Build() =>
		this.Data.ToString("g", provider);

	public IDosonObject Copy() =>
		new JsonDouble(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.DOUBLE);
		bw.Write((double) this.Data);
	}
}
