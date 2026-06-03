using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // params ile istediğimiz kadar IResult gönderebiliriz
        {
            foreach (var logic in logics)
            {
                if (!logic.Success) // eğer herhangi bir kural başarısız olursa
                {
                    return logic; // o kuralın hata mesajını döndürürüz
                }
            }
            return null; // eğer tüm kurallar başarılı olursa null döndürürüz
        }
    }
}
