namespace ValidationPOC.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task CompleteAsync();
}