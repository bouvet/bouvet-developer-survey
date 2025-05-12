import { Survey } from "./types";

export const surveyData: Survey = {
  title: "Annual Developer Survey 2025",
  id: "12345",
  startDate: "",
  endDate: "",
  sections: [
    {
      title: "Grunnleggende informasjon",
      description: "description",
      questions: [
        {
          id: 1,
          type: "single-choice",
          title: "Hva er din alder?",
          options: ["18-24 år", "25-34 år", "65 år eller eldre"],
        },
        {
          id: 2,
          type: "dropdown",
          title: "Hvilket kontor tilhører du?",
          options: ["Arendal", "Oslo", "Bergen", "Stavanger"],
        },
        {
          id: 3,
          type: "multiple-choice",
          title:
            "Hvilke av de følgende punktene beskriver best koden du skriver utenom arbeidstid?",
          options: [
            "Frilans",
            "skolearbeid eller akademiske prosjekter",
            "Jeg koder ikke utenfor arbeidet",
          ],
        },
      ],
    },
    {
      title: "Teknologi og teknologikultur",
      description: "description",
      questions: [
        {
          id: 4,
          type: "likert",
          title:
            "Hvilke programmeringsspråk har du prøvd og hvilke ønsker du å jobbe med?",
          rows: ["JavaScript", "TypeScript", "Python", "C#", "Rust", "Go"],
          columns: ["Admired", "Desired"],
        },
      ],
    },
  ],
};
