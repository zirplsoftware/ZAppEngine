
namespace Zirpl.AppEngine.Core.Model
{
    public interface IKeyedEntity<TId>
    {
         TId Id { get; }
    }
}
