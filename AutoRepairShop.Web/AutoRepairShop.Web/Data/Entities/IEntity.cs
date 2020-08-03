using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public interface IEntity
    {


        int Id { get; set; }



        DateTime? CreationDate { get; set; }



        DateTime? UpdateDate { get; set; }



        DateTime? DeleteDate { get; set; }



        bool IsActive { get; set; }

    }
}
