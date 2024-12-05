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
  barColor: "#11133c",
};

export const darkTheme = {
  dotMinColor: "#F9A86F",
  dotMaxColor: "#1D43C6",
  dottedLineColor: "#ddd",
  thickLineColor: "#ccc",
  graphBackgroundColor: "#1A1B26",
  fontColor: "#E4E4E4",
  barColor: "#11133c",
};

export const useChartTheme = () => {
  const isDark = useDarkMode();
  return isDark ? darkTheme : lightTheme;
};