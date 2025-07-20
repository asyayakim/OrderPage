using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain;

public class EmbeddingEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string TextContent { get; set; } 

    [Required]
    public float[] Embedding { get; set; } 

    public string? Metadata { get; set; } 
}
