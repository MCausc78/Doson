namespace Doson;

public interface IDosonObject : IDosonBuildable,
	IDosonCopyable<IDosonObject>,
	IDosonBinarySerializable
{	
}
