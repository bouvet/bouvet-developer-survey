"use client";

import { useFormContext } from "react-hook-form";
import { ISurveySection } from "./types";
import { FC } from "react";

interface Props {
  section: ISurveySection;
}

const SurveySection: FC<Props> = ({ section }) => {
  const { register } = useFormContext();
  return (
    <section key={section.title}>
      <h2 className="text-lg font-bold">{section.title}</h2>
      <p className="text-gray-600">{section.description}</p>
      {section.questions.map((question) => (
        <div key={question.id} className="mt-4">
          <label className="font-medium">{question.title}</label>

          {/* Single-choice (radio buttons) */}
          {question.type === "single-choice" && (
            <div>
              {question.options.map((option, index) => (
                <label key={index} className="block">
                  <input
                    type="radio"
                    {...register(`question_${question.id}`)}
                    value={option}
                    className="mr-2"
                  />
                  {option}
                </label>
              ))}
            </div>
          )}

          {/* Dropdown (select) */}
          {question.type === "dropdown" && (
            <select
              {...register(`question_${question.id}`)}
              className="border p-2 rounded"
            >
              {question.options.map((option, index) => (
                <option key={index} value={option}>
                  {option}
                </option>
              ))}
            </select>
          )}

          {/* Multiple-choice (checkboxes) */}
          {question.type === "multiple-choice" && (
            <div>
              {question.options.map((option, index) => (
                <label key={index} className="block">
                  <input
                    type="checkbox"
                    {...register(`question_${question.id}`)}
                    value={option}
                    className="mr-2"
                  />
                  {option}
                </label>
              ))}
            </div>
          )}

          {/* Likert Scale (admired/desired) */}
          {question.type === "likert" && (
            <table className="border-collapse w-full mt-2">
              <thead>
                <tr>
                  <th className="border p-2 text-left">Language</th>
                  {question.columns.map((col, colIndex) => (
                    <th key={colIndex} className="border p-2">
                      {col}
                    </th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {question.rows.map((row, rowIndex) => (
                  <tr key={rowIndex} className="border">
                    <td className="border p-2">{row}</td>
                    {question.columns.map((col, colIndex) => (
                      <td key={colIndex} className="border p-2 text-center">
                        <input
                          type="checkbox"
                          {...register(`question_${question.id}.${row}.${col}`)}
                          className="mr-2"
                        />
                      </td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      ))}
    </section>
  );
};

export default SurveySection;
