using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class CentroCostoDto: EntityDto<int>
    {
     
        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }
    }
}
