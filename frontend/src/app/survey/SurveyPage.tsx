"use client";

import { FormProvider } from "react-hook-form";
import { useSurveyForm } from "./hooks/useSurveyForm";
import { Suspense, useState } from "react";
import FormSection from "./FormSection";
import Spinner from "@/app/components/loading/Spinner";

const SurveyPage = () => {
  const { methods, onSubmit, surveyData, isLoading, isValidating } =
    useSurveyForm();
  const [currentStep, setCurrentStep] = useState(0);
  const totalSteps = surveyData?.sections?.length || 0;

  const isLastStep = currentStep === totalSteps - 1;
  const isFirstStep = currentStep === 0;
  const section = surveyData?.sections?.[currentStep];

  if (isLoading) return <Spinner />;

  if (!surveyData?.questions?.length && !isLoading && !isValidating)
    return <span>no data</span>;

  return (
    <Suspense fallback={<Spinner />}>
      <FormProvider {...methods}>
        <form onSubmit={onSubmit} className="survey-wrapper">
          <div className="max-w-5xl w-full bg-gray-300 h-3 rounded mx-auto">
            <div
              role="progressbar"
              className="h-3 bg-blue-500 rounded-l"
              style={{ width: `${((currentStep + 1) / totalSteps) * 100}%` }}
            ></div>
          </div>
          <FormSection
            key={section?.id}
            section={section}
            questions={surveyData?.questions}
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
                className="w-20 h-12 py-2 bg-blue-500 text-white"
              >
                Neste
              </button>
            ) : (
              <button type="submit" className="px-4 bg-green-600 text-white">
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
