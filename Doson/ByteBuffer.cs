using System.Text;

namespace Doson;
public class ByteBuffer
{
	private byte[] Data {
		get;
		set;
	}
	public ByteBuffer()
	{
		Data = Array.Empty<byte>();
	}
	public ByteBuffer(byte[] data) =>
		this.Data = data
			?? throw new ArgumentNullException("Data is null");
	public ByteBuffer AppendBytes(byte[] vals)
	{
		if (vals.Length == 0)
		{
			return this;
		}
		byte[] result = new byte[Data.Length + vals.Length];
		if (Data.Length > 0)
		{
			Array.Copy(Data, 0, result, 0, Data.Length);
		}
		Data = result;
		Array.Copy(vals, vals.Length, this.Data, 0, vals.Length);
		return this;
	}
	public ByteBuffer AppendByte(byte val)
	{
		byte[] result = new byte[Data.Length + 1];
		if (Data.Length > 0)
		{
			Array.Copy(Data, 0, result, 0, Data.Length);
		}
		Data = result;
		Data[^1] = val;
		return this;
	}
	public ByteBuffer AppendShort(short val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendUShort(ushort val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendInt(int val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendUInt(uint val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendLong(long val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendULong(ulong val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendBoolean(bool val) => this.AppendByte(BitConverter.GetBytes(val)[0]);
	public ByteBuffer AppendFloat(float val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendDouble(double val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendChar(char val) => this.AppendBytes(BitConverter.GetBytes(val));
	public ByteBuffer AppendString(string val)
	{
		byte[] result = Encoding.UTF8.GetBytes(val);
		return this
			.AppendInt(result.Length)
			.AppendBytes(result);
	}
}
