import { ReactNode, useState } from "react";

interface Props {
  tabs?: string[];
  children: ReactNode | ReactNode[];
}

const AnswerContainer = ({ tabs, children }: Props) => {
  const [activeTab, setActiveTab] = useState(0);

  return (
<<<<<<< HEAD
    <div className={`bg-[#11133c]  flex-[5] relative`}>
      {!!tabs?.length && (
        <div className="flex bg-[#11133c] p-4 gap-8 justify-center">
          {tabs.map((tab, index) => (
            <button
              key={index}
              className={`flex-1 text-center font-bold text-sm  max-w-40 h-8
                        ${
                          activeTab === index
                            ? "text-black bg-slate-50"
                            : "text-white hover:text-blue-400 "
                        }`}
              onClick={() => setActiveTab(index)}
            >
              {tab}
            </button>
          ))}
        </div>
      )}
      {Array.isArray(children) ? children[activeTab] : children}
=======
    <div className={`bg-[#3b3f45] rounded-2xl flex-[5] shadow-md`}>
      <div className="relative flex flex-col h-full">
        {!!tabs?.length && (
          <div className="flex bg-[#2f3237] p-4 rounded-2xl rounded-b-none gap-8 justify-center">
            {tabs.map((tab, index) => (
              <button
                key={index}
                className={`flex-1 text-center font-bold text-sm rounded-md max-w-40 h-8
                  ${
                    activeTab === index
                      ? "text-black bg-slate-50"
                      : "text-white hover:text-blue-400"
                  }`}
                onClick={() => setActiveTab(index)}
              >
                {tab}
              </button>
            ))}
          </div>
        )}
        <div className="flex-1">
          {Array.isArray(children) ? children[activeTab] : children}
        </div>
      </div>
>>>>>>> 98bc988 (fix: unify chart counter position for all charts)
    </div>
  );
};

export default AnswerContainer;
