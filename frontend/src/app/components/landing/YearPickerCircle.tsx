"use client";

import { motion } from "framer-motion";
import { YearButton } from "./YearButton";
import { Circle } from "./Circle";

export const YearPickerCircle = () => {
  const years = [
    { year: 2024, disabled: false },
    { year: 2025, disabled: true },
  ];

  return (
    <motion.div
      className="inline-flex justify-center items-center relative"
      initial={{ scale: 0 }}
      animate={{ scale: 1 }}
      transition={{ duration: 0.5, ease: "easeOut" }}
    >
      <Circle width="350" height="350" fill="var(--contrast-network-green)" />

      <div className="absolute flex flex-col items-center gap-4">
        <h3 className="text-3xl font-bold text-network-green-950 ">Velg år</h3>
        <nav className="flex flex-row gap-4 list-none">
          {years.map((year) => (
            <YearButton
              key={year.year}
              disabled={year.disabled}
              year={year.year}
            />
          ))}
        </nav>
      </div>
    </motion.div>
  );
};
