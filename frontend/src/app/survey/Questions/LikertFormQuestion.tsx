"use client";

import { useFormContext } from "react-hook-form";
import { LikertQuestion, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: LikertQuestion;
}

const LikertFormQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();
  console.log(question)
  return (
    <table className="border-collapse max-w-[70%] mt-2">
      <thead>
        <tr>
          <th className="border p-2 text-left"></th>
          {question?.columns?.map((col, colIndex) => (
            <th key={colIndex} className="border p-2 text-center">
              {col}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {question?.options?.map((option, rowIndex) => (
          <tr key={rowIndex}>
            <td className="border p-2">{option.value || ""}</td>
            {question?.columns?.map((col, colIndex) => (
              <td key={colIndex} className="border p-2 text-center">
                <input
                  type="checkbox"
                  {...register(`${question.id}.${option.id}.${col}`)}
                  className="mr-2"
                />
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default LikertFormQuestion;
