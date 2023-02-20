using System;

namespace Entities.Shared.Audit
{
    public interface IAuditable
    {
        DateTime FechaCreacion { get; set; }
        string UsuarioCreacion { get; set; }
        DateTime FechaModificacion { get; set; }
        string UsuarioModificacion { get; set; }
    }
}
