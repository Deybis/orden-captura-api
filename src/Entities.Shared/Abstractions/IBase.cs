using System;

namespace Entities.Shared.Abstractions
{
    public interface IBase
    {
    }

    public interface IBase<TKey, TUserKey> : IBase
    {
        TKey Id { get; set; }
        TUserKey CreatedById { get; set; }
        DateTime CreationDate { get; set; }
        TUserKey ModifiedById { get; set; }
        DateTime ModificationDate { get; set; }
        bool Removed { get; set; }
    }
}
