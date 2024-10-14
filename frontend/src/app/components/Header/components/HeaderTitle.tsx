"use client";

// Component
export default function HeaderTitle({ title }: { title: string }) {
  // Render
  return (
    <div className="flex lg:flex-1">
      <a href="#" className="-m-1.5 p-1.5">
        <span className="sr-only">{title}</span>
        <img
          alt=""
          src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
          className="h-8 w-auto"
        />
      </a>
    </div>
  );
}
