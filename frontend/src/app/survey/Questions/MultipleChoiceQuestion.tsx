"use client";

import { useFormContext } from "react-hook-form";
import { Question, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: Question;
}

const MultipleChoiceQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  return (
    <div className='flex flex-col gap-3'>
      {question?.options?.map((option, index) => (
        <label key={index} className="block">
          <input
            type="checkbox"
            {...register(question.id)}
            value={option.id}
            className="mr-2"
          />
          {option.value}
        </label>
      ))}
    </div>
  );
};

export default MultipleChoiceQuestion;
