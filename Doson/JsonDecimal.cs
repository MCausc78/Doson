using System.Globalization;

namespace Doson;

public class JsonDecimal : IDosonObject
{
	private static readonly CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
	public decimal Data
	{
		get;
		set;
	}
	public JsonDecimal() =>
		this.Data = 0.0M;
	public JsonDecimal(decimal data) =>
		this.Data = data;
	public string Build() =>
		this.Data.ToString("g", provider);
	public IDosonObject Copy() =>
		new JsonDecimal(this.Data);
	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.DECIMAL);
		bw.Write((decimal) this.Data);
	}
}
