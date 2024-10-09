using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSyncService : IImportSyncService
{
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly IQuestionService _questionService;
    private readonly ISurveyService _surveyService;
    
    public ImportSyncService(ISurveyBlockService surveyBlockService, IBlockElementService blockElementService,
        IQuestionService questionService, ISurveyService surveyService)
    {
        _surveyBlockService = surveyBlockService;
        _blockElementService = blockElementService;
        _questionService = questionService;
        _surveyService = surveyService;
    }
    public async Task AddSurveyElements(SurveyElementBlockDto surveyBlockDto, string surveyId)
    {
        var blockElements = new List<NewBlockElementDto>();
        
        foreach (var element in surveyBlockDto.Payload.Values)
        {
            var surveyBlock = await _surveyBlockService.CreateSurveyBlock(new NewSurveyBlockDto
            {
                SurveyId = surveyId,
                Type = element.Type,
                Description = element.Description,
                SurveyBlockId = element.Id
            });
                    
            blockElements.AddRange(element.BlockElements.Select(blockElement => new NewBlockElementDto
            {
                BlockId = surveyBlock.Id,
                Type = blockElement.Type,
                QuestionId = blockElement.QuestionId
            }));
        }

        await _blockElementService.CreateBlockElement(blockElements);
    }

    public async Task AddQuestions(SurveyQuestionsDto questionsDto, string surveyId)
    {
        foreach (var surveyElement in questionsDto.SurveyElements!)
        {
            var newQuestionsForm = new NewQuestionDto
            {
                BlockElementId = surveyElement.PrimaryAttribute,
                SurveyId = surveyId,
                DateExportTag = surveyElement.Payload != null ? surveyElement.Payload.DataExportTag : string.Empty,
                QuestionText = surveyElement.Payload != null ? surveyElement.Payload.QuestionText : string.Empty,
                QuestionDescription = surveyElement.Payload != null
                    ? surveyElement.Payload.QuestionDescription
                    : string.Empty,
                Choices = surveyElement.Payload != null
                    ? surveyElement.Payload.Choices.Select(choice => new NewChoiceDto
                    {
                        Text = choice.Value.Display
                    }).ToList()
                    : null
            };
            
            await _questionService.CreateQuestionAsync(newQuestionsForm);
        }
    }
    
    public async Task CheckForDifference(SurveyBlocksDto surveyBlockDto, Domain.Entities.Survey.Survey survey)
    {
        //Find the survey, check if there are any differences in the name, Language
        var surveyEntry = surveyBlockDto.SurveyEntry;
        
        if (survey.Name != surveyEntry.SurveyName || survey.SurveyLanguage != surveyEntry.SurveyLanguage)
        {
            await _surveyService.UpdateSurveyAsync(survey.Id, new NewSurveyDto
            {
                Name = surveyEntry.SurveyName,
                Language = surveyEntry.SurveyLanguage
            });
        }
        
        //Find all surveyElements related to the survey and look for differencdes on type and description
        
        //Find all blockelements related to the updated surveyelement and updathe the type or questionId if there are any differences
    }
    
    
}