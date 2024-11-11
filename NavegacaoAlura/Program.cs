using Microsoft.Extensions.DependencyInjection;
using NavegacaoAlura.Domain.Models.Entities;
using NavegacaoAlura.Services.Interfaces;
using NavegacaoAlura.Services.Navegador;
using OpenQA.Selenium;
using System;

namespace NavegacaoAlura;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServices();
        NavegadorAlura navegadorAlura = services.GetRequiredService<NavegadorAlura>();
        navegadorAlura.ExecutaNavegacao();
    }

    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<NavegadorAlura>(new NavegadorAlura())
            .BuildServiceProvider();

        return serviceProvider;
    }
}

