"use client";
import { useMemo, useState } from "react";
import HeaderTitle from "./components/HeaderTitle";
import HeaderTabs from "./components/HeaderTabs";
import HeaderMobileMenu from "./components/HeaderMobileMenu";
import HeaderMobileMenuButton from "./components/HeaderMobileMenuButton";
import UserMenu from "./UserMenu";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { BlockElement, Question, SurveyBlock } from "@/app/types/survey";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";

export type HeaderProps = {
  /**
   * Simple variant will only have the bouvet logo and user menu
   */
  simple?: boolean;
};
export default function Header({ simple }: HeaderProps) {
  const { data, isLoading, isValidating } = useSurveyStructure();
  const [mobileMenuOpen, setMobileMenuOpen] = useState<boolean>(false);

  const technology = useMemo(
    () =>
      data?.surveyBlocks
        ?.filter(
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
                  text: question.dataExportTag,
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
    <header className="shadow-md bg-inherit fixed w-full z-50">
      <nav
        aria-label="Global"
        className="mx-auto flex max-w-8xl p-6 lg:px-app-padding-x items-center"
      >
        <HeaderTitle />
        {!simple && (
          <>
            <HeaderMobileMenuButton
              isLoading={isLoading || isValidating}
              onClick={() => setMobileMenuOpen(true)}
            />
            <HeaderTabs
              subNavigationItems={{ technology, aboutParticipants }}
              isLoading={isLoading || isValidating}
            />
          </>
        )}
        <UserMenu />
      </nav>
      {!isLoading && (
        <HeaderMobileMenu
          mobileMenuOpen={mobileMenuOpen}
          onClick={() => setMobileMenuOpen(false)}
          subNavigationItems={{ technology, aboutParticipants }}
        />
      )}
    </header>
  );
}
