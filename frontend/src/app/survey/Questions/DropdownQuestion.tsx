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
    <select {...register(question.id)} className="border p-2 w-fit min-w-32">
      {question?.options?.map((option, index) => (
        <option key={index} value={option.id}>
          {option.value}
        </option>
      ))}
    </select>
  );
};

export default DropdownQuestion;
