

using System;
using System.ComponentModel.DataAnnotations;

namespace V1.Dtos
{

    public record ItemDto(Guid id, string Name, string Description, decimal Price, DateTimeOffset CreateDate);

    public record CreateItemDto([Required] string Name, string Description, decimal Price, DateTimeOffset CreateDate);

    public record UpdateItemDto([Required] string Name, string Description, decimal Price, DateTimeOffset CreateDate);
    
}