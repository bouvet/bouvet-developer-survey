"use client";
import Logo from "@/app/components/Header/Logo";

// Component
export default function HeaderTitle() {
  // Render
  return (
    <div className="flex lg:flex-1 pr-5">
      <a href="#" className="-m-1.5 p-1.5">
        <Logo />
      </a>
    </div>
  );
}
