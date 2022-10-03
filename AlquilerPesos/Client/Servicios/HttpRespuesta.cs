namespace AlquilerPesos.Client.Servicios
{
    public class Httprespuesta<T> //T = cualquier clase
    {
        public T Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public Httprespuesta(T respuesta,
                             bool error,
                             HttpResponseMessage httpResponseMessage)

        {
            Respuesta = respuesta;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }


    }
}
