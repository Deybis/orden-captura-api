using System;

namespace Entities.Shared.Model
{
    public class Funcionario
    {
        public Guid FuncionarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public string OrganoJurisdiccional { get; set; }
        public string DespachoSala { get; set; }
        public string Cargo { get; set; }
        public string Puesto { get; set; }
        public string Correo { get; set; }
    }
}
