using System.ClientModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.OpenAI;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Ai;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace Bouvet.Developer.Survey.Service.Survey.Ai;

public class AiService : IAiService
{
    private readonly IConfiguration _configuration;
    private readonly IQuestionService _questionService;
    private readonly IAiAnalyseService _aiAnalyseService;
    private readonly DeveloperSurveyContext _context;
    
    public AiService(IConfiguration configuration, IQuestionService questionService, IAiAnalyseService aiAnalyseService, 
        DeveloperSurveyContext context)
    {
        _configuration = configuration;
        _questionService = questionService;
        _aiAnalyseService = aiAnalyseService;
        _context = context;
    }

    public async Task CheckForDifferenceAsync(string surveyId)
    {
        var surveyQuestions = await _context.Questions
            .Where(q => q.SurveyId == surveyId)
            .ToListAsync();

        foreach (var question in surveyQuestions)
        {
            Console.WriteLine("Checking for difference in AI analysis for question with id: " + question.Id);
            var existingAiAnalyse = await _aiAnalyseService.GetAiAnalysesByQuestionId(question.Id);

            if (existingAiAnalyse != null)
            {
                Console.WriteLine("Updating AI analysis for question with id: " + question.Id);
                var newText = await GetAiSummaryAsync(question.Id);

                if (existingAiAnalyse.Text != newText)
                {
                    await _aiAnalyseService.UpdateAiAnalyse(existingAiAnalyse.Id, new NewAiAnalyseDto
                    {
                        Text = newText,
                        QuestionId = question.Id
                    });
                }
            }
            else
            {
                Console.WriteLine("Creating new AI analysis for question with id: " + question.Id);
                var newText = await GetAiSummaryAsync(question.Id);

                await _aiAnalyseService.CreateAiAnalyse(new NewAiAnalyseDto
                {
                    Text = newText,
                    QuestionId = question.Id
                });
            }
        }
    }
    
    private async Task<string> GetAiSummaryAsync(Guid questionId)
    {
        AzureOpenAIClient azureClient = new(
            new Uri(_configuration["OpenAiUrl"]!),
            new ApiKeyCredential(_configuration["OpenAiSecretKey"]!));
        var chatClient = azureClient.GetChatClient("gpt-4o-mini");
        
        var questionResponseDto = await _questionService.QuestionOnlyResponses(questionId);
        
        var jsonString = JsonSerializer.Serialize(questionResponseDto, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        Console.WriteLine("Sending question to AI");
        ChatCompletion completion = await chatClient.CompleteChatAsync(
            new SystemChatMessage(Prompts.SummaryPrompt),
            new UserChatMessage(jsonString));
        
        return completion.Content[0].Text;
    }
}