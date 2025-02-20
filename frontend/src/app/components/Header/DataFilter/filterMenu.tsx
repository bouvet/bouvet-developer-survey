"use client"

import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { Button, Menu, MenuButton, MenuItems } from "@headlessui/react";
import { AdjustmentsHorizontalIcon } from "@heroicons/react/24/solid";
import SurveyFilter from "./surveyFilter";
import { XMarkIcon } from '@heroicons/react/16/solid';
import { useSurveyFilters } from '@/app/Context/SurveyFilterContext';

type FilterCategory = {
  category: string;
  options: string[];
  questionId: string;
};

const FilterMenu = () => {
  const { data, error, isLoading } = useSurveyStructure();
  const { filters, setFilters } = useSurveyFilters();
  console.log(filters)
  if (isLoading) return <div>Loading filters...</div>;
  if (error) return <div>Failed to load filters</div>;

  const categories: FilterCategory[] =
    data?.surveyBlocks?.flatMap((surveyBlock) =>
      surveyBlock.blockElements.flatMap((blockElement) =>
        blockElement.questions
          .filter((question) => !question.isMultipleChoice)
          .map((question) => ({
            // @ts-expect-error naming mismatch between front and backend
            category: question.dateExportTag, 
            options: [],
            questionId: question.id,
          }))
      )
    ) || [];

  console.log(categories);
  return (
    <Menu>
      <MenuButton
        className={`ml-auto mr-8 p-2 rounded-full ${!!filters.length ? "text-white" : "text-gray-400"}`}
      >
        <AdjustmentsHorizontalIcon className="w-10 h-10" />
      </MenuButton>
      <MenuItems className="z-50 bg-white dark:bg-slate-800 gap-10 p-4 w-96 rounded-lg shadow-lg max-h-[500px] overflow-y-auto absolute top-full mt-2 right-0">
        <Button className="ml-auto flex" onClick={() => setFilters({})}>Clear filters <XMarkIcon  className='size-5' /></Button>
        {categories.map((filter) => (
          <SurveyFilter
            key={filter.category}
            questionId={filter.questionId}
            category={filter.category}
          />
        ))}
      </MenuItems>
    </Menu>
  );
};

export default FilterMenu;
