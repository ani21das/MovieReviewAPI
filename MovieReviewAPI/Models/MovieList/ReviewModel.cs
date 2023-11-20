using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NSwag.Annotations;

public class ReviewModel
{
    [OpenApiIgnore]
    public int? Id { get; set; }
    [OpenApiIgnore]
    public string UserName { get; set; }
    [OpenApiIgnore]
    public int? MovieId { get; set; }
    [Required]
    public string Comment { get; set; }
    [Required]
    public bool Recommended { get; set; }
    [OpenApiIgnore]
    public string MovieName { get; set; }
    [ReadOnly(true)]
    public DateTime? CreatedAt { get; set; }
}


