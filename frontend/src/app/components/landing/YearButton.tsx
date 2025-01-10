import React from "react";
import { Button } from "@headlessui/react";
import { motion } from "framer-motion";

type YearButtonProps = {
  disabled?: boolean;
  children: React.ReactNode;
  onClick: () => void;
};

export const YearButton = ({
  children,
  disabled = false,
  onClick,
}: YearButtonProps) => {
  const commonStyle =
    "text-boris-orange-950 bg-boris-orange-300 px-6 py-2 rounded text-xl font-semibold";

  const enabledStyle =
    "hover:bg-boris-orange-400 data-[active]:bg-boris-orange-500";

  const disabledStyle = "cursor-not-allowed opacity-60 point";

  return (
    <motion.div
      whileHover={!disabled ? { scale: 1.1 } : undefined}
      whileTap={!disabled ? { scale: 0.9 } : undefined}
      transition={{ type: "spring", stiffness: 400, damping: 17 }}
    >
      <Button
        disabled={disabled}
        onClick={onClick}
        className={`${commonStyle} ${disabled ? disabledStyle : enabledStyle}`}
      >
        {children}
      </Button>
    </motion.div>
  );
};
