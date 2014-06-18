
namespace Zirpl.AppEngine.Model
{
    public interface IKeyedEntity<TId>
    {
         TId Id { get; }
    }
}
