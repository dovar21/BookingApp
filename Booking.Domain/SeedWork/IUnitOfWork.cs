﻿namespace Booking.Domain.SeedWork;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
}