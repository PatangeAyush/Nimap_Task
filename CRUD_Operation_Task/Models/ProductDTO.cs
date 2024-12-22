using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_Task.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public int CategoryID { get; set; }

    }
}
