using System;

namespace Ninject.Extensions.Unity
{

    [AttributeUsage(AttributeTargets.Class)]
    public class UnityEntityAttribute : Attribute
    {

        public Type InterfaceType { get; }

        public UnityEntityAttribute(Type interfaceType)
        {
            if (interfaceType.IsInterface)
                InterfaceType = interfaceType;
            else
                throw new ArgumentException($"The type {interfaceType.Name} is not an interface!");
        }

    }

}