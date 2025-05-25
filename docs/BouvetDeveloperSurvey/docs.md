# Bouvet Developer Survey - New "Bouvet" Survey System Documentation

This document outlines the architecture and components of the new "Bouvet" survey system, compares it to the previous system, and lists future development steps.

## Overview of Changes (New "Bouvet" Survey System)

A new, distinct system has been implemented to handle "Bouvet" surveys, designed for flexibility and clear separation of concerns.

1.  **Survey Definition Management:**
    *   **JSON-based Definitions:** Survey structures are defined using a flexible JSON format. This raw JSON is stored in the `BouvetSurveyStructure` table, uniquely identified by `Year`.
    *   **Service (`IBouvetSurveyDefinitionService`):** Implemented by `BouvetSurveyDefinitionService`.
        *   **Responsibilities:** Manages the CRUD operations for survey JSON definitions stored in `BouvetSurveyStructure`.
        *   When a definition is created or updated, it triggers the `ISurveyStructureService` to unpack the JSON into relational tables.
    *   **Controller (`SurveyDefinitionController`):** Provides API endpoints (GET, PUT, DELETE) for administrators to manage these JSON survey definitions by year.

2.  **Relational Survey Structure (Unpacked):**
    *   **New Entities:** The JSON definition is unpacked into a set of new, relational entities: `BouvetSurvey` (overall survey details), `BouvetSection` (logical groupings of questions), `BouvetQuestion` (individual questions with types like single-choice, multiple-choice, Likert, free-text), and `BouvetOption` (predefined answer choices for questions).
    *   **Database Context (`BouvetSurveyContext`):** A new EF Core `DbContext` dedicated to managing all entities related to the "Bouvet" survey system.

3.  **User Handling:**
    *   **Entity (`BouvetUser`):** Represents a specific respondent's participation in a particular `BouvetSurvey`. It links a `RespondId` (intended to be the unique identifier from SSO) to a specific `BouvetSurvey.Id`.
    *   **Service (`IBouvetUserService`):** Implemented by `BouvetUserService`.
        *   **Responsibilities:** Handles retrieving an existing `BouvetUser` or creating a new one based on the provided `respondId` and the internal `bouvetSurveyId`. This ensures each survey participation by a user is uniquely tracked.

4.  **Response Submission & Retrieval:**
    *   **Entity (`BouvetResponse`):** Stores individual answers provided by a `BouvetUser` to a `BouvetQuestion`. It can link to a selected `BouvetOption` and includes specific boolean fields (`HasWorkedWith`, `WantsToWorkWith`) for Likert-type questions. It now also includes a `FreeTextAnswer` string field for free-text question types.
    *   **Service (`ISurveyResponseService`):** Implemented by `SurveyResponseService`.
        *   **Responsibilities:**
            *   Processes incoming survey submissions (`BouvetSurveyResponseDto`), validates data, creates `BouvetUser` entries (via `IBouvetUserService`), and saves `BouvetResponse` records.
            *   Retrieves the unpacked, relational survey structure for a given year (as `SurveyExportDto`) for consumption by frontend applications.
    *   **Controller (`SurveyResponseController`):** Exposes API endpoints for:
        *   `POST /submit`: Allowing users to submit their survey responses.
        *   `GET /structure/{year}`: Allowing frontends to fetch the survey structure to render the survey form.
    *   **DTOs:**
        *   `BouvetSurveyResponseDto`: The payload for submitting survey answers, containing `RespondentId`, `SurveyExternalId`, and a list of `BouvetAnswerDto`.
        *   `BouvetAnswerDto`: Represents a single answer, containing `QuestionExternalId`, a list of `OptionExternalIds` (for choice-based answers), and now `FreeTextAnswer` (for free-text input).

5.  **Testing Helper (`TestHelperController`):**
    *   Provides API endpoints for development and testing:
        *   `POST /load-sample-survey`: Loads a hardcoded sample survey JSON definition (for year 2028) into the system and triggers unpacking.
        *   `POST /submit-sample-response`: Submits a hardcoded set of answers (including free-text) to the sample survey.

