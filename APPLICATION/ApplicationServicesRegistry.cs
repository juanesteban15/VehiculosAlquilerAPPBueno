// ============================================================
// ARCHIVO: APPLICATION/ApplicationServicesRegistry.cs
// ============================================================
// Registra todos los UseCases y Validators en el contenedor
// Program.cs lo llama con una sola línea: AddApplicationServices()

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.UseCases.Chat.Commands.EnviarMensajeChat;
using VehiculosAlquilerApp.Application.UseCases.Chat.Queries.GetChatDetalle;
using VehiculosAlquilerApp.Application.UseCases.Notificaciones.Queries.GetNotificacionesUsuario;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.AceptarReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CambiarEstadoReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CrearReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.GetReservasList;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.RechazarReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetMisReservas;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetReservasRecibidas;
using VehiculosAlquilerApp.Application.UseCases.Tarifas.CrearTarifas;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.ActualizarPerfil;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearComentarioPerfil;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.GetUsuariosList;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilEditar;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilPublico;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.RegistrarDocumentoUsuario;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CambiarEstadoVehiculo;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CrearVehiculo;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.GetVehiculosList;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculoDetalle;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculosHome;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetCrearVehiculoCatalogos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            // Transient = nueva instancia cada vez
            // El Mediator se crea nuevo en cada llamada
            services.AddTransient<IMediator, SimpleMediator>();

            // --- UseCases de Vehículos ---
            // Scoped = uno por request HTTP
            services.AddScoped
                <IRequestHandler<CrearVehiculoCommand, string>,
                CrearVehiculoUseCase > ();

            services.AddScoped
                <IRequestHandler<CambiarEstadoVehiculoCommand>,
                CambiarEstadoVehiculoUseCase > ();

            services.AddScoped
                <IRequestHandler <GetVehiculosListQuery, PaginationResponse<VehiculoListItemDTO>>,
                GetVehiculosListUseCase > ();

            services.AddScoped
                <IRequestHandler<GetVehiculosHomeQuery, VehiculosHomeResult>,
                GetVehiculosHomeUseCase>();

            services.AddScoped
                <IRequestHandler<GetVehiculoDetalleQuery, VehiculoDetalleDto?>,
                GetVehiculoDetalleUseCase>();

            services.AddScoped
                <IRequestHandler<GetCrearVehiculoCatalogosQuery, CrearVehiculoCatalogosDto>,
                GetCrearVehiculoCatalogosUseCase>();

            // --- UseCases de Usuarios ---
            services.AddScoped
                <IRequestHandler<CrearUsuarioCommand, int>,
                CrearUsuarioUseCase > ();

            services.AddScoped
                <IRequestHandler<GetUsuariosListQuery, PaginationResponse<UsuarioListItemDTO>>,
                GetUsuariosListUseCase > ();

            services.AddScoped
                <IRequestHandler<ActualizarPerfilCommand>,
                ActualizarPerfilUseCase>();

            services.AddScoped
                <IRequestHandler<RegistrarDocumentoUsuarioCommand>,
                RegistrarDocumentoUsuarioUseCase>();

            services.AddScoped
                <IRequestHandler<CrearComentarioPerfilCommand>,
                CrearComentarioPerfilUseCase>();

            services.AddScoped
                <IRequestHandler<GetPerfilEditarQuery, PerfilEdicionDto?>,
                GetPerfilEditarUseCase>();

            services.AddScoped
                <IRequestHandler<GetPerfilPublicoQuery, PerfilPublicoResult>,
                GetPerfilPublicoUseCase>();

            // --- UseCases de Reservas ---
            services.AddScoped
                <IRequestHandler<CrearReservaCommand, int>,
                CrearReservaUseCase > ();

            services.AddScoped
                <IRequestHandler<AceptarReservaCommand>,
                AceptarReservaUseCase > ();

            services.AddScoped
                <IRequestHandler<RechazarReservaCommand>,
                RechazarReservaUseCase>();

            services.AddScoped
                <IRequestHandler<CambiarEstadoReservaCommand>,
                CambiarEstadoReservaUseCase > ();

            services.AddScoped
                <IRequestHandler<GetReservasListQuery, PaginationResponse<ReservaListItemDTO>>,
                GetReservasListUseCase > ();

            services.AddScoped
                <IRequestHandler<GetMisReservasQuery, List<ReservaResumenDto>>,
                GetMisReservasUseCase>();

            services.AddScoped
                <IRequestHandler<GetReservasRecibidasQuery, List<ReservaResumenDto>>,
                GetReservasRecibidasUseCase>();

            // --- UseCases de Notificaciones ---
            services.AddScoped
                <IRequestHandler<GetNotificacionesUsuarioQuery, List<NotificacionDto>>,
                GetNotificacionesUsuarioUseCase>();

            // --- UseCases de Chat ---
            services.AddScoped
                <IRequestHandler<GetChatDetalleQuery, ChatDetalleDto?>,
                GetChatDetalleUseCase>();

            services.AddScoped
                <IRequestHandler<EnviarMensajeChatCommand>,
                EnviarMensajeChatUseCase>();

            // --- UseCases de Tarifas ---
            services.AddScoped
                <IRequestHandler<CrearTarifaCommand, int>,
                CrearTarifaUseCase > ();

            // Registra automáticamente todos los Validators de FluentValidation
            // Busca todas las clases AbstractValidator<> en el assembly de Application
            services.AddValidatorsFromAssemblyContaining<CrearVehiculoCommandValidator>();

            return services;
        }
    }
}
