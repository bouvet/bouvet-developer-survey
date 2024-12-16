import { useTheme } from 'next-themes';

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
  graphBackgroundColor: 'transparent',
  fontColor: "#fff",
  barColor: "#11133c",
};

export const darkTheme = {
  dotMinColor: "#F9A86F",
  dotMaxColor: "#1D43C6",
  dottedLineColor: "#ddd",
  thickLineColor: "#ccc",
  graphBackgroundColor: 'transparent',
  fontColor: "#E4E4E4",
  barColor: "#61DAFB",
};

export const useChartTheme = () => {
  const {theme} = useTheme();
  return theme === 'dark' ? darkTheme : lightTheme;
};