namespace ToyWebService.Application.DAL.Models.Item;

public class ItemV1
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public int[] TagIds { get; set; }
}