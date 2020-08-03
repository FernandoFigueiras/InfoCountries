using CoreMVCStudy.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCStudy.Web.Models
{
    public class ProductViewModel : Product
    {

        [Display(Name = "Image")]
        public IFormFile imageFile { get; set; }//Isto e o que permite ir buscar as imagens
        //mexer na view do create
    }
}
