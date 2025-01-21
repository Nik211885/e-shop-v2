using System.Reflection;

namespace Application.Helper
{
    public static class MappingHelper
    {
        /// <summary>
    ///     Simple way activator and mapping data
    /// </summary>
    /// <param name="mapFrom">data source</param>
    /// <typeparam name="TFrom">type data source</typeparam>
    /// <typeparam name="TTo">type data leaks ;)</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception">
    ///     Return exception if create instance for typeof t to
    /// </exception>
    public static TTo? Mapping<TFrom, TTo>(TFrom mapFrom) where TTo : class
        where TFrom : class
    {
        var typeTo = typeof(TTo);
        var propertiesTo = GetPropertyInfos(typeTo);
        var instance = Activator.CreateInstance(typeTo)
            ?? throw new Exception($"Can not create instance {typeTo.Name}");
        var typeFrom = typeof(TFrom);
        var propertiesFrom = GetPropertyInfos(typeFrom);
        foreach (var p in propertiesTo)
        {
            if (!p.CanWrite) continue;
            var propertyFrom = propertiesFrom.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType);
            if(propertyFrom is null) continue;
            p.SetValue(instance, propertyFrom.GetValue(mapFrom));
        }
        return instance as TTo;
    }
    /// <summary>
    ///     Simple way activator and mapping list data
    /// </summary>
    /// <param name="mapFrom">data source</param>
    /// <typeparam name="TFrom">type data source</typeparam>
    /// <typeparam name="TTo">type data leaks ;)</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception">
    ///     Return exception if create instance for typeof t to
    /// </exception>
    public static List<TTo> Mapping<TFrom, TTo>(List<TFrom> mapFrom) where TFrom : class
        where TTo : class
    {
        List<TTo> result = [];
        var typeTo = typeof(TTo);
        var typeFrom = typeof(TFrom);
        var propertiesTo = GetPropertyInfos(typeTo);
        var propertiesFrom = GetPropertyInfos(typeFrom);
        var countItemsFrom = mapFrom.Count;
        for (var i = 0; i < countItemsFrom; i++)
        {
            if (Activator.CreateInstance(typeTo) is not TTo instance)
            {
                throw new Exception("Can not create instance");
            }

            result.Add(instance);
        }
        foreach (var p in propertiesTo)
        {
            if(!p.CanWrite) continue;
            var propertyFrom = propertiesFrom.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType);
            if(propertyFrom is null) continue;
            for (var i = 0; i < countItemsFrom; i++)
            {
                p.SetValue(result.ElementAt(i), propertyFrom.GetValue(mapFrom.ElementAt(i)));
            }
        }
        return result;
    }

    public static PropertyInfo[] GetPropertyInfos(Type type)
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
    }
}