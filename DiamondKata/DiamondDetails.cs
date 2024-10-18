namespace DiamondKata;

// used records as data is immutable and performance is faster with value type rather than reference type e.g. class
public record DiamondDetails
{
    public char Character { get; init; }
    public int NumberOfTrailingSpaces { get; init; }
    public int NumberOfInternalSpaces { get; init; }
}