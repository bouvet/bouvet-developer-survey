// "use client";

// import React, { useState, useEffect } from "react";

// interface Choice {
//   index: number;
//   text: string;
// }

// interface Question {
//   id: string;
//   text: string;
//   choices: Choice[];
// }

// interface ApiResponse {
//   data: Question[];
// }

// const QuestionFilter: React.FC = () => {
//   const [questions, setQuestions] = useState<Question[]>([]);
//   const [filteredQuestions, setFilteredQuestions] = useState<Question[]>([]);
//   const [selectedQuestion, setSelectedQuestion] = useState<string>("");

//   useEffect(() => {
//     // Fetch data from API
//     fetch("/api/survey/questions")
//       .then((res) => res.json())
//       .then((response: ApiResponse) => {
//         setQuestions(response.data);
//         setFilteredQuestions(response.data); // Default to all questions
//       })
//       .catch((error) => console.error("Error fetching questions:", error));
//   }, []);

//   const handleFilterChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
//     const selectedValue = event.target.value;
//     setSelectedQuestion(selectedValue);

//     if (selectedValue === "") {
//       setFilteredQuestions(questions); // Show all if no specific question is selected
//     } else {
//       setFilteredQuestions(
//         questions.filter((q) => q.text.includes(selectedValue))
//       );
//     }
//   };

//   return (
//     <div>
//       <label htmlFor="questionFilter" style={{ color: 'white' }}>Filter by Question:</label>
//       <select
//         id="questionFilter"
//         value={selectedQuestion}
//         onChange={handleFilterChange}
//       >
//         <option value="">All Questions</option>
//         {questions.map((q) => (
//           <option key={q.id} value={q.text}>
//             {q.text}
//           </option>
//         ))}
//       </select>

//       <h3>Filtered Questions:</h3>
//       <ul>
//         {filteredQuestions.map((q) => (
//           <li key={q.id}>{q.text}</li>
//         ))}
//       </ul>
//     </div>
//   );
// };

// export default QuestionFilter;


"use client";

import React, { useState, useEffect } from "react";
import BarChart from "./BarChart";

interface Choice {
  index: number;
  text: string;
}

interface Answer {
  id: string;
  dataExportTag: string;
  questionText: string;
  questionDescription: string;
  isMultipleChoice: boolean;
  createdAt: string;
  updatedAt: string | null;
  choices: Choice[];
  aiAnalysedText: string | null;
  numberOfRespondents: number;
}

interface ChartWithFilterProps {
  data: Answer;
}

const ChartWithFilter: React.FC<ChartWithFilterProps> = ({ data }) => {
  const [filteredData, setFilteredData] = useState<Answer>(data);
  const [selectedOption, setSelectedOption] = useState<string>("");

  useEffect(() => {
    setFilteredData(data); // Update filtered data when data prop changes
  }, [data]);

  const handleFilterChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = event.target.value;
    setSelectedOption(selectedValue);

    if (selectedValue === "") {
      setFilteredData(data); // Show all if no specific option is selected
    } else {
      const filteredChoices = data.choices.filter((choice) =>
        choice.text.includes(selectedValue)
      );
      setFilteredData({ ...data, choices: filteredChoices });
    }
  };

  return (
    <div>
      <label htmlFor="optionFilter" style={{ color: 'white' }}>Filter by Option:</label>
      <select
        id="optionFilter"
        value={selectedOption}
        onChange={handleFilterChange}
      >
        <option value="">All Options</option>
        <option value="Bidrar til åpen kildekode-prosjekter">Bidrar til åpen kildekode-prosjekter</option>
        <option value="Jeg koder ikke utenfor arbeidet">Jeg koder ikke utenfor arbeidet</option>
        <option value="Hobby">Hobby</option>
        <option value="Skolearbeid eller akademiske prosjekter">Skolearbeid eller akademiske prosjekter</option>
        <option value="Frilans-/kontraktsarbeid">Frilans-/kontraktsarbeid</option>
      </select>

      <h3>Filtered Data:</h3>
      <ul>
        {filteredData.choices.map((choice) => (
          <li key={choice.index}>{choice.text}</li>
        ))}
      </ul>

      {/* Pass filtered data to BarChart */}
      {filteredData.choices.length > 0 && (
        <BarChart
          x={filteredData.choices.map((choice) => choice.index)}
          y={filteredData.choices.map((choice) => choice.text)}
          numberOfRespondents={filteredData.numberOfRespondents}
        />
      )}
    </div>
  );
};

export default ChartWithFilter;