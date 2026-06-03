using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception // 
    {
        private Type _validatorType; // Hangi validator'un kullanılacağını belirtmek için bir Type değişkeni tanımlıyoruz.
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir doğrulama sınıfı değil"); // "this is not a validation class"
            }

            _validatorType = validatorType; // Reflection
        }
        protected override void OnBefore(IInvocation invocation) 
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection ile doğrulama sınıfının bir örneğini oluşturuyoruz.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // BaseType ile validator'un hangi sınıftan türetildiğini buluyoruz, GetGenericArguments ile de o sınıfın tipini alıyoruz.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // invocation.Arguments ile metodun parametrelerini alıyoruz, Where ile de bu parametreler arasında doğrulama sınıfının tipine eşit olanları filtreliyoruz.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); // ValidationTool.Validate ile doğrulama işlemini gerçekleştiriyoruz. Validator ve entity'yi parametre olarak veriyoruz.
            }
        }
    }
}
