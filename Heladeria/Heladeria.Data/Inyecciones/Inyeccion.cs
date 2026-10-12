using Heladeria.Data.Contexto;
using Heladeria.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria.Data.Inyecciones
{
    public static class Inyeccion
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HeladoContexto>(o =>
                o.UseSqlServer(connectionString).LogTo(Console.WriteLine));
            services.AddScoped<IHeladoRepositorio, HeladoRepo>();
            return services;
        }
    }
}
