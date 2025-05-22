"use client";

import { Question } from "./types";
import { FC } from "react";
import SingleChoiceQuestion from "./Questions/SingleChoiceQuestion";
import DropdownQuestion from "./Questions/DropdownQuestion";
import MultipleChoiceQuestion from "./Questions/MultipleChoiceQuestion";
import LikertFormQuestion from "./Questions/LikertFormQuestion";
import InputQuestion from "./Questions/InputQuestion";

interface Props {
  question: Question;
}

const QuestionComponent: FC<Props> = ({ question }) => {
  return (
    <div
      key={question.id}
      className="mt-4 bg-white dark:bg-slate-800 py-6 px-10 flex flex-col gap-2"
    >
      <h3 className="md:text-4xl font-bold md:mb-5 break-words text-2xl">
        {question.title}
      </h3>
      <h4> {question.description}</h4>

      {/* {question.type === "single-choice" && (
        <SingleChoiceQuestion question={question} />
      )} */}

      {question.type === "single-choice" && (
        <DropdownQuestion question={question} />
      )}
      {question.type === "dropdown" && <DropdownQuestion question={question} />}

      {question.type === "multiple-choice" && (
        <MultipleChoiceQuestion question={question} />
      )}
      {question.type === "input" && <InputQuestion question={question} />}

      {question.type === "likert" && <LikertFormQuestion question={question} />}
    </div>
  );
};

export default QuestionComponent;
