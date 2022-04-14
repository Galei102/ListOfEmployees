using System.ComponentModel.DataAnnotations;

namespace ListOfEmployees.ViewModels
{
    public class PositionVM
    {
        public int NameId { get; set; }

        [StringLength(50)]
        [Display(Name = "Название должности")]
        [Required(ErrorMessage = "Введите название должности!")]
        public string Name { get; set; }
        public int SubdivisionId { get; set; }
    }
}
