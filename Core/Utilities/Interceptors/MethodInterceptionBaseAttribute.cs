using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    // Aspectler için temel attribute sınıfı
    // AttributeUsage ile bu attribute'un hangi elemanlara uygulanabileceğini, kaç kez uygulanabileceğini ve miras alınıp alınamayacağını belirtiyoruz.
    // Bu sınıf, Castle DynamicProxy kütüphanesinin IInterceptor arayüzünü implement eder. Bu sayede, bu attribute'u kullanan sınıflar veya metodlar,
    // belirli bir öncelik sırasına göre kesilebilir (intercepted).
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, Castle.DynamicProxy.IInterceptor
    {
        public int Priority { get; set; } // Aspectlerin çalıştırılma sırasını belirlemek için kullanılabilir.

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
