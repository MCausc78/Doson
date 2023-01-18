namespace Doson;

public enum Boolean
{
	False = 0,
	None = 255,
	True = 1
};

public static class BooleanImpl
{
	public static Boolean FromBoolean(bool x) =>
		x
			? Boolean.True
			: Boolean.False;

	public static bool ToBool(this Boolean x) =>
		x == Boolean.True;

	public static Boolean And(this Boolean x, bool y)
		=> FromBoolean(x.ToBool() & y);

	public static Boolean And(this Boolean x, Boolean y)
		=> x.And(y.ToBool());

	public static Boolean Or(this Boolean x, bool y)
		=> FromBoolean(x.ToBool() | y);

	public static Boolean Or(this Boolean x, Boolean y)
		=> x.And(y.ToBool());

	public static Boolean Xor(this Boolean x, bool y)
		=> FromBoolean(x.ToBool() ^ y);

	public static Boolean Xor(this Boolean x, Boolean y)
		=> x.Xor(y.ToBool());

	public static Boolean Not(this Boolean x)
		=> FromBoolean(!x.ToBool());

	public static Boolean Nand(this Boolean x, bool y)
		=> x.Not().And(!y);

	public static Boolean Nand(this Boolean x, Boolean y)
		=> x.Nand(y.ToBool());

	public static Boolean Nor(this Boolean x, bool y)
		=> x.Not().Or(!y);

	public static Boolean Nor(this Boolean x, Boolean y)
		=> x.Nor(y.ToBool());


	public static Boolean Nxor(this Boolean x, bool y)
		=> x.Not().Xor(!y);
	public static Boolean Nxor(this Boolean x, Boolean y)
		=> x.Nxor(y.ToBool());

	public static T Ternary<T>(this Boolean x, T a, T b) =>
		x.ToBool()
			? a
			: b;
	public static string ToString(this Boolean x) =>
		x.Ternary<string>("true", "false");

	public static Boolean Copy(this Boolean x) => x switch
		{
			Boolean.False => Boolean.False,
			Boolean.True => Boolean.True,
			_ => Boolean.None,
		};
}
