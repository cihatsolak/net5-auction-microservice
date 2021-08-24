﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Mapper;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Configure Mapper
            var config = new MapperConfiguration(configure =>
            {
                configure.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                configure.AddProfile<OrderMappingProfile>();
            });
            var mapper = config.CreateMapper();
            #endregion
        }
    }
}
