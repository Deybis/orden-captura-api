using System;

namespace Entities.Shared.Audit
{
    public static class AuditExtensions
    {
        public static IAuditable CreateAuditable<T>(string UserName, DateTime CreatedDate)
            where T : IAuditable
        {
            Type type = typeof(T);
            var audit = (T)Activator.CreateInstance(type);
            audit.SetAsCreated(UserName, CreatedDate);
            return audit;
        }
        public static void SetAsCreated(this IAuditable Auditable, string UserName, DateTime CreatedDate)
        {
            Auditable.FechaCreacion = CreatedDate;
            Auditable.FechaModificacion = CreatedDate;
            Auditable.UsuarioCreacion = UserName;
            Auditable.UsuarioModificacion = UserName;
        }
        public static void SetAsUpdated(this IAuditable Auditable, string UserName, DateTime CreatedDate)
        {
            Auditable.FechaModificacion = CreatedDate;
            Auditable.UsuarioModificacion = UserName;
        }
    }
}
