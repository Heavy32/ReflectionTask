namespace ReflectionTest
{
    public class ClassWithThreeConstructor
    {
        public int Number { get; set; }
        public string Text { get; set; }

        public ClassWithThreeConstructor() { }

        public ClassWithThreeConstructor(int number)
        {
            Number = number;
        }

        public ClassWithThreeConstructor(int number, string text)
        {
            Number = number;
            Text = text;
        }
    }
}
