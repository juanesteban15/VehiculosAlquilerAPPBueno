using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculoAlquilerAPP.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class ColoresLicenciaTransito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[dbo].[Colores]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [dbo].[Colores](
                        [Id] int IDENTITY(1,1) NOT NULL,
                        [Nombre] nvarchar(30) NOT NULL,
                        [Activo] bit NOT NULL CONSTRAINT [DF_Colores_Activo] DEFAULT CAST(1 AS bit),
                        CONSTRAINT [PK_Colores] PRIMARY KEY ([Id])
                    );

                    CREATE UNIQUE INDEX [IX_Colores_Nombre] ON [dbo].[Colores]([Nombre]);
                END;

                IF COL_LENGTH(N'dbo.VehiculoColores', N'ColorVehiculoId') IS NULL
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores] ADD [ColorVehiculoId] int NULL;
                END;
                """);

            migrationBuilder.Sql("""
                IF COL_LENGTH(N'dbo.VehiculoColores', N'Orden') IS NULL
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores] ADD [Orden] int NOT NULL CONSTRAINT [DF_VehiculoColores_Orden] DEFAULT 1;
                END;
                """);

            migrationBuilder.Sql("""
                ;WITH ColoresOrdenados AS
                (
                    SELECT [VehiculoPlaca], [Id],
                           ROW_NUMBER() OVER(PARTITION BY [VehiculoPlaca] ORDER BY [Id]) AS [NuevoOrden]
                    FROM [dbo].[VehiculoColores]
                )
                UPDATE vc
                SET [Orden] = co.[NuevoOrden]
                FROM [dbo].[VehiculoColores] vc
                INNER JOIN ColoresOrdenados co ON co.[VehiculoPlaca] = vc.[VehiculoPlaca] AND co.[Id] = vc.[Id]
                WHERE co.[NuevoOrden] BETWEEN 1 AND 3;
                """);

            migrationBuilder.Sql("""
                ;WITH ColoresOrdenados AS
                (
                    SELECT [VehiculoPlaca], [Id],
                           ROW_NUMBER() OVER(PARTITION BY [VehiculoPlaca] ORDER BY [Id]) AS [NuevoOrden]
                    FROM [dbo].[VehiculoColores]
                )
                DELETE vc
                FROM [dbo].[VehiculoColores] vc
                INNER JOIN ColoresOrdenados co ON co.[VehiculoPlaca] = vc.[VehiculoPlaca] AND co.[Id] = vc.[Id]
                WHERE co.[NuevoOrden] > 3;
                """);

            migrationBuilder.Sql("""
                IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE [name] = N'CK_VehiculoColores_Orden')
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores]
                    ADD CONSTRAINT [CK_VehiculoColores_Orden] CHECK ([Orden] BETWEEN 1 AND 3);
                END;
                """);

            migrationBuilder.Sql("""
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_VehiculoColores_VehiculoPlaca_Orden' AND [object_id] = OBJECT_ID(N'[dbo].[VehiculoColores]'))
                BEGIN
                    CREATE UNIQUE INDEX [IX_VehiculoColores_VehiculoPlaca_Orden]
                    ON [dbo].[VehiculoColores]([VehiculoPlaca], [Orden]);
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_VehiculoColores_VehiculoPlaca_Orden' AND [object_id] = OBJECT_ID(N'[dbo].[VehiculoColores]'))
                BEGIN
                    DROP INDEX [IX_VehiculoColores_VehiculoPlaca_Orden] ON [dbo].[VehiculoColores];
                END;

                IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE [name] = N'CK_VehiculoColores_Orden')
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores] DROP CONSTRAINT [CK_VehiculoColores_Orden];
                END;

                IF COL_LENGTH(N'dbo.VehiculoColores', N'Orden') IS NOT NULL
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores] DROP COLUMN [Orden];
                END;

                IF COL_LENGTH(N'dbo.VehiculoColores', N'ColorVehiculoId') IS NOT NULL
                BEGIN
                    ALTER TABLE [dbo].[VehiculoColores] DROP COLUMN [ColorVehiculoId];
                END;

                IF OBJECT_ID(N'[dbo].[Colores]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [dbo].[Colores];
                END;
                """);
        }
    }
}
