namespace ReflectionTest
{
    public class ClassWithTwoConstructor
    {
        public int Number { get; set; }

        public ClassWithTwoConstructor() { }

        public ClassWithTwoConstructor(int number)
        {
            Number = number;
        }
    }
}