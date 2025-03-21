import React, { useEffect, useState } from 'react';

// Define a minimal type for Survey
type Survey = {
  id: string;
  name: string;
  description: string;
};

function SurveyPage() {
  const [surveys, setSurveys] = useState<Survey[]>([]);  // Use the Survey type
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Fetch surveys data
  useEffect(() => {
    const fetchSurveys = async () => {
      try {
        const response = await fetch('http://localhost:5001/api/v1/Results/Surveys');  // API URL
        if (!response.ok) {
          throw new Error('Failed to fetch surveys');
        }
        const data = await response.json();  // Get the JSON data
        setSurveys(data);  // Set the surveys to state
        setLoading(false);
      } catch (error) {
        setError('An error occurred while fetching data');
        setLoading(false);
      }
    };

    fetchSurveys();
  }, []);

  if (loading) {
    return <p>Loading surveys...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  return (
    <div>
      <h1>Surveys</h1>
      {surveys.length === 0 ? (
        <p>No surveys available.</p>
      ) : (
        <ul>
          {surveys.map((survey) => (
            <li key={survey.id}>
              <h2>{survey.name}</h2>
              <p>{survey.description}</p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default SurveyPage;
