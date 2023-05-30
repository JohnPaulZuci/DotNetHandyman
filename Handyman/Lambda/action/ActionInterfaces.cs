using System;

namespace ActionInterfaceNS
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionContext : Attribute
    {
        public string ActionName { get; set; } = "";
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ActionExecution : Attribute
    {
        public string ActionName { get; set; } = "";
    }

    public interface IActionContext
    {
        public string name {get; }
    }

    public interface IActionExecution
    {
        void execute();

        bool executeIf();
    }
}