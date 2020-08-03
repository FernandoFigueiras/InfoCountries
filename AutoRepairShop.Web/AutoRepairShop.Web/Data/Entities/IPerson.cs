namespace AutoRepairShop.Web.Data.Entities
{
    public interface IPerson : IEntity
    {


        string FirstName { get; set; }



        string LastName { get; set; }



        string DocumentNumber { get; set; }



        string Address { get; set; }



        string ZipCode { get; set; }



        string Place { get; set; }



        string PhoneNumber { get; set; }



        string Email { get; set; }
    }
}
