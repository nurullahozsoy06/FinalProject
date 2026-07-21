using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // void yerine IResult döndürmek istiyoruz çünkü void sadece işlem yapar ama bize işlem sonucu ve mesajı vermez
    public interface IResult
    {
        bool Success { get; }// sadece okunabilir
        string Message { get; }
    }
}
