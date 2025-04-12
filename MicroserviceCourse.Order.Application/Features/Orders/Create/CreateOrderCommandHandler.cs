using System.Net;
using MediatR;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Domain.Entities;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Services;

namespace MicroserviceCourse.Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(
    IGenericRepository<Guid, Domain.Entities.Order> orderRepository,
    IGenericRepository<int, Address> addressRepository,
    IIdentityService identityService)
    : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return Task.FromResult(ServiceResult.Error("Order items not found", "Orders must have at least one item",
                HttpStatusCode.BadRequest));


        //TODO: transaction başlatılacak
        var newAddress = new Address()
        {
            Province = request.Address.Province,
            Street = request.Address.Street,
            District = request.Address.District,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };
        addressRepository.Add(newAddress);

        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate,
            newAddress.Id);

        foreach (var orderItem in request.Items)
        {
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
        }

        orderRepository.Add(order);

        //payment işlemleri yapılacak

        var paymentID = Guid.Empty;
        order.SetPaidStatus(paymentID);

        orderRepository.Update(order);

        return Task.FromResult(ServiceResult.SuccessAsNoContent());
    }
}