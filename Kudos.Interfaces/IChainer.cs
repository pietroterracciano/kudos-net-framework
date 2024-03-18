namespace Kudos.Interfaces
{
    public interface IChainer<ChainType, ObjectType> 
        where ChainType : IChain<ObjectType>
    {
        ChainType CreateChain();
    }
}
