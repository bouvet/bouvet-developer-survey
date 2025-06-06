"use client";

import { Question, QuestionType } from "./types";
import { FC } from "react";
import DropdownQuestion from "./Questions/DropdownQuestion";
import MultipleChoiceQuestion from "./Questions/MultipleChoiceQuestion";
import LikertFormQuestion from "./Questions/LikertFormQuestion";
import InputQuestion from "./Questions/InputQuestion";

interface Props {
  question: Question;
}

const QuestionComponent: FC<Props> = ({ question }: Props) => (
  <fieldset
    key={question.id}
    className="mt-4 bg-white dark:bg-slate-800 py-6 md-px-10 flex flex-col gap-2"
  >
    <legend>
      <h2 className="md:text-4xl font-bold md:mb-5 break-words text-2xl">
        {question.title} - {question.id}
      </h2>
      {!!question.description && <h3>{question.description}</h3>}
    </legend>
    {/* {question.type === "single-choice" && (
        <SingleChoiceQuestion question={question} />
      )} */}

    {question.type === QuestionType.SINGLE_CHOICE && (
      <DropdownQuestion question={question} />
    )}
    {question.type === QuestionType.DROPDOWN && (
      <DropdownQuestion question={question} />
    )}

    {question.type === QuestionType.MULTIPLE_CHOICE && (
      <MultipleChoiceQuestion question={question} />
    )}
    {question.type === QuestionType.INPUT && (
      <InputQuestion question={question} />
    )}

    {question.type === QuestionType.LIKERT && (
      <LikertFormQuestion question={question} />
    )}
  </fieldset>
);

export default QuestionComponent;
