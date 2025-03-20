import { QuestionMarkCircleIcon } from "@heroicons/react/20/solid";
import { Answer } from "../../types/survey";
import { ChevronDownIcon } from "@heroicons/react/24/outline";
import useMediaMatch, { ScreenSize } from "@/app/hooks/useMediaMatch";
import { useEffect, useState } from "react";

interface Props {
  data: Answer;
}

const QuestionContainer = ({ data }: Props) => {
  const isSmallScreen = !useMediaMatch(ScreenSize.MD);
  const [isOpen, setIsOpen] = useState<boolean>(!isSmallScreen);

  useEffect(() => {
    setIsOpen(!isSmallScreen);
  }, [isSmallScreen]);

  return (
    <div className="bg-white dark:text-white dark:bg-slate-800 w-full p-8 flex-[2]">
      <div className="sticky top-32 flex flex-col gap-4 transition">
        <div className="flex place-content-between">
          <h3 className="md:text-4xl font-bold md:mb-5 break-words text-2xl">
            {data.dataExportTag}
          </h3>
          {isSmallScreen && (
            <button onClick={() => setIsOpen((prev) => !prev)}>
              <ChevronDownIcon
                className={`text-gray-600 h-6 w-6 transition ${isOpen && "rotate-180"}`}
                aria-hidden="true"
              />
            </button>
          )}
        </div>
        {isOpen && (
          <>
            <p>{data.aiAnalysedText}</p>
            <div className="w-full bg-[#eef5f9] dark:bg-slate-600 text-sm p-2 flex items-center gap-3">
              <QuestionMarkCircleIcon className="max-w-6 min-w-6 max-h-6 min-h-6 text-gray-700 dark:text-slate-400" />
              <p dangerouslySetInnerHTML={{ __html: data.questionText }} />
            </div>
          </>
        )}
      </div>
    </div>
  );
};
export default QuestionContainer;
