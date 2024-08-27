namespace PF2SrdApi.Models;

public interface IEntity
{
    static abstract string TableName { get; }
}
