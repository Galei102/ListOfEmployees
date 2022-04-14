using System.ComponentModel.DataAnnotations;

namespace ListOfEmployees.ViewModels
{
    public class AddEmployeeVM
    {
        public int EmployeeID { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 30 символов")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите имя!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 30 символов")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Введите отчество!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 30 символов")]
        public string FirstMidName { get; set; }

        [Display(Name = "Список организации")]
        [Required(ErrorMessage = "Выберите организацию!")]
        public int OrganizationId { get; set; }

        [Display(Name = "Список подразделений")]
        [Required(ErrorMessage = "Выберите подразделение!")]
        public int SubdivisionId { get; set; }

        [Display(Name = "Список должностей")]
        [Required(ErrorMessage = "Выберите должность!")]
        public int PositionId { get; set; }


        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Введите номер телефона!")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Длина строки должна быть 11 символов")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите адресс электронной почты!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        public string Email { get; set; }
    }
}
