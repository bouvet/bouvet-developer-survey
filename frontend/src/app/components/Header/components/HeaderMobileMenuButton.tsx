"use client";

import { SetStateAction, useState } from "react";
import { Bars3Icon } from "@heroicons/react/24/outline";
// Component
export default function HeaderMobileMenuButton({
  title,
  onClick,
}: {
  title: string;
  onClick: (bool: boolean) => void;
}) {
  // Render
  return (
    <div className="flex lg:hidden">
      <button
        type="button"
        onClick={() => onClick(true)}
        className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
      >
        <span className="sr-only">{title}</span>
        <Bars3Icon aria-hidden="true" className="h-6 w-6" />
      </button>
    </div>
  );
}
