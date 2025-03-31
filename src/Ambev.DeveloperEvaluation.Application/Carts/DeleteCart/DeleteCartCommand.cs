using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public record DeleteCartCommand(Guid Id) : IRequest<DeleteCartResult>;