using System.ComponentModel.DataAnnotations.Schema;

public abstract class Entity<T>
{
    public T? Id { get; set; }

    [NotMapped]
    public ValidationResult ValidationResult { get; set; }
}