"use client";
import Link from "next/link";

export type HeaderTabProps = {
  item: { id: string; label: string };
  isActive: boolean;
};
export const HeaderTab = ({ item, isActive }: HeaderTabProps) => {
  return (
    <Link
      key={item.id}
      href={`#${item.id}`}
      className={`
    hover:underline 
    decoration-2
    underline-offset-4
    ${isActive ? "underline" : ""}
  `}
    >
      {item.label}
    </Link>
  );
};
