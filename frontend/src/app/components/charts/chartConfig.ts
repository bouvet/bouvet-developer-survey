import { useDarkMode } from "@/app/hooks/useDarkMode";

export const chartConfig = {
  dottedLineWidth: 1,
  thickLineWidth: 3,
  markerSize: 15,
  fontSize: 11,
  yItemHeight: 30,
};

export const lightTheme = {
  dotMinColor: "#F9A86F",
  dotMaxColor: "#1D43C6",
  dottedLineColor: "#ddd",
  thickLineColor: "#ccc",
  graphBackgroundColor: "#fff",
  fontColor: "#555",
};

export const darkTheme = {
  dotMinColor: "#F9A86F",
  dotMaxColor: "#1D43C6",
  dottedLineColor: "#ddd",
  thickLineColor: "#ccc",
  graphBackgroundColor: "#000",
  fontColor: "#fff",
};

export const useChartTheme = () => {
  const isDark = useDarkMode();
  return isDark ? darkTheme : lightTheme;
};