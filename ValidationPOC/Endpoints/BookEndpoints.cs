using Microsoft.AspNetCore.Mvc;
using ValidationPOC.DTOs;
using ValidationPOC.Enums;
using ValidationPOC.Handlers;
using ValidationPOC.Persistence;
using ValidationPOC.Persistence.Models;

namespace ValidationPOC.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapPost("/add-book-request", AddBookRequest);
        app.MapPost("/edit-book-request", EditBookRequest);
        app.MapPost("/add-book-decline-reason-test", AddBookDeclineReason);
    }


    private static async Task AddBookDeclineReason([FromServices] ApplicationDbContext dbContext)
    {
        List<DeclineReason> declineReasons = [DeclineReason.Reason1, DeclineReason.Reason2];
        var bookRequest = new BookRequest()
        {
            ApprovalStatus = ApprovalStatus.Pending,
            AuthorId = $"New Author {Guid.NewGuid()}",
            DeclineReasons = declineReasons.Select(x => new BookRequestDeclineReason(Guid.NewGuid(), x)).ToList(),
            MainId = Guid.NewGuid(),
            RequestType = RequestType.Add,
            Title = "Test",
            NewTitle = "Test"
        };

        await dbContext.AddAsync(bookRequest);
        await dbContext.SaveChangesAsync();
    }


    private static async Task AddBookRequest(HttpContext context,
        IAddBookHandler _addBookHandler,
        [FromBody]
        AddBookRequestDto addBookRequestDto)
    {
        try
        {
            await _addBookHandler.HandleRequestAsync(addBookRequestDto);
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            await _addBookHandler.HandleRequestAsync(addBookRequestDto);
        }
    }

    private static async Task EditBookRequest(HttpContext context,
        IEditBookHandler _editBookHandler,
        [FromBody]
        EditBookRequestDto editBookRequestDto)
    {
        try
        {
            await _editBookHandler.HandleRequestAsync(editBookRequestDto);
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            await _editBookHandler.HandleRequestAsync(editBookRequestDto);
        }
    }
}