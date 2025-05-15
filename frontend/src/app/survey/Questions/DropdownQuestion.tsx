"use client";

import { useFormContext } from "react-hook-form";
import { Question, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: Question;
}

export const DropdownQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  return (
    <select
      {...register(`question_${question.id}`)}
      className="border p-2 rounded max-w-56"
    >
      {question?.options?.map((option, index) => (
        <option key={index} value={option.id}>
          {option.value}
        </option>
      ))}
    </select>
  );
};

export default DropdownQuestion
