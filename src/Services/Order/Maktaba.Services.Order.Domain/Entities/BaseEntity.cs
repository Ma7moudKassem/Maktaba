﻿namespace Maktaba.Services.Order.Domain;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
