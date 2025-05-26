"use client";

import { FormProvider } from "react-hook-form";
import { Survey } from "./types";
import { useSurveyForm } from "./hooks/useSurveyForm";
import QuestionComponent from "./QuestionComponent";
import { useState } from "react";

const CustomSurvey = ({ surveyData }: { surveyData: Survey }) => {
  const { methods, onSubmit } = useSurveyForm();

  const [currentStep, setCurrentStep] = useState(0);
  const totalSteps = surveyData?.sections?.length || 0;

  const isLastStep = currentStep === totalSteps - 1;
  const isFirstStep = currentStep === 0;

  const getSectionQuestions = (sectionId: string) =>
    surveyData?.questions?.filter((q) => q.sectionId === sectionId);

  if (!surveyData?.questions?.length) return <span>no data</span>;
  return (
    <FormProvider {...methods}>
      <form
        onSubmit={onSubmit}
        className="space-y-6 flex flex-col justify-center px-app-padding-x "
      >
        <div className="max-w-5xl w-full bg-gray-300 h-3 rounded mx-auto">
          <div
            className="h-3 bg-blue-500"
            style={{ width: `${((currentStep + 1) / totalSteps) * 100}%` }}
          ></div>
        </div>

        {(() => {
          const section = surveyData.sections[currentStep];
          const questions = getSectionQuestions(section.id);

          return (
            <section
              key={section.id}
              className="flex flex-col gap-2 survey-section bg-white dark:bg-slate-800"
            >
              <div className='mt-10'>
                <h2 className="section-title">{section.title}</h2>
                <h3 className="dark:text-gray-400 text-gray-700">
                  {section.description}
                </h3>
              </div>
              <div className="flex flex-col w-full h-[875px] overflow-auto">
                {questions.map((q) => (
                  <QuestionComponent key={q.id} question={q} />
                ))}
              </div>
            </section>
          );
        })()}

        {/* <button
          type="submit"
          className="mt-4 px-6 bg-blue-500 text-white ml-auto"
        >
          Submit
        </button> */}
        <div className="flex w-full justify-between max-w-8xl mx-auto">
          {!isFirstStep && (
            <button
              type="button"
              onClick={() => setCurrentStep((prev) => prev - 1)}
              className="w-20 h-12 py-2 bg-gray-300 text-black min-"
            >
             Previous
            </button>
          )}

          {!isLastStep ? (
            <button
              type="button"
              onClick={() => setCurrentStep((prev) => prev + 1)}
              className="w-20 h-12 py-2 bg-blue-500 text-white"
            >
              Next
            </button>
          ) : (
            <button
              type="submit"
              className="px-4 bg-green-600 text-white"
            >
              Submit
            </button>
          )}
        </div>
      </form>
    </FormProvider>
  );
};

export default CustomSurvey;