6.  **Configuration (`Program.cs`):**
    *   All new services (`IBouvetSurveyDefinitionService`, `IBouvetUserService`, `ISurveyResponseService`) and the `BouvetSurveyContext` are registered for dependency injection.
    *   Database migrations for `BouvetSurveyContext` are configured to be applied automatically on application startup.

7.  **General Enhancements:**
    *   **Free-Text Answers:** The system now fully supports free-text answers. This involved updating `BouvetResponse` entity, `BouvetAnswerDto`, `SurveyResponseService`, and adding a corresponding database migration.
    *   **Logging:** Basic logging has been introduced in `SurveyResponseService` and is planned for other services.

## Comparison: Old Developer Survey vs. New "Bouvet" Survey System

| Feature                 | Old Developer Survey System (Inferred)                                                                                                | New "Bouvet" Survey System (Implemented)                                                                                                                               |
| :---------------------- | :------------------------------------------------------------------------------------------------------------------------------------ | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Primary DbContext**   | `DeveloperSurveyContext`                                                                                                              | `BouvetSurveyContext` (for new entities), coexists with `DeveloperSurveyContext`. Both use the same connection string.                                                 |
| **Survey Definition**   | Uses `ImportSurveyService` with `SurveyBlocksDto` (different JSON structure). Entities like `SurveyBlock`, `BlockElement`.             | Flexible JSON structure stored in `BouvetSurveyStructure` (keyed by `Year`). Managed by `IBouvetSurveyDefinitionService`.                                            |
| **Survey Structure**    | Entities: `Domain.Entities.Survey.Survey`, `SurveyBlock`, `BlockElement`. Questions might use `DataExportTag`.                          | Relational entities: `BouvetSurvey`, `BouvetSection`, `BouvetQuestion`, `BouvetOption`. Uses `ExternalId` from JSON. Unpacked by `ISurveyStructureService`.             |
| **User Entity**         | `User` entity in `DeveloperSurveyContext`. `RespondId` likely links to SSO ID.                                                        | `BouvetUser` entity in `BouvetSurveyContext`. `RespondId` links SSO ID to a specific `BouvetSurvey.Id`. Managed by `IBouvetUserService`.                               |
| **Response Entity**     | `Response` entity, possibly linked via `ResponseUser`. Free-text handling unclear.                                                    | `BouvetResponse` entity. Links `UserId`, `QuestionId`, `OptionId`. Includes Likert booleans and `FreeTextAnswer` string.                                              |
| **Service Layer**       | `ImportSurveyService`, `ISurveyService`, `IQuestionService`, `IResultService`, `IUserService` (all operating on `DeveloperSurveyContext`). | New dedicated services: `IBouvetSurveyDefinitionService`, `IBouvetUserService`, `ISurveyResponseService` (operating on `BouvetSurveyContext`).                       |
| **API Endpoints**       | Existing controllers for the developer survey.                                                                                        | New controllers: `SurveyDefinitionController`, `SurveyResponseController`, `TestHelperController`.                                                                       |
| **Year-Based Surveys** | Implicit year handling.                                                                                                               | Explicitly designed around `Year` for survey definitions and retrieval.                                                                                                |
| **SSO User Link**       | `RespondId` in `User` entity likely holds SSO unique ID.                                                                              | `RespondId` in `BouvetUser` holds SSO unique ID. Critical that this is derived server-side from authenticated user claims.                                            |

## Discussion on Shared Services

A key consideration is whether services can or should be shared or merged between the old and new survey systems.

