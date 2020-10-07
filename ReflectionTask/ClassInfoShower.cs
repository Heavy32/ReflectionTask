using System;
using System.Collections.Generic;

namespace ReflectionTask
{
    class ClassInfoShower
    {
        public IReadOnlyCollection<object> Classes { get; set; }

        public ClassInfoShower(IReadOnlyCollection<object> classes)
        {
            Classes = classes;
        }

        public void Show()
        {
            foreach (var item in Classes)
            {
                Type type = item.GetType();

                ShowBasicClassInfo(type);
                ShowConstrucrors(type);
                ShowMethods(type);
                ShowProperties(type);

                Console.WriteLine(new string('_', 60));
            }
        }

        private void ShowBasicClassInfo(Type type)
        {
            Console.WriteLine(type.Name);
            Console.WriteLine(type.Assembly.FullName);
            Console.WriteLine($"Is nested {type.IsNested}");
            Console.WriteLine();
        }

        private void ShowConstrucrors(Type type)
        {
            Console.WriteLine("Constructors: ");
            foreach (var constructor in type.GetConstructors())
            {
                Console.WriteLine(constructor.Name);
                foreach (var param in constructor.GetParameters())
                {
                    Console.WriteLine(param.Name);
                    Console.WriteLine(param.ParameterType);
                    Console.WriteLine();
                }
            }
        }

        private void ShowMethods(Type type)
        {
            Console.WriteLine("Methods: ");
            foreach (var method in type.GetMethods())
            {
                Console.WriteLine(method.Name);
                Console.WriteLine($"Is static: {method.IsStatic}");
                Console.WriteLine($"Is public: {method.IsPublic}");

                foreach (var param in method.GetParameters())
                {
                    Console.WriteLine(param.Name);
                    Console.WriteLine(param.ParameterType);
                    Console.WriteLine();
                }
            }
        }

        private void ShowProperties(Type type)
        {
            Console.WriteLine("Properties: ");
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine(property.Name);
                Console.WriteLine($"Can write: {property.CanWrite}");
                Console.WriteLine($"Can read: {property.CanRead}");
                Console.WriteLine($"Type used: {property.PropertyType}");
                Console.WriteLine();
            }
        }
    }
}
