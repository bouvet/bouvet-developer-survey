import theme from "tailwindcss/defaultTheme";
import { useCallback, useEffect, useState } from "react";

export const ScreenSize = {
  SM: theme.screens.sm,
  MD: theme.screens.md,
  LG: theme.screens.lg,
  XL: theme.screens.xl,
};

const useMediaMatch = (screenSize: string) => {
  const [isMatch, setIsMatch] = useState(false);

  const updateMatch = useCallback(
    (e: MediaQueryListEvent) => {
      setIsMatch(e.matches);
    },
    [setIsMatch]
  );

  useEffect(() => {
    if (typeof window !== "undefined") {
      const media = window.matchMedia(`screen and (min-width: ${screenSize})`);
      media.addEventListener("change", updateMatch);

      setIsMatch(media.matches);

      return () => media.removeEventListener("change", updateMatch);
    }
  }, [setIsMatch, updateMatch, screenSize]);

  return isMatch;
};

export default useMediaMatch;
