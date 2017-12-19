using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    /// <summary>
    /// Used to decorate injectable types
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreInjectionAttribute : Attribute
    {

    }
}
