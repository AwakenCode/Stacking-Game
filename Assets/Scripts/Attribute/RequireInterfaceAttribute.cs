using System;
using UnityEngine;

/// <summary>
/// Attribute that require implementation of the provided interface.
/// </summary>
public class RequireInterfaceAttribute : PropertyAttribute
{
    public Type RequiredType { get; private set; }

    public RequireInterfaceAttribute(Type type)
    {
        RequiredType = type;
    }
}