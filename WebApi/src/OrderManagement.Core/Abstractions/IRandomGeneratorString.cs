namespace OrderManagement.Core.Abstractions;

public interface IRandomGeneratorString
{
    string GenerateString(int length = 128);
}