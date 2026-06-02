using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValidationException = FluentValidation.ValidationException;

namespace Core.CrossCuttingConcerns.Validation
{
    public static  class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity); // Doğrulama işlemi için bir ValidationContext oluşturuyoruz ve doğrulanacak nesneyi içine koyuyoruz 

            var result = validator.Validate(context); // Validator'ı kullanarak doğrulama işlemini gerçekleştiriyoruz ve sonucu result değişkenine atıyoruz
            if (!result.IsValid) // Eğer doğrulama sonucu geçerli değilse, yani doğrulama hataları varsa
            {
                throw new ValidationException(result.Errors); // Doğrulama hatalarını içeren bir ValidationException fırlatıyoruz. Bu exception, doğrulama hatalarını
                                                              // içeren bir liste içerir ve bu hataları yakalayarak kullanıcıya göstermek için kullanılabilir.
            }
        }
    }

}
