"use client";

import { useFormContext } from "react-hook-form";
import { Question, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: Question;
}

const DropdownQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  return (
    <select
      {...register(question.id, { required: question.required })}
      className="border p-2 w-fit min-w-32"
    >
      {/*TODO: should always be there?*/}
      {question.required && (
        <option key="empty" value="">
          Velg fra listen
        </option>
      )}
      {question?.options?.map((option) => (
        <option key={option.id} value={option.id}>
          {option.value}
        </option>
      ))}
    </select>
  );
};

export default DropdownQuestion;
