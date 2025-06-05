"use client";

import QuestionComponent from "./QuestionComponent";
import { Question, SurveySection } from "./types";
import { FC, ReactNode } from "react";

interface Props {
  section: SurveySection;
  questions: Question[];
}

const FormSection: FC<Props> = ({ section, questions }) => {
  const sectionQuestions = questions?.reduce(
    (questions: ReactNode[], question: Question) => {
      if (question.sectionId === section.id) {
        questions.push(
          <QuestionComponent key={question.id} question={question} />
        );
      }
      return questions;
    },
    []
  );
  return (
    <section
      key={section?.id}
      className="flex flex-col gap-2 survey-section bg-white dark:bg-slate-800"
    >
      <div className="mt-10">
        <h1 className="section-title">{section?.title}</h1>
        <p className="dark:text-gray-400 text-gray-700">
          {section?.description}
        </p>
      </div>
      <div className="flex flex-col w-full h-[875px] overflow-auto">
        {sectionQuestions}
      </div>
    </section>
  );
};

export default FormSection;
