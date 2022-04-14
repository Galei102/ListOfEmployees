using System.ComponentModel.DataAnnotations;

namespace ListOfEmployees.ViewModels
{
    public class OrganizationVM
    {
        public int NameId { get; set; }

        [StringLength(50)]
        [Display(Name = "Название организации")]
        [Required(ErrorMessage = "Введите название организации!")]
        public string Name { get; set; }
    }
}
