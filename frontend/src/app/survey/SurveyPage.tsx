"use client";

import { FormProvider } from "react-hook-form";
import { useSurveyForm } from "./hooks/useSurveyForm";
import { Suspense, useState } from "react";
import FormSection from "./FormSection";
import Spinner from "@/app/components/loading/Spinner";

const SurveyPage = () => {
  const { methods, onSubmit, surveyData, isLoading, isValidating, formState } =
    useSurveyForm();
  const [currentStep, setCurrentStep] = useState(0);
  const totalSteps = surveyData?.sections?.length || 0;
  const isFirstStep = currentStep === 0;
  const isLastStep = currentStep === totalSteps - 1;
  const section = surveyData?.sections?.[currentStep];
  const sectionQuestions = surveyData?.questions?.filter(
    (q) => q.sectionId === section.id
  );

  if (isLoading) return <Spinner />;

  if (!surveyData?.questions?.length && !isLoading && !isValidating)
    return <span>no data</span>;

  return (
    <Suspense fallback={<Spinner />}>
      <FormProvider {...methods}>
        <form onSubmit={onSubmit} className="survey-wrapper">
          <progress
            max={totalSteps}
            value={currentStep + 1}
            className="h-3 max-w-5xl w-full rounded mx-auto"
          />
          <FormSection
            key={section?.id}
            section={section}
            questions={sectionQuestions}
          />
          <div
            className={`flex w-full max-w-8xl mx-auto button-wrapper ${isFirstStep ? "place-content-end" : "justify-between"}`}
          >
            {!isFirstStep && (
              <button
                type="button"
                onClick={() => setCurrentStep((prev) => prev - 1)}
                className="w-20 h-12 py-2 bg-gray-300 text-black min-"
              >
                Tidligere
              </button>
            )}

            {!isLastStep ? (
              <button
                type="button"
                onClick={() => setCurrentStep((prev) => prev + 1)}
                className={`w-20 h-12 py-2 ${!formState.isValid && !isFirstStep ? "bg-blue-300" : "bg-blue-500"} text-white`}
                disabled={!formState.isValid && !isFirstStep}
              >
                Neste
              </button>
            ) : (
              <button
                type="submit"
                className="px-4 bg-green-600 text-white"
                disabled={!formState.isValid}
              >
                Svare
              </button>
            )}
          </div>
        </form>
      </FormProvider>
    </Suspense>
  );
};

export default SurveyPage;
