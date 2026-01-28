namespace Blue.Domain.Common;

public abstract class BaseEntity<TId>
{
    //Clase base para todas las Entidades del Dominio.
    public TId Id { get; protected set; } = default!;
    //Protected set evita que capas externas modifiquen el Id.
}