using ValidationPOC.Domain.Models;
using ValidationPOC.DTOs;
using ValidationPOC.Persistence.Repositories;
using ValidationPOC.Persistence.UnitOfWork;

namespace ValidationPOC.Handlers;

public class AddBookHandler(
    IBookRepository _bookRepository,
    [FromKeyedServices("One")]
    IBookRequestRepository _bookRequestRepository,
    IBookCountRepository _bookCountRepository,
    IUnitOfWork _unitOfWork) : IAddBookHandler
{
    public async Task HandleRequestAsync(AddBookRequestDto addBookRequestDto)
    {
        var bookCount = await _bookCountRepository.GetBookCountForAuthor(addBookRequestDto.AuthorId);
        Console.WriteLine(bookCount);
        var count = bookCount?.Count ?? 0;

        if (count == 3)
            return;

        if (count == 0)
        {
            await _bookCountRepository.AddAsync(addBookRequestDto.AuthorId);
            //Add request we know none exist
            await _bookRequestRepository.AddAddBookRequestAsync(new AddBookRequest(addBookRequestDto.AuthorId,
                addBookRequestDto.Title));

            await _unitOfWork.CompleteAsync();
            return;
        }

        if (await DoesExist(addBookRequestDto.AuthorId, addBookRequestDto.Title)) return;

        await _bookRequestRepository.AddAddBookRequestAsync(new AddBookRequest(addBookRequestDto.AuthorId,
            addBookRequestDto.Title));

        await _bookCountRepository.IncrementCountAsync(bookCount!.Id);

        await _unitOfWork.CompleteAsync();
    }


    private async Task<int> GetTotalRequestsAndBooks(string authorId)
    {
        var addRequestCount =
            await _bookRequestRepository.GetPendingAddRequestCountAsync(authorId);

        var books = await _bookRepository.GetCountAsync(authorId);

        return addRequestCount + books;
    }

    private async Task<bool> DoesExist(string authorId, string title)
    {
        var requestExists = await _bookRequestRepository.PendingExistsAsync(authorId, title);
        var bookExists = await _bookRepository.ExistsAsync(authorId, title);
        return requestExists || bookExists;
    }
}