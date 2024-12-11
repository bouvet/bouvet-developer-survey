import { QuestionMarkCircleIcon } from "@heroicons/react/20/solid";
import { Answer } from "../types/survey";
interface Props {
  data: Answer;
}
const QuestionContainer = ({ data }: Props) => {
  return (
    <div className="bg-white dark:text-white dark:bg-slate-800 rounded-xl w-full p-8 flex-1">
      <div className='sticky top-32 flex flex-col gap-4'>
        <h3 className="text-4xl font-bold mb-5">{data.dateExportTag}</h3>
        <span>
          Ai generated notes on the data. Lorem ipsum dolor sit amet,
          consectetur adipiscing elit. Praesent a laoreet leo. Duis consequat
          dapibus velit et ultrices.
        </span>
        <div className="w-full bg-slate-100 dark:bg-slate-600 rounded-xl text-sm p-2 flex items-center gap-3">
          <QuestionMarkCircleIcon className="w-36 h-36 text-gray-700 dark:text-slate-400" />
          <span dangerouslySetInnerHTML={{ __html: data.questionText }} />
        </div>
      </div>
    </div>
  );
};
export default QuestionContainer;
