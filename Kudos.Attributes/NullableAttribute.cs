namespace Kudos.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class NullableAttribute : Attribute { }