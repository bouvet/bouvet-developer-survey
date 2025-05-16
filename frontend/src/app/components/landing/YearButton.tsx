import React from "react";
import Link from "next/link";
import { motion } from "framer-motion";

type YearButtonProps = {
  year: number;
  disabled: boolean;
};

export const YearButton = ({ year, disabled }: YearButtonProps) => {
  const commonStyle =
    "text-boris-orange-950 bg-boris-orange-300 px-6 py-2 rounded text-xl font-semibold";

  const enabledStyle =
    "hover:bg-boris-orange-400 data-[active]:bg-boris-orange-500";

  const disabledStyle = "cursor-not-allowed opacity-60 point";

  return (
    <motion.li
      whileHover={!disabled ? { scale: 1.1 } : undefined}
      whileTap={!disabled ? { scale: 0.9 } : undefined}
      transition={{ type: "spring", stiffness: 400, damping: 17 }}
    >
      <Link
        aria-disabled={disabled}
        onClick={(e) => (disabled ? e.preventDefault() : undefined)}
        href={`/${year}`}
        className={`${commonStyle} ${disabled ? disabledStyle : enabledStyle}`}
      >
        {year}
      </Link>
    </motion.li>
  );
};
