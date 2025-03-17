"use client";
import { useActiveSectionId } from "@/app/hooks/useActiveSectionId";
import { HeaderTab } from "./HeaderTab";
import { menuItems } from "./menuItems";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { useMemo } from "react";
import { BlockElement, Question, SurveyBlock } from "@/app/types/survey";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";

export default function HeaderTabs() {
  const activeId = useActiveSectionId();
  const { data } = useSurveyStructure();

  const technology = useMemo(
    () =>
      data?.surveyBlocks
        .filter(
          (surveyBlock: SurveyBlock) =>
            surveyBlock.blockElements.filter(
              (blockElement) =>
                blockElement.questions.filter(
                  (question: Question) => question.isMultipleChoice
                ).length !== 0
            ).length !== 0
        )
        .map(
          (blockElement: SurveyBlock): HeaderDropdownNavigationProps => ({
            id: blockElement.id,
            text: blockElement.description,
          })
        ),
    [data]
  );

  const aboutParticipants = useMemo(
    () =>
      data?.surveyBlocks?.reduce(
        (acc: HeaderDropdownNavigationProps[], surveyBlock: SurveyBlock) => {
          surveyBlock.blockElements.forEach((blockElement: BlockElement) => {
            blockElement.questions.forEach((question: Question) => {
              if (!question.isMultipleChoice) {
                acc.push({
                  id: question.id,
                  text: question.dateExportTag!,
                });
              }
            });
          });
          return acc;
        },
        []
      ),
    [data]
  );

  return (
    <ul className="hidden lg:flex w-full justify-center items-center space-x-6 font-bold text-sm">
      {menuItems.map((item) => (
        <HeaderTab
          key={item.id}
          item={item}
          isActive={activeId === item.id}
          technology={technology}
          aboutParticipants={aboutParticipants}
        />
      ))}
    </ul>
  );
}
