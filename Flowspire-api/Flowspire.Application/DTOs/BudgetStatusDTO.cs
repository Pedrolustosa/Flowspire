﻿namespace Flowspire.Application.DTOs;

public class BudgetStatusDTO
{
    public string CategoryName { get; set; }
    public decimal BudgetAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal PercentageUsed { get; set; }
}
