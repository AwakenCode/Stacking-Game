namespace Infrastructure.Factory
{
    public interface IObjectFactory<T>
    {
        T CreateBox();
    }
}