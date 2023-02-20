using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.FileManager.Client.Models
{
    public class ArchivoExpediente : FileModel, IFileModel
    {
        public int CodigoInstanciaJuridica { get; set; }
        public int CodigoDependenciaJurisdiccional { get; set; }
        public string NumeroExpediente { get; set; }
    }
}
