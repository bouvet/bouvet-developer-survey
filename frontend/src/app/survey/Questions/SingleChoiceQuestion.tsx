"use client";

import { useFormContext } from "react-hook-form";
import { Question, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: Question;
}

export const SingleChoiceQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  return (
    <div>
      {question?.options?.map((option, index) => (
        <label key={index} className="block">
          <input
            type="radio"
            {...register(`question_${question.id}`)}
            value={option.id}
            className="mr-2"
          />
          {option.value}
        </label>
      ))}
    </div>
  );
};

export default SingleChoiceQuestion;
