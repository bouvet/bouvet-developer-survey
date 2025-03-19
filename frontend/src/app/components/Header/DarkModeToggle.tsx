"use client";

import { Button } from "@headlessui/react";
import { MoonIcon, SunIcon } from "@heroicons/react/16/solid";
import { useTheme } from "next-themes";
import { useEffect, useState } from "react";

const DarkModeToggle = () => {
  const [mounted, setMounted] = useState(false);
  const { theme, setTheme } = useTheme();

  useEffect(() => {
    setMounted(true);
  }, []);

  if (!mounted) {
    return null;
  }

  const isDarkMode = theme === "dark";

  return (
    <Button
      className="flex px-4 gap-3"
      onClick={() => setTheme(isDarkMode ? "light" : "dark")}
    >
      {isDarkMode ? (
        <>
          <SunIcon className="size-6" aria-hidden="true" />
          <span>Light mode</span>
        </>
      ) : (
        <>
          <MoonIcon className="size-6" aria-hidden="true" />
          <span>Dark mode</span>
        </>
      )}
    </Button>
  );
};

export default DarkModeToggle;
