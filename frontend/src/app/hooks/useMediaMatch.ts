import theme from "tailwindcss/defaultTheme";
import { useMediaQuery } from "usehooks-ts";

export const ScreenSize = {
  SM: theme.screens.sm,
  MD: theme.screens.md,
  LG: theme.screens.lg,
  XL: theme.screens.xl,
};

const useMediaMatch = (screenSize: string) => {
  return useMediaQuery(`screen and (min-width: ${screenSize})`);
};

export default useMediaMatch;
