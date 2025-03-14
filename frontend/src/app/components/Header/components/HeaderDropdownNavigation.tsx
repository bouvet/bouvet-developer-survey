"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import Link from "next/link";
import { SurveyBlock } from "@/app/types/survey";

export default function HeaderDropdownNavigation() {
  const { data } = useSurveyStructure();
  return (
    <>
      {data?.surveyBlocks !== undefined && (
        <ul className="invisible absolute flex flex-col gap-2 w-fit z-20 p-4 dark:bg-slate-800 bg-white rounded-lg shadow-lg group-hover:visible">
          {data?.surveyBlocks
            .filter(
              (surveyBlock: SurveyBlock) =>
                surveyBlock.blockElements.filter(
                  (blockElement) => blockElement.questions.length > 0
                ).length !== 0
            )
            .map((surveyBlock: SurveyBlock) => (
              <li key={surveyBlock.id}>
                <Link
                  href={`#${surveyBlock.description.replaceAll(/\s/g, "-")}`}
                  className="hover:underline decoration-2 underline-offset-4"
                >
                  {surveyBlock.description}
                </Link>
              </li>
            ))}
        </ul>
      )}
    </>
  );
}
