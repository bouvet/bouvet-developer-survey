import { useTheme } from "next-themes";
import { Shape } from "plotly.js";

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
  graphBackgroundColor: "transparent",
  fontColor: "#fff",
  barColor: "#f9a86f",
};

export const darkTheme = {
  dotMinColor: "#F9A86F",
  dotMaxColor: "#1D43C6",
  dottedLineColor: "#ddd",
  thickLineColor: "#ccc",
  graphBackgroundColor: "transparent",
  fontColor: "#E4E4E4",
  barColor: "#61DAFB",
};

export const useChartTheme = () => {
  const { theme } = useTheme();
  return theme === "dark" ? darkTheme : lightTheme;
};

export const getGridShapes = (data: unknown[]): Partial<Shape>[] =>
  data
    .map((_, i) => ({
      type: "line",
      xref: "paper",
      yref: "y",
      x0: 0,
      x1: 1,
      y0: i - 0.5,
      y1: i - 0.5,
      line: {
        color: "rgba(255, 255, 255, 0.3)",
        width: 1,
        dash: "dot",
      },
    }))
    .slice(1) as Partial<Shape>[];
