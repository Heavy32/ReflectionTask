using System;

namespace ReflectionTask
{
    public abstract class GetTypeByNameDecorator : IGetTypeByParameter
    {
        private readonly IGetTypeByParameter type;

        public GetTypeByNameDecorator(IGetTypeByParameter type)
        {
            this.type = type;
        }

        public virtual Type GetTypeByName(string name)
        {
            return type.GetTypeByName(name);
        }
    }
}
