"use client";

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

      {question.type === "dropdown" && (
        <DropdownFormSection question={question} />
      )}

      {question.type === "multiple-choice" && (
        <MultipleChoiceQuestion question={question} />
      )}

      {question.type === "likert" && <LikertFormSection question={question} />}
    </div>
  );

  const getSectionQuestions = (sectionId: string) =>
    surveyData?.questions?.filter((q) => q.sectionId === sectionId);

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

        <button
          type="submit"
          className="mt-4 py-3 px-6 bg-blue-500 text-white ml-auto"
        >
          Submit
        </button>
      </form>
    </FormProvider>
  );
};

export default CustomSurvey;
