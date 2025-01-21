namespace Application.Models
{
    public record ErrorResponse(string Title, int StatusCode, string Detail);
}