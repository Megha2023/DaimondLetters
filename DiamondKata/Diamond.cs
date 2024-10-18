using System.Text;
using System.Text.RegularExpressions;

namespace DiamondKata;

public interface IDiamond
{
    string PlotDiamond(string? character);
}

public class Diamond : IDiamond
{
    private readonly List<DiamondDetails> _diamondDetails = new();

    public string PlotDiamond(string? character)
    {
        if (character is null)
        {
            throw new ArgumentNullException(nameof(character),"Null character supplied - must supply a single alphabetical character");
        }

        if (character.Length != 1 || !char.IsLetter(character[0]))
        {
            throw new ArgumentException("Invalid character supplied - must be a single alphabetical character");
        }

        var validCharacter = character
            .ToUpper()
            .ToCharArray()
            .FirstOrDefault();

        CalculateDiamondDetails(validCharacter);
        
        var diamondTop = DrawDiamondToConsole(_diamondDetails);

        // as we don't need the supplied character to be printed again, use linq to skip it
        var diamondBottom = DrawDiamondToConsole(_diamondDetails.SkipLast(1).Reverse());
        _diamondDetails.Clear();
        return $"{diamondTop}{diamondBottom}";
    }

    private void CalculateDiamondDetails(int lastCharacterIndex)
    {
        const int firstCharacterIndex = 'A';
        
        for (var character = firstCharacterIndex; character <= lastCharacterIndex; character++)
        {
            
            _diamondDetails.Add(new DiamondDetails
            {
                Character = (char)character,
                NumberOfInternalSpaces = CalculateInsideSpacing(character),
                NumberOfTrailingSpaces = CalculateLeadingAndTrailingSpacing(character, lastCharacterIndex)
            });
            
        }
    }

    private static int CalculateInsideSpacing(int startingCharacter)
    {
        return (startingCharacter - 'A') * 2 - 1;
    }

    private static int CalculateLeadingAndTrailingSpacing(int startingCharacter, int endingCharacter)
    {
        return endingCharacter % startingCharacter;
    }

    private static string DrawOuterDiamond(int numberOfDashes)
    {
        var stringBuilder = new StringBuilder();

        for (var dash = 0; dash < numberOfDashes; dash++)
        {
            stringBuilder.Append('-');
        }

        return stringBuilder.ToString();
    }

    private static string DrawDiamondToConsole(IEnumerable<DiamondDetails> diamondDetails)
    {
        var stringBuilder = new StringBuilder();

        foreach (var diamondDetail in diamondDetails)
        {
            stringBuilder.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces))
                .Append(diamondDetail.Character);
            //appending character twice 
            if (diamondDetail.NumberOfInternalSpaces > 0)
            {
                stringBuilder.Append(new string('-', diamondDetail.NumberOfInternalSpaces))
                    .Append(diamondDetail.Character);
            }

            stringBuilder.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces))
                .AppendLine();
        }

        return stringBuilder.ToString();
    }
}