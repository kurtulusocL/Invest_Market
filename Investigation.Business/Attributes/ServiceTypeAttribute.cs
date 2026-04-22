
namespace Investigation.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceTypeAttribute : Attribute
    {
        public Type ServiceType { get; }

        public ServiceTypeAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
