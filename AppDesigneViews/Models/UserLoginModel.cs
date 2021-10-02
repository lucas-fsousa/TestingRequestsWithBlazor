using System.ComponentModel.DataAnnotations;

namespace AppDesigneViews.Models {
  public class UserLoginModel {

    [Required(ErrorMessage = "Necessary Field!")]
    public string UserLogin { get; set; }

    [Required(ErrorMessage = "Necessary Field!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
