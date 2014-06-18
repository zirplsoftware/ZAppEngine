namespace Zirpl.AppEngine.DataService
{
    public interface ISequenceValueProvider
    {
        long GetNextValue();
        long GetCurrentValue();
    }
}
