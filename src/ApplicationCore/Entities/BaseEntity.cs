using System;

namespace Vimo.ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }

    public abstract class UpsertEntity : InsertableEntity
    {
        public DateTimeOffset? UpdatedAt { get; private set; }

        public void SetUpdate()
        {
            UpdatedAt = DateTime.Now;
        }
    }

    public abstract class InsertableEntity : BaseEntity
    {
        public DateTimeOffset InsertedAt { get; protected set; } = DateTime.UtcNow;

        public void SetInsert()
        {
            InsertedAt = DateTime.Now;
        }
    }

    public abstract class SoftDeleteEntity : InsertableEntity
    {
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; protected set; }

        public void SetDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}