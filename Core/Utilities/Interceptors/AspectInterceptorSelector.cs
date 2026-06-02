using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector 
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method,IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute> //Ne yapar? Metodun içinde bulunduğu sınıfın tepesine yazılmış olan aspect'leri arar.
                (true).ToList(); //true parametresi, sınıfın kendisi ve onun üst sınıflarındaki (varsa) tüm attribute'leri arar. Eğer false olsaydı, sadece sınıfın kendisindeki attribute'leri arardı. ToList() ile de sonucu bir listeye dönüştürüyoruz.
            var methodAttributes = type.GetMethod(method.Name) //Ne yapar? Metodun kendisine yazılmış olan aspect'leri arar.
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //Ne yapar? Metodun kendisine yazılmış olan aspect'leri arar.
            classAttributes.AddRange(methodAttributes); //Ne yapar? Sınıfın tepesine yazılmış olan aspect'leri ve metodun kendisine yazılmış olan aspect'leri birleştirir.


            return classAttributes.OrderBy(x => x.Priority).ToArray(); //Ne yapar? Birleştirilmiş aspect'leri öncelik sırasına göre sıralar ve bir dizi olarak döndürür. Öncelik sırası, aspect'lerin hangi sırayla çalışacağını belirler. Daha düşük öncelik numarası olan aspect'ler önce çalışır.
        } 
    }
}
