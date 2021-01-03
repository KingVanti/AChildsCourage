using System;

namespace AChildsCourage
{

    [AttributeUsage(AttributeTargets.Field)]
    public class FindInSceneAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class FindServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Event)]
    public class PubAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class SubAttribute : Attribute
    {

        public string EventName { get; }


        public SubAttribute(string eventName) => EventName = eventName;

    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FindComponentAttribute : Attribute
    {

        public ComponentFindMode FindMode { get; }


        public FindComponentAttribute(ComponentFindMode findMode = ComponentFindMode.OnSelf) =>
            FindMode = findMode;

    }

}