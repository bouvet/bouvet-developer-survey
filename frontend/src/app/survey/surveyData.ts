import { Survey } from "./types";

export const surveyData: Survey = {
  title: "Annual Developer Survey 2025",
  id: "12345",
  startDate: "",
  endDate: "",
  year: 2025,
  sections: [
    {
      title: "Grunnleggende informasjon",
      description: "Den første delen vil fokusere på noen grunnleggende opplysninger om hvem du er.",
      id: "s1",
    },
    {
      title: "Teknologi og teknologikultur",
      description: "De neste spørsmålene vil fokusere på teknologi og teknologikultur",
      id: "s2",
    },
  ],
  questions: [
    {
      id: "1",
      type: "single-choice",
      title: "Hva er din alder?",
      sectionId: 's1',
      options: [
        { id: "q1-01", value: "18-24 år" },
        { id: "q1-02", value: "25-34 år" },
        { id: "q1-03", value: "65 år eller eldre" },
      ],
    },
    {
      id: "2",
      type: "dropdown",
      title: "Hvilket kontor tilhører du?",
      sectionId: 's1',
      options: [
        { id: "q2-01", value: "Haugesund" },
        { id: "q2-03", value: "Oslo" },
        { id: "q2-04", value: "Bergen" },
        { id: "q2-05", value: "Stavanger" },
      ],
    },
    {
      id: "3",
      type: "multiple-choice",
      sectionId: 's1',
      title:
        "Hvilke av de følgende punktene beskriver best koden du skriver utenom arbeidstid?",
      options: [
        { id: "q3-o1", value: "Frilans" },
        { id: "q3-o2", value: "skolearbeid eller akademiske prosjekter" },
        { id: "q3-o3", value: "Jeg koder ikke utenfor arbeidet" },
      ],
    },
    {
      id: "4",
      type: "likert",
      sectionId: 's2',
      title:
        "Hvilke programmeringsspråk har du prøvd og hvilke ønsker du å jobbe med?",
      options: [
        { id: "q4-o1", value: "javascript" },
        { id: "q4-o2", value: "Rust" },
        { id: "q4-o3", value: "C#" },
        { id: "q4-o4", value: "Go" },
        { id: "q4-o5", value: "Python" },
        { id: "q4-o6", value: "TypeScript" },
      ],
      columns: ["Jobbet med SISTE året", "Ønsker å jobbe med NESTE år"],
    },
  ],
};