*   **User Services (`IUserService` vs. `IBouvetUserService`):**
    *   The old system uses an `IUserService` (visible in `ImportSurveyService`) that interacts with the `User` entity in `DeveloperSurveyContext`.
    *   The new system has `IBouvetUserService` for the `BouvetUser` entity in `BouvetSurveyContext`.
    *   **Can they be merged?** Potentially, if a common underlying user profile store (based on the SSO `RespondId`) was established. However, their current responsibilities and entity scopes are different:
        *   `BouvetUser` is specifically for tracking a user's participation in a *particular* `BouvetSurvey` (composite key idea: `RespondId` + `SurveyId`).
        *   The older `User` entity might be more general or have a different participation model.
    *   **Should they be merged?**
        *   **Recommendation for now: Keep them separate.** This maintains clear boundaries and avoids the complexity of retrofitting the older system or creating a more abstract user service at this stage. The `IBouvetUserService` is tailored to the needs of the new system.
        *   The most critical aspect is ensuring that the `respondId` passed to `IBouvetUserService` is the *actual unique identifier from the authenticated SSO user*, derived server-side in the API controllers.

*   **Survey Structure Service (`ISurveyStructureService`):**
    *   This service is already being leveraged by the new `BouvetSurveyDefinitionService` to unpack the JSON definitions. This is a good example of reusing existing compatible infrastructure.

*   **Results Services:**
    *   The old system has an `IResultService`. The new system will likely require its own distinct service for querying and analyzing results from `BouvetResponse` and related entities in `BouvetSurveyContext`.

## Next Steps (TODO List)

The following are key areas for future development and refinement:

1.  **Refine Error Handling & Logging (High Priority):**
    *   Implement comprehensive, structured logging (using `ILogger`) across all new services (`BouvetSurveyDefinitionService`, `BouvetUserService`, `SurveyResponseService`) and controllers.
    *   Ensure robust error handling for edge cases (e.g., invalid JSON schema, database errors, malformed `RespondId`) and return appropriate HTTP status codes.

2.  **Implement Authorization (High Priority):**
    *   Secure sensitive endpoints, particularly in `SurveyDefinitionController` (e.g., PUT, DELETE methods), using appropriate roles or policies (e.g., "Admin" role).
    *   Ensure all data modification endpoints in `SurveyResponseController` require authenticated users.

3.  **Address SSO User Identity Link in Controllers (High Priority):**
    *   Modify API controllers (especially `SurveyResponseController`) to derive the user's `RespondId` from the authenticated `HttpContext.User.Claims` (e.g., `NameIdentifier` or `oid` from SSO token) instead of relying on client-supplied `RespondentId` in DTOs. This is crucial for security and correct user attribution.

4.  **Add Comprehensive Automated Tests (High Priority):**
    *   **Unit Tests:** For service logic (e.g., `BouvetUserService.GetOrCreateUserAsync`, `SurveyResponseService` answer processing including Likert and free-text, `BouvetSurveyDefinitionService` JSON handling and unpacking trigger).
    *   **Integration Tests:** For API endpoints using an in-memory database or a test database. Test the full flow: define survey, get structure, submit various answer types, verify database state.

5.  **Review and Refactor DTOs (Medium Priority):**
    *   Consider moving DTOs currently in the global namespace (like `BouvetSurveyResponseDto`, `BouvetAnswerDto`) into appropriate namespaces (e.g., `Bouvet.Developer.Survey.Service.TransferObjects.Survey.Response`) for better organization and consistency. Update `using` statements accordingly.

6.  **Code Review and Cleanup (Medium Priority):**
    *   Conduct a thorough code review of all new components.
    *   Remove any dead or commented-out code, temporary debug statements (`Console.WriteLine`).
    *   Ensure consistency in coding style and best practices.

7.  **Frontend Integration Planning/Development (Next Major Phase):**
    *   Plan and develop the frontend application to:
        *   Fetch survey structure via `GET /api/bouvet/survey-responses/structure/{year}`.
        *   Render different question types dynamically.
        *   Construct and submit the `BouvetSurveyResponseDto` payload, including free-text answers.

8.  **Survey Results/Analysis (Next Major Phase):**
    *   Design and implement functionality for querying, aggregating, and potentially exporting results from the `BouvetResponse` table and related entities. This may involve a new "BouvetResultsService".