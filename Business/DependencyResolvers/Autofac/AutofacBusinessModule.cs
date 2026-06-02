using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder) // Autofac'ın modül yapısını kullanarak bağımlılıkları kaydettiğimiz yer
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); // Birisi senden IProductService isterse ona ProductManager ver demek
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance(); // Birisi senden IProductDal isterse ona EfProductDal ver demek
          
            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // Şu an çalışan assembly'i alır
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() // Şu an çalışan assembly'deki tüm tipleri alır ve onların uyguladığı arayüzlere göre kaydeder
             
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() // AspectInterceptorSelector'ı kullanarak hangi interceptor'ların uygulanacağını belirler
                {
                    Selector = new AspectInterceptorSelector()// AspectInterceptorSelector'ı kullanarak hangi interceptor'ların uygulanacağını belirler
                }).SingleInstance(); // Tüm tipler için tek bir instance oluşturur
        }
        //Kayıt ettiğin bu sınıfların (örneğin ProductManager'ın) metotlarını doğrudan çalıştırma.
        //Onların etrafına bir koruyucu kalkan (Proxy) ör ve metotların arayüz interceptor'larını aktif et."
    }
}
