using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace ReflectionTask
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassFactory factory = new ClassFactory(
                "ReflectionTestClasses.dll",
                new MemoryCache(new MemoryCacheOptions()),
                new MemoryCache(new MemoryCacheOptions()));

            List<object> classesCreated = new List<object>
            {
                factory.Create("ClassWithThreeConstructor"),
                factory.Create("ClassWithTwoConstructor"),
                factory.Create("OuterClass"),
                factory.Create("InnerClass"),
            };

            ClassInfoShower shower = new ClassInfoShower(classesCreated);
            shower.Show();
        }
    }
}
