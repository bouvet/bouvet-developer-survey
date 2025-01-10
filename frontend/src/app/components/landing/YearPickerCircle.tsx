"use client";

import { motion } from "framer-motion";
import { YearButton } from "./YearButton";
import Circle from "./Circle";
import { useRouter } from "next/navigation";

export const YearPickerCircle = () => {
  const router = useRouter();
  const years = [2024, 2025];

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
        <div className="flex flex-row gap-4">
          {years.map((year) => (
            <YearButton
              key={year}
              disabled={year === 2025}
              onClick={() => router.push(`/${year}`)}
            >
              {year}
            </YearButton>
          ))}
        </div>
      </div>
    </motion.div>
  );
};
