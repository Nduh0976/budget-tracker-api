namespace BudgetTracker.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }

    protected DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class InvalidBudgetAmountException : DomainException
{
    public InvalidBudgetAmountException() : base("Budget amount must be greater than zero.") { }
}

public class InvalidDateRangeException : DomainException
{
    public InvalidDateRangeException() : base("Start date must be before end date.") { }
}

public class BudgetExceededException : DomainException
{
    public BudgetExceededException(decimal amount, decimal limit)
        : base($"Budget exceeded. Amount: {amount}, Limit: {limit}") { }
}