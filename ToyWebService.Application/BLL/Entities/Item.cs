namespace ToyWebService.Application.BLL.Entities;

public class Item
{
    public long Id { get; set; }

    public string Name { get; set; }
    
    public int[] TagIds { get; set; }
}