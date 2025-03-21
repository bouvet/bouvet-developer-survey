
"use client";

import { useSurveyFilters } from "@/app/Context/SurveyFilterContext";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import { FC, useState } from "react";
import { Combobox, ComboboxInput, ComboboxButton, ComboboxOptions, ComboboxOption } from "@headlessui/react";
import { CheckIcon, ChevronDownIcon } from "@heroicons/react/20/solid"; 

interface Props {
  questionId: string;
  category: string;
}

const SurveyFilter: FC<Props> = ({ questionId, category }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  const { filters, setFilters } = useSurveyFilters();
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
    console.log("Filters updated Håvard:", filters); // Add this line to verify filters
  };

  return (
    <div className="mx-auto h-28">
      <label className="py-5 w-full whitespace-nowrap">{category}</label>
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
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => setQuery(event.target.value)}
          />
          <ComboboxButton className="group absolute inset-y-0 right-0 px-2.5">
            <ChevronDownIcon className="size-4 fill-white/60 group-data-[hover]:fill-white" />
          </ComboboxButton>
        </div>
        <ComboboxOptions anchor="bottom" className="z-50 min-w-56 bg-slate-700 shadow-md max-h-2 border-red-600 border-2">
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
    </div>
  );
};

export default SurveyFilter;