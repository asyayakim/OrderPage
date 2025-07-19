using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain;

public class EmbeddingEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string TextContent { get; set; } = null!;

    [Required]
    public float[] Embedding { get; set; } = null!; 

    public string? Metadata { get; set; } 
}
