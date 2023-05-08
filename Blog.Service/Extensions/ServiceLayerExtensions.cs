﻿using Blog.Service.FluentValidations;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Blog.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IArticleService, ArticleService> ();
            services.AddScoped<ICategorySevice, CategorySevice>();
            services.AddAutoMapper(assembly);

            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation=true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });
       

      
            return services;
        }
    }
}
