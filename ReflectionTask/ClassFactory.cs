using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Reflection;

namespace ReflectionTask
{
    public class ClassFactory
    {
        private readonly IMemoryCache typeCache;
        private readonly Assembly assembly;

        public ClassFactory(
            string assemblyPath,
            IMemoryCache typeCache,
            IMemoryCache assemblyCache)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new ArgumentNullException("Assembly's path is null");

            this.typeCache = typeCache;

            if (!assemblyCache.TryGetValue(assemblyPath, out assembly))
            {
                assembly = Assembly.LoadFrom(assemblyPath);
                assemblyCache.Set(assemblyPath, assembly);
            }
        }

        public object Create(string className, params object[] constructorArgs)
        {
            if (string.IsNullOrEmpty(className))
                throw new ArgumentNullException("Class name is null");

            TypeFromCache typeFromCache = new TypeFromCache(typeCache);
            var type = new GetTypeFromAssembly(typeFromCache, assembly).GetTypeByName(className);

            if(!InputArgumentsMatchCtor(type, constructorArgs))
                throw new MissingMethodException($"Constructor of type {className} with arguments passed cannot be found");

            return Activator.CreateInstance(type, constructorArgs);
        }

        private bool InputArgumentsMatchCtor(Type type, params object[] constructorArgs)
        {
            var constructors = type.GetConstructors();

            foreach(var constructor in constructors)
            {
                var parametersTypes = constructor.GetParameters().Select(x => x.ParameterType);
                var constructorArgsTypes = constructorArgs.Select(x => x.GetType());
                if (Enumerable.SequenceEqual(parametersTypes, constructorArgsTypes))
                    return true;
            }

            return false;
        }
    }
}
