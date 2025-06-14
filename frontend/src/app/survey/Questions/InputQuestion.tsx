"use client";

import { useFormContext } from "react-hook-form";
import { Question, SurveyFormState } from "../types";
import { FC } from "react";
import { Textarea } from "@headlessui/react";

interface Props {
  question: Question;
}

const InputQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  return (
    <Textarea
      {...register(question.id, { required: question.required })}
      className="border p-2 rounded"
    ></Textarea>
  );
};

export default InputQuestion;
