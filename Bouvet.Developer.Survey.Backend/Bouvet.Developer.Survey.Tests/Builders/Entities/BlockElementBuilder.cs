using System;
using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class BlockElementBuilder
{
    private readonly BlockElement _blockElement;
    private readonly SurveyBlockBuilder _surveyBlockBuilder = new();
    public readonly Guid Id = Guid.Parse("a0e1ff8a-3383-49b6-8c29-31b78a28718e");
    public const string Type = "Test Block Element";
    public const string QuestionId = "question-123457";
    
    public BlockElementBuilder()
    {
        var surveyBlock = _surveyBlockBuilder.Build();
        
        _blockElement = new BlockElement
        {
            Id = Id,
            SurveyElementGuid = surveyBlock.Id,
            Type = Type,
            QuestionId = QuestionId
        };
    }
    
    public BlockElement Build()
    {
        return _blockElement;
    }
}