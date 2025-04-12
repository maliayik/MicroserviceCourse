using System.Net;
using MediatR;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Application.Contracts.UnitOfWorks;
using MicroserviceCourse.Order.Domain.Entities;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Services;

namespace MicroserviceCourse.Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IGenericRepository<int, Address> addressRepository,
    IIdentityService identityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return (ServiceResult.Error("Order items not found", "Orders must have at least one item",
                HttpStatusCode.BadRequest));

        unitOfWork.BeginTransactionAsync();
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

        order.Address = newAddress;
        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        //TODO:payment işlemleri yapılacak

        var paymentID = Guid.Empty;
        order.SetPaidStatus(paymentID);

        orderRepository.Update(order);

        await unitOfWork.CommitAsync(cancellationToken);

        return (ServiceResult.SuccessAsNoContent());
    }
}