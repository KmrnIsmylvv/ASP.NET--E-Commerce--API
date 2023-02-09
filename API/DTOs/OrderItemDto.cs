using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;

namespace API.DTOs;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}