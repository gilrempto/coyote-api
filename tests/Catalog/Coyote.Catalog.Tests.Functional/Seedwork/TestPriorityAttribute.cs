using System;

namespace Coyote.Catalog.Tests.Functional.Seedwork;

/// <summary>
/// Taken from https://github.com/xunit/samples.xunit/blob/main/TestOrderExamples/TestCaseOrdering/TestPriorityAttribute.cs
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute : Attribute
{
    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

    public int Priority { get; private set; }
}
