namespace Entities.Shared.Abstractions
{
    public interface IDtoBase
    {
    }

    public interface IDtoBase<TKey, TUserKey> : IBase<TKey, TUserKey>, IDtoBase
    {

    }
}
