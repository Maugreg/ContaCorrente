﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }
            }

            return services;
        }
    }
}
