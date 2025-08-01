"use client";

import { FormProvider } from "react-hook-form";
import { useSurveyForm } from "./hooks/useSurveyForm";
import { useState } from "react";
import FormSection from "./FormSection";

const SurveyPage = () => {
  const { methods, onSubmit, surveyData } = useSurveyForm();

  const [currentStep, setCurrentStep] = useState(0);
  const totalSteps = surveyData?.sections?.length || 0;

  const isLastStep = currentStep === totalSteps - 1;
  const isFirstStep = currentStep === 0;
  const section = surveyData?.sections?.[currentStep];

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

        <FormSection
          key={section.id}
          section={section}
          questions={surveyData.questions}
        ></FormSection>

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
            <button type="submit" className="px-4 bg-green-600 text-white">
              Submit
            </button>
          )}
        </div>
      </form>
    </FormProvider>
  );
};

export default SurveyPage;
