using System;
using System.Collections.Generic;
using System.Text;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.AceptarReserva
{
    public class AceptarReservaCommand : IRequest
    {
        public required int ReservaId { get; set; }
        public required int DuenioId { get; set; }
    }
}
