using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_Task.Models
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string CategoryName { get; set; }
    }
}
