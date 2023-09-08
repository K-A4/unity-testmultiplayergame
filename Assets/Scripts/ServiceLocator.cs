using System.Collections.Generic;
using UnityEngine;
using System;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance 
    {
        get => instance;
    }

    private static ServiceLocator instance;
    private ServiceRegistry serviceRegistry;

    private void Awake()
    {
        instance = this;
        serviceRegistry = new();
    }

    public void SetService<T>(object instance)
    {
        serviceRegistry.AddService(typeof(T), instance);
    }

    public T GetService<T>()
    {
        return (T)serviceRegistry.GetService(typeof(T));
    }
}

public class ServiceRegistry
{
    private Dictionary<Type, object> services = new();

    public void AddService(Type serviceType, object Instance)
    {
        if (services.ContainsKey(serviceType))
        {
            return;
        }
        
        services.Add(serviceType, Instance);
    }

    public object GetService(Type type)
    {
        if (services.ContainsKey(type))
        {
            return services[type];
        }

        return null;
    }
}
