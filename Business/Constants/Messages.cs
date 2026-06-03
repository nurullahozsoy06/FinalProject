using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "ürün eklendi";
        public static string ProductNameInvalid = "ürün ismi geçersiz";
        public static string MaintenanceTime = "sistem bakımda";
        public static string ProductsListed = "ürünler listelendi";
        public static string ProductCountOfCategoryError = "bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "kategori limiti aşıldığı için yeni ürün eklenemiyor";
    }
}
