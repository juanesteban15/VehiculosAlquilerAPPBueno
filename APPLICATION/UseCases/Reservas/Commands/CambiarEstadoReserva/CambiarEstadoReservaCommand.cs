using System;
using System.Collections.Generic;
using System.Text;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CambiarEstadoReserva
{
    public class CambiarEstadoReservaCommand:IRequest
    {
        public required int ReservaId { get; set; }
        public required int UsuarioId { get; set; }   
        public required string NuevoEstado { get; set; }



    }
}
