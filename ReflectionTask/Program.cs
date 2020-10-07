using System.Collections.Generic;

namespace ReflectionTask
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassFactory factory = new ClassFactory("ReflectionTest.dll");

            List<object> classesCreated = new List<object>
            {
                factory.Create("InnerClass"),
                factory.Create("ClassWithOneProperty"),
                factory.Create("ClassWithThreeConstructor"),
                factory.Create("ClassWithTwoConstructor", 2),
                factory.Create("OuterClass"),
                factory.Create("TestClassWithPrivateFields"),
            };

            ClassInfoShower shower = new ClassInfoShower(classesCreated);
            shower.Show();
        }
    }
}
