using ValidationPOC.DTOs;

namespace ValidationPOC.Handlers;

public interface IAddBookHandler
{
    Task HandleRequestAsync(AddBookRequestDto addBookRequestDto);
}