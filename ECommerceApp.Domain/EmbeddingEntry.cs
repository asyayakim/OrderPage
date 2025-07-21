using System.ComponentModel.DataAnnotations;
using Pgvector;

namespace ECommerceApp.Domain;

public class EmbeddingEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string TextContent { get; set; } 

    [Required]
    public Vector Embedding { get; set; } 

    public string? Metadata { get; set; } 
}
