"use client";

<<<<<<< HEAD
import { FormProvider } from "react-hook-form";
import { Question, Survey } from "./types";
import SingleChoiceQuestion from "./Questions/SingleChoiceQuestion";
import MultipleChoiceQuestion from "./Questions/MultipleChoiceQuestion";
import LikertFormSection from "./Questions/LikertFormQuestion";
import { useSurveyForm } from "./hooks/useSurveyForm";
import DropdownFormSection from "./Questions/DropdownQuestion";

const CustomSurvey = ({ surveyData }: { surveyData: Survey }) => {
  const { methods, onSubmit } = useSurveyForm();
  const renderQuestion = (question: Question) => (
    <div
      key={question.id}
      className="mt-4 bg-white dark:bg-slate-800 py-6 px-10 flex flex-col gap-2"
    >
      <label className="font-medium">{question.title}</label>

      {question.type === "single-choice" && (
        <SingleChoiceQuestion question={question} />
      )}

      {question.type === "dropdown" && <DropdownFormSection question={question} />}

      {question.type === "multiple-choice" && (
        <MultipleChoiceQuestion question={question} />
      )}

      {question.type === "likert" && <LikertFormSection question={question} />}
=======
import { FormProvider, useForm } from "react-hook-form";
import {
  Question,
  Survey,
  SurveyFormState,
  SurveyResponseAnswer,
  SurveyResponseDto,
} from "./types";
import { useMsal } from "@azure/msal-react";
import crypto from "crypto";
import { scopes } from '../auth/authConfig';

const CustomSurvey = ({ surveyData }: { surveyData: Survey }) => {
  const methods = useForm<SurveyFormState>();
  const { accounts, instance } = useMsal();
  const { register, handleSubmit } = methods;

  const hashUserId = (userId: string) => {
    return crypto.createHash("sha256").update(userId).digest("hex");
  };

  const onSubmit = async (formData:SurveyFormState) => {
    const userId = hashUserId(accounts[0].localAccountId);
  
    const answers: SurveyResponseAnswer[] = surveyData.questions.reduce(
      (acc: SurveyResponseAnswer[], question: Question) => {
        const fieldName = `question_${question.id}`;
        const value = formData[fieldName as `question_${string}`];
  
        if (value === undefined) return acc;
  
        if (question.type === "likert") {
          const optionIds: string[] = [];
  
          // value is { optionId: { column: boolean } }
          for (const optionId in value) {
            for (const colId in value[optionId]) {
              if (value[optionId][colId]) {
                optionIds.push(`${optionId}-${colId}`);
              }
            }
          }
  
          if (optionIds.length > 0) {
            acc.push({
              questionId: question.id,
              optionIds,
            });
          }
  
          return acc;
        }
  
        if (question.type === "multiple-choice") {
          acc.push({
            questionId: question.id,
            optionIds: Array.isArray(value) ? value : [value],
          });
          return acc;
        }
  
        acc.push({
          questionId: question.id,
          optionIds: [value],
        });
        return acc;
      },
      []
    );
  
    const payload: SurveyResponseDto = {
      respondentId: userId,
      surveyId: surveyData.id,
      answers,
    };
  
    console.log("✅ Final Payload:", payload);
  
    // TODO: Send to backend
        try {
          const account = accounts[0];
          const accessToken = (await instance.acquireTokenSilent({
            account: account,
            scopes: scopes, // Adjust if needed
          })).accessToken;
          
          const response = await fetch(`https://localhost:5001/api/v1/SurveyResponse`, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
              "Authorization": `Bearer ${accessToken}`, // ✅ ADD this
            },
            body: JSON.stringify(payload),
          });
      
          if (!response.ok) {
            throw new Error("Failed to submit survey response");
          }
      
          console.log("✅ Survey response submitted successfully!");
        } catch (error) {
          console.error("❌ Error submitting survey response:", error);
        }
  };
  

  const renderQuestion = (question: Question) => (
    <div key={question.id} className="mt-4">
      <label className="font-medium">{question.title}</label>

      {question.type === "single-choice" && (
        <div>
          {question?.options?.map((option, index) => (
            <label key={index} className="block">
              <input
                type="radio"
                {...register(`question_${question.id}`)}
                value={option.id}
                className="mr-2"
              />
              {option.value}
            </label>
          ))}
        </div>
      )}

      {question.type === "dropdown" && (
        <select
          {...register(`question_${question.id}`)}
          className="border p-2 rounded"
        >
          {question?.options?.map((option, index) => (
            <option key={index} value={option.id}>
              {option.value}
            </option>
          ))}
        </select>
      )}

      {question.type === "multiple-choice" && (
        <div>
          {question?.options?.map((option, index) => (
            <label key={index} className="block">
              <input
                type="checkbox"
                {...register(`question_${question.id}`)}
                value={option.id}
                className="mr-2"
              />
              {option.value}
            </label>
          ))}
        </div>
      )}

{question.type === "likert" && (
  <table className="border-collapse w-full mt-2">
    <thead>
      <tr>
        <th className="border p-2 text-left">Option</th>
        {question.columns.map((col, colIndex) => (
          <th key={colIndex} className="border p-2 text-center">
            {col}
          </th>
        ))}
      </tr>
    </thead>
    <tbody>
      {question.options.map((option, rowIndex) => (
        <tr key={rowIndex}>
          <td className="border p-2">{option.value}</td>
          {question.columns.map((col, colIndex) => (
            <td key={colIndex} className="border p-2 text-center">
              <input
                type="checkbox"
                {...register(`question_${question.id}.${option.id}.${col}`)}
                className="mr-2"
              />
            </td>
          ))}
        </tr>
      ))}
    </tbody>
  </table>
)}


>>>>>>> f72b521 (rebase main)
    </div>
  );

  const getSectionQuestions = (sectionId: string) =>
    surveyData?.questions?.filter((q) => q.sectionId === sectionId);

<<<<<<< HEAD
  // const standaloneQuestions = surveyData?.questions?.filter(
  //   (q) => !q.sectionId
  // );

  if (!surveyData?.questions?.length) return <span>no data</span>;
  return (
    <FormProvider {...methods}>
      <form
        onSubmit={onSubmit}
        className="space-y-6 flex flex-col justify-center px-app-padding-x"
      >
        {surveyData?.sections?.map((section) => (
          <section key={section.id} className="flex flex-col gap-2">
            <div>
              <h2 className="text-lg font-bold">{section.title}</h2>
              <p className="dark:text-gray-400 text-gray-700">
                {section.description}
              </p>
            </div>
            {getSectionQuestions(section.id)?.map(renderQuestion)}
          </section>
        ))}
        {/* 
=======
  const standaloneQuestions = surveyData?.questions?.filter((q) => !q.sectionId);

  if(!surveyData?.questions?.length) return <span>no data</span>
  return (
    <FormProvider {...methods}>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
        {surveyData?.sections?.map((section) => (
          <div key={section.id}>
            <h2 className="text-lg font-bold">{section.title}</h2>
            <p className="text-gray-600">{section.description}</p>
            {getSectionQuestions(section.id)?.map(renderQuestion)}
          </div>
        ))}

>>>>>>> f72b521 (rebase main)
        {standaloneQuestions.length > 0 && (
          <div>
            <h2 className="text-lg font-bold mt-6">Other Questions</h2>
            {standaloneQuestions?.map(renderQuestion)}
          </div>
<<<<<<< HEAD
        )} */}

        <button
          type="submit"
          className="mt-4 py-3 px-6 bg-blue-500 text-white ml-auto"
=======
        )}

        <button
          type="submit"
          className="mt-4 p-2 bg-blue-500 text-white rounded"
>>>>>>> f72b521 (rebase main)
        >
          Submit
        </button>
      </form>
    </FormProvider>
  );
};

export default CustomSurvey;
