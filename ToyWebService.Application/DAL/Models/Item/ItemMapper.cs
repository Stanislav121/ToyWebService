namespace ToyWebService.Application.DAL.Models.Item;

public static class ItemMapper
{
    public static BLL.Entities.Item Map(this ItemV1 item)
    {
        return new BLL.Entities.Item
        {
            Id = item.Id,
            Name = item.Name
        };
    }
}