"use client";

import { useFormContext } from "react-hook-form";
import { LikertQuestion, SurveyFormState } from "../types";
import { FC } from "react";

interface Props {
  question: LikertQuestion;
}

const LikertFormQuestion: FC<Props> = ({ question }) => {
  const { register } = useFormContext<SurveyFormState>();

  const columnKeys = ["Admired", "Desired"];

  return (
    <table className="border-collapse max-w-[70%] mt-2">
      <thead>
        <tr>
          <th className="p-2 text-left"></th>
          {columnKeys.map((col) => (
            <th key={col} className="p-2 text-center">
              {col}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {question.options.map((option, optionIndex) => (
          <tr key={option.id}>
            <td className="p-2">{option.value}</td>
            {columnKeys.map((col) => (
              <td key={col} className="p-2 text-center">
                <input
                  type="checkbox"
                  {...register(
                    `${question.id}.likertAnswers.${optionIndex}.${col.toLowerCase()}` as const
                  )}
                />
              </td>
            ))}
            <input
              type="hidden"
              value={option.id}
              {...register(
                `${question.id}.likertAnswers.${optionIndex}.optionId` as const
              )}
            />
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default LikertFormQuestion;
