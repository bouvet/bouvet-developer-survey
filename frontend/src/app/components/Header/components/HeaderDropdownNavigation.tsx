"use client";

import Link from "next/link";

export interface HeaderDropdownNavigationProps {
  id: string;
  text: string;
}

export default function HeaderDropdownNavigation({
  items,
}: {
  items: HeaderDropdownNavigationProps[];
}) {
  return (
    <>
      {items !== undefined && (
        <ul className="invisible absolute flex flex-col gap-2 w-fit z-20 p-4 dark:bg-slate-800 bg-white rounded-lg shadow-lg group-hover:visible">
          {items.map((item: HeaderDropdownNavigationProps) => (
            <li key={item.id}>
              <Link
                href={`#${item.text.replaceAll(/\s/g, "-")}`}
                className="hover:underline decoration-2 underline-offset-4"
              >
                {item.text}
              </Link>
            </li>
          ))}
        </ul>
      )}
    </>
  );
}
