using System;
using System.Linq;
using System.Reflection;

namespace ReflectionTask
{
    public class GetTypeFromAssembly : GetTypeByNameDecorator
    {
        private readonly Assembly assembly;

        public GetTypeFromAssembly(IGetTypeByParameter type, Assembly assembly) : base(type)
        {
            this.assembly = assembly;              
        }

        public override Type GetTypeByName(string name)
        {
            if(base.GetTypeByName(name) == null)
            {
                return assembly.DefinedTypes
                    .Where(x => x.Name == name)
                    .FirstOrDefault()
                    .AsType();
            }
            
            return base.GetTypeByName(name);
        }
    }
}
