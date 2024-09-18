namespace ValidationPOC.Exceptions;

public class ConcurrentUpdateException(string message) : Exception(message);