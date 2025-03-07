"use client"

import { useSurveyFilters } from "@/app/Context/SurveyFilterContext";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import {
  Combobox,
  ComboboxButton,
  ComboboxInput,
  ComboboxOption,
  ComboboxOptions,
  Field,
  Label,
} from "@headlessui/react";
import { ChevronDownIcon } from "@heroicons/react/24/outline";
import { CheckIcon } from "@heroicons/react/24/solid";
import { FC, useState } from "react";

interface Props {
  questionId: string;
  category: string;
}

const SurveyFilter: FC<Props> = ({ questionId, category }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  const { filters, setFilters } = useSurveyFilters(); // Use context
  const [query, setQuery] = useState("");

  if (isLoading) return null;
  if (error) return <p className="px-4 text-red-500">Failed to load filter.</p>;
  if (!data) return null;

  const filteredData =
    query === ""
      ? data.choices
      : data.choices.filter((choice) =>
          choice.text.toLowerCase().includes(query.toLowerCase())
        );

  const selectedFilters = filters[questionId] || [];


  const handleFilterChange = (newValues: string[]) => {
    setFilters({
      ...filters, 
      [questionId]: newValues,
    });
  };

  return (
    <Field className="mx-auto h-28">
      <Label className="py-5 w-full whitespace-nowrap">{category}</Label>
      <Combobox
        multiple
        value={selectedFilters}
        onChange={handleFilterChange} 
        onClose={() => setQuery("")}
      >
        <div className="relative mt-2">
          <ComboboxInput
            className="w-full border-none bg-white/5 py-1.5 pr-8 pl-3 text-sm/6 text-white"
            aria-label="Assignee"
            placeholder={selectedFilters.join(", ")}
            onChange={(event) => setQuery(event.target.value)}
          />
          <ComboboxButton className="group absolute inset-y-0 right-0 px-2.5">
            <ChevronDownIcon className="size-4 fill-white/60 group-data-[hover]:fill-white" />
          </ComboboxButton>
        </div>
        <ComboboxOptions
          anchor="bottom"
          className="z-50 min-w-56 bg-slate-700 shadow-md max-h-2 border-red-600 border-2"
        >
          {filteredData.map((choice) => (
            <ComboboxOption
              key={choice.id}
              value={choice.text}
              className="flex items-center h-10 hover:bg-slate-500 p-2 whitespace-nowrap"
            >
              {selectedFilters.includes(choice.text) && (
                <CheckIcon className="size-6 text-white" />
              )}
              <span className="flex-[3]">{choice.text}</span>
            </ComboboxOption>
          ))}
        </ComboboxOptions>
      </Combobox>
    </Field>
  );
};

export default SurveyFilter;
