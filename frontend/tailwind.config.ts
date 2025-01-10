import type { Config } from "tailwindcss";

const config: Config = {
  content: ["./src/**/*.{js,ts,jsx,tsx,mdx}"],
  darkMode: ["selector", '[data-mode="dark"]'],
  theme: {
    extend: {
      colors: {
        background: "var(--background)",
        foreground: "var(--foreground)",
        "boris-orange": {
          "50": "#fff5ed",
          "100": "#fee9d6",
          "200": "#fccfac",
          "300": "#f9a86f", // base
          "400": "#f68141",
          "500": "#f35f1c",
          "600": "#e44512",
          "700": "#bd3211",
          "800": "#962916",
          "900": "#792415",
          "950": "#411009",
        },
        "network-green": {
          "50": "#eefff1",
          "100": "#d7ffe2",
          "200": "#b2ffc6",
          "300": "#79fe9d", // base
          "400": "#35f369",
          "500": "#0bdc45",
          "600": "#02b734",
          "700": "#068f2d",
          "800": "#0b7028",
          "900": "#0b5c24",
          "950": "#003410",
        },
        "hal-red": {
          "50": "#fff0f1",
          "100": "#ffe3e5",
          "200": "#ffcbd1",
          "300": "#ffa0ac", // base
          "400": "#ff6b82",
          "500": "#fc375a",
          "600": "#eb1d4b",
          "700": "#c50b39",
          "800": "#a50c37",
          "900": "#8d0e35",
          "950": "#4f0218",
        },
      },
      minHeight: {
        "bar-chart": "500px",
      },
      maxWidth: {
        "8xl": "2000px",
      },
      padding: {
        "app-padding-x": "48px",
      },
    },
  },
  plugins: [],
};
export default config;
