import type { Config } from "tailwindcss";

const config: Config = {
  content: ["./src/**/*.{js,ts,jsx,tsx,mdx}"],
  darkMode: "selector",
  theme: {
    extend: {
      colors: {
        background: "var(--background)",
        foreground: "var(--foreground)",
      },
      minHeight: {
        "bar-chart": "500px",
      },
      maxWidth: {
        "8xl": '2000px',
      },
      padding: {
        'app-padding-x': 'var(--app-padding)'
      }
    },
  },
  plugins: [],
};
export default config;
