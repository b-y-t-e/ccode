using System;
using System.Linq;
using ccode.app.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ccode.app;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        var assemblies = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .ToList();

        collection.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ViewModelBase)))
                .AsSelf()
                .WithTransientLifetime());

        return collection;
    }
}
