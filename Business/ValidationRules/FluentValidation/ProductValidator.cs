using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product> // FluentValidation kütüphanesinden AbstractValidator sınıfını miras alarak ProductValidator sınıfını oluşturuyoruz
    {
        public ProductValidator() // Constructor içinde kurallarımızı tanımlıyoruz
        {
            RuleFor(p => p.ProductName).NotEmpty();  // ProductName alanının boş olmaması gerektiğini belirtiyoruz
            RuleFor(p => p.ProductName).MinimumLength(2); 
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p=>p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
        }

        private bool StartWithA(string arg)
        {
           return arg.StartsWith("A");
        }
    }
}
