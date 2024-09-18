using ValidationPOC.DTOs;

namespace ValidationPOC.Handlers;

public interface IEditBookHandler
{
    Task HandleRequestAsync(EditBookRequestDto editBookRequestDto);
}