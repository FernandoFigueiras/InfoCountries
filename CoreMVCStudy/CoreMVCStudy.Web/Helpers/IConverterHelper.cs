using CoreMVCStudy.Web.Data.Entities;
using CoreMVCStudy.Web.Models;

namespace CoreMVCStudy.Web.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string path, bool isNew);


        ProductViewModel ToProductViewModel(Product model);
    }
}
