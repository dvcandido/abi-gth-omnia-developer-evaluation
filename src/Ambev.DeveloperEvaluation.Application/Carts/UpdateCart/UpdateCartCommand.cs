﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public record UpdateCartCommand : IRequest<UpdateCartResult>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public List<UpdateCartItemCommand> Items { get; init; } = [];
}

public record UpdateCartItemCommand(Guid ProductId, string ProductTitle, int Quantity);
