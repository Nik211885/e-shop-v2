namespace Core.Exceptions
{
    public class BadRequestException(string valueError) : Exception(valueError);
}