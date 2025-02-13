namespace Application.Interface
{
    public interface IFactoryMethod<out TInstance>
    {
        TInstance Create(string key);
    }
}