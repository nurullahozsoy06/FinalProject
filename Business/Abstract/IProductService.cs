using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>>GetAll(); // hem işlem sonucu hem mesajı hem döndüreceği şeyi içerir 


        IDataResult<List<Product>> GetAllByCategoryId(int id);

        IDataResult<List<Product>> GetByUnitPrice(decimal min ,decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product >GetById(int productId);
        IResult Add(Product product); // önceden void' di
        
    }
}
