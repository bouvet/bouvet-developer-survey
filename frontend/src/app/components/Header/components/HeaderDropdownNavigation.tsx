"use client";

import Link from "next/link";
import { Years } from "@/app/components/landing/YearPickerCircle";

export interface HeaderDropdownNavigationProps {
  id: string;
  text: string;
}

const HeaderDropdownNavigation = ({
  items,
}: {
  items: HeaderDropdownNavigationProps[] | Years[];
}) => (
  <>
    {items !== undefined && (
      <ul className="invisible absolute flex flex-col gap-2 w-fit z-20 p-4 dark:bg-slate-800 bg-white rounded-lg shadow-lg group-hover:visible">
        {items.map((item: HeaderDropdownNavigationProps | Years) => (
          <li key={"id" in item ? item.id : item.year}>
            <Link
              href={
                "text" in item
                  ? `#${item?.text?.replaceAll(/\s/g, "-")}`
                  : `/results/${item.year}`
              }
              className="hover:underline decoration-2 underline-offset-4"
            >
              {"text" in item ? item.text : item.year}
            </Link>
          </li>
        ))}
      </ul>
    )}
  </>
);

export default HeaderDropdownNavigation;
