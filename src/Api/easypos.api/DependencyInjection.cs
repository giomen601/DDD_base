﻿using easypos.api.Middleware;
using easypos.application;
using FluentValidation;

namespace easypos.api;
public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddTransient<GlobalExceptionHandlingMiddleware>();
    return services;
  }
}