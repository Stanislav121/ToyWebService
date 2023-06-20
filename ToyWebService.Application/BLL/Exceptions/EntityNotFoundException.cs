namespace ToyWebService.Application.BLL.Exceptions;

public class EntityNotFoundException : Exception
{
    public long Id { get; }
    
    public EntityNotFoundException(long id)
    {
        Id = id;
    }
}