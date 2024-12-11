"use client";
import { useEffect } from "react";
import { useLocalStorage } from "usehooks-ts";
import { Button } from "@headlessui/react";
import { MoonIcon, SunIcon } from '@heroicons/react/16/solid';

const DarkModeToggle = () => {
  const [isDarkMode, setIsDarkMode] = useLocalStorage("isDarkMode", false, {
    initializeWithValue: false,
  });

  useEffect(() => {
    if (isDarkMode) {
      return document.body.classList.add("dark");
    }
    document.body.classList.remove("dark");
  }, [isDarkMode]);

  return (
    <div className='flex-1 min-w-32'>
    <Button onClick={() => setIsDarkMode(!isDarkMode)} className='flex items-center w-32 text-nowrap'>
      {isDarkMode ? "Light mode" : "Dark mode"}
      {isDarkMode ? <SunIcon /> : <MoonIcon />}
    </Button>
    </div>
  );
};

export default DarkModeToggle;
