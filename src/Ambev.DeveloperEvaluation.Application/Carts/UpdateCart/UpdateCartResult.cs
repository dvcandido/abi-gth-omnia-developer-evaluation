using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public record UpdateCartResult(
    Guid Id,
    Guid UserId,
    string UserName,
    DateTime Date,
    List<UpdateCartItemResult> Items
);

public record UpdateCartItemResult(Guid ProductId, string ProductTitle, int Quantity);