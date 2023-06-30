namespace OrderManagement.Core.Abstractions;

public interface IClock
{
    DateTime CurrentDate();
    DateTime CurrentServerDate();
}