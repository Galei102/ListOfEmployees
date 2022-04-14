using System.ComponentModel.DataAnnotations;

namespace ListOfEmployees.ViewModels
{
    public class SubdivisionVM
    {
        public int NameId { get; set; }

        [StringLength(50)]
        [Display(Name = "Название подразделении")]
        [Required(ErrorMessage = "Введите название подразделении!")]
        public string Name { get; set; }
        public int OrganizationId { get; set; }
    }
}
