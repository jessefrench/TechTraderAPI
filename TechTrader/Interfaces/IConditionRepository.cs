﻿using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IConditionRepository
    {
        Task<List<Condition>> GetConditionsAsync();
    }
}