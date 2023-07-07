using SprEmployeeReimbursement.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprEmployeeReimbursement.DataAccess.Models;

public class SprEmployee
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; }

    // Navigation property to represent the relationship with reimbursements
    public ICollection<ReimbursementModel> Reimbursements { get; set; }
}
