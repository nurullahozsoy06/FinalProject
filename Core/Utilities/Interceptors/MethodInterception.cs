using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    // MethodInterception, methodların önünde, sonunda, hata durumunda veya başarılı olduğunda çalışacak kodları yazmak için kullanılan bir base sınıftır.
    // Bu sınıfı kullanarak, istediğimiz methodlara aspect'ler ekleyebiliriz.

    //Eğer bu MethodInterception sınıfı olmasaydı, yazacağın her bir aspect (Log, Cache, Validation vs.) için Castle DynamicProxy'nin Intercept metodunu tek tek ezip
    //içlerine satırlarca try-catch-finally bloğu yazmak zorunda kalacaktın.

    //Bu sınıf, tüm aspect'lerin ortaklaşa kullanacağı evrensel bir şablon (çerçeve) sunar.
    //Diğer aspect'ler sadece kendi ilgilendikleri evreyi (örneğin sadece OnBefore'u) ezerek tertemiz bir şekilde yazılırlar.
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    { //invocation =business kodlarının çalıştığı methodu temsil eder. Yani, aspect'in uygulanacağı methodun bilgilerini ve o methodun çalışmasını kontrol etmek için kullanılan bir nesnedir.
        protected virtual void OnBefore(IInvocation invocation) { } //Bu method, aspect'in uygulanacağı methodun çalışmasından önce çalışacak kodları yazmak için kullanılır. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
        protected virtual void OnAfter(IInvocation invocation) { } //Bu method, aspect'in uygulanacağı methodun çalışmasından sonra çalışacak kodları yazmak için kullanılır. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
        protected virtual void OnException(IInvocation invocation, System.Exception e) { } //Bu method, aspect'in uygulanacağı methodun çalışması sırasında bir hata oluşursa çalışacak kodları yazmak için kullanılır. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
        protected virtual void OnSuccess(IInvocation invocation) { }// Bu method, aspect'in uygulanacağı methodun çalışması başarılı bir şekilde tamamlandığında çalışacak kodları yazmak için kullanılır. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed(); // Bu satır, aspect'in uygulanacağı methodun çalışmasını sağlar. Yani, bu satırın çalışmasıyla birlikte, aspect'in uygulanacağı methodun kodları çalışmaya başlar.
            }
            catch (Exception e)
            {
                isSuccess = false; // Eğer aspect'in uygulanacağı methodun çalışması sırasında bir hata oluşursa, isSuccess değişkeni false olarak atanır.
                OnException(invocation, e); // Eğer aspect'in uygulanacağı methodun çalışması sırasında bir hata oluşursa, OnException methodu çağrılır ve hatanın detayları burada işlenebilir. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
                throw; // Hata fırlatılır, böylece hata üst katmanlara iletilir ve uygun şekilde ele alınabilir.
            }
            finally
            {
                if (isSuccess) 
                {
                    OnSuccess(invocation); // Eğer aspect'in uygulanacağı methodun çalışması başarılı bir şekilde tamamlanırsa, OnSuccess methodu çağrılır ve başarılı çalışmanın detayları burada işlenebilir. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
                }
            }
            OnAfter(invocation); // OnAfter methodu, aspect'in uygulanacağı methodun çalışmasından sonra her zaman çalışır. Yani, başarılı veya başarısız olsun, bu method her zaman çalışır. Örneğin, bir log aspect'i için burada loglama kodları yazılabilir.
        }
    }
}
