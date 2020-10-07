using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Reflection;

namespace ReflectionTask
{
    public class ClassFactory
    {
        private readonly MemoryCache typeCache;
        private readonly MemoryCache assemblyCache;
        private readonly string assemblyPath;

        public ClassFactory(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new ArgumentNullException("Assembly's path is null");

            this.assemblyPath = assemblyPath;
            typeCache = new MemoryCache(new MemoryCacheOptions());
            assemblyCache = new MemoryCache(new MemoryCacheOptions());
        }

        public object Create(string className, params object[] constructorArgs)
        {
            if (string.IsNullOrEmpty(className))
                throw new ArgumentNullException("Class name is null");

            if (!assemblyCache.TryGetValue(assemblyPath, out Assembly assembly))
            {
                assembly = Assembly.LoadFrom(assemblyPath);
                assemblyCache.Set(assemblyPath, assembly);
            }

            if (!typeCache.TryGetValue(className, out Type type))
            {
                try
                {
                    type = assembly.DefinedTypes
                        .Where(x => x.Name == className)
                        .FirstOrDefault()
                        .AsType();
                }
                catch (NullReferenceException)
                {
                    throw new Exception($"Class with name {className} could not be found in assembly {assembly.ManifestModule.Name}");
                }

                typeCache.Set(className, type);
            }

            if(!IsInputArgumentsMatchWithClassContstructor(type, constructorArgs))
                throw new MissingMethodException($"Constructor of type {className} with arguments passed cannot be found");

            return Activator.CreateInstance(type, constructorArgs);
        }

        private bool IsInputArgumentsMatchWithClassContstructor(Type type, params object[] constructorArgs)
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
