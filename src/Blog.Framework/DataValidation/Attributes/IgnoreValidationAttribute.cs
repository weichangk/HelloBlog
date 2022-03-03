namespace Blog.Framework.DataValidation.Attributes
{
    /// <summary>
    /// 忽略数据验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class IgnoreValidationAttribute : Attribute
    {

    }
}
