using System.Text;
using Doson.Grammar;

namespace Doson;

public class JsonCharacter : IDosonObject
{
	public char Data
	{
		get;
		set;
	}
	public JsonCharacter(char val) =>
		this.Data = val;
	public string Build()
	{
		string result = this.Data switch
			{
				'\\'	=> "\\",
				'\''	=> "'",
				'\b'	=> "\\b",
				'\f'	=> "\\f",
				'\n'	=> "\\n",
				'\r'	=> "\\r",
				'\t'	=> "\\t",
				'\x7f'	=> "\\x7f",
				< ' '	=> $"\\x{this.Data.ToString():0<2X}",
				_		=> this.Data.ToString(),
			};
		return new StringBuilder(Token.START_CHAR)
			.Append(result)
			.Append(Token.END_CHAR)
			.ToString();
	}
	public IDosonObject Copy() =>
		new JsonCharacter(this.Data);

	public void WriteTo(BinaryWriter bw)
	{
		bw.Write(ObjectType.CHARACTER);
		bw.Write((char) this.Data);
	}
}
