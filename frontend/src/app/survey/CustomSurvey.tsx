"use client";

import { FormProvider } from "react-hook-form";
import { Survey } from "./types";
import { useSurveyForm } from "./hooks/useSurveyForm";
import QuestionComponent from "./QuestionComponent";

const CustomSurvey = ({ surveyData }: { surveyData: Survey }) => {
  const { methods, onSubmit } = useSurveyForm();

  const getSectionQuestions = (sectionId: string) =>
    surveyData?.questions?.filter((q) => q.sectionId === sectionId);

  if (!surveyData?.questions?.length) return <span>no data</span>;
  return (
    <FormProvider {...methods}>
      <form
        onSubmit={onSubmit}
        className="space-y-6 flex flex-col justify-center px-app-padding-x"
      >
        {surveyData?.sections?.map((section) => {
          const questions = getSectionQuestions(section.id);
          return (
            <section key={section.id} className="flex flex-col gap-2 survey-section">
              <div>
                <h2 className="section-title">{section.title}</h2>
                <h3 className="dark:text-gray-400 text-gray-700">
                  {section.description}
                </h3>
              </div>
              {questions.map((q) => (
                <QuestionComponent key={q.id} question={q} />
              ))}
            </section>
          );
        })}

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
