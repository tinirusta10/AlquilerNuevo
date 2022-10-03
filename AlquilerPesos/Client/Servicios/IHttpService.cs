namespace AlquilerPesos.Client.Servicios
{
    public interface IHttpService
    {
        Task<Httprespuesta<T>> Get<T>(string url);
    }
}
