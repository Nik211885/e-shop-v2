namespace Core.Exceptions
{
    public class NotFoundException(string nameEntity) : Exception($"Không tìm thấy {nameEntity}");
}