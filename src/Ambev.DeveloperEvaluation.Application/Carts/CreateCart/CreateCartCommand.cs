using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public record CreateCartCommand(): IRequest<CreateCartResult>
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public List<CreateCartItemCommand> Items { get; init; } = [];
}

public record CreateCartItemCommand(Guid ProductId, string ProductTitle, int Quantity);