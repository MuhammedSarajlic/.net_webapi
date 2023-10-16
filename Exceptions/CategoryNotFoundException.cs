namespace Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException() : base("Category not found")
    {
    }
}