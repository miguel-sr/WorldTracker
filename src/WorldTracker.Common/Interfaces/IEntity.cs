﻿namespace WorldTracker.Common.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
