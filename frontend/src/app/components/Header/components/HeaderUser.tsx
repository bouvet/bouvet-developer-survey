
"use client";

// Component
export default function HeaderUser({ title }: { title: string }) {

  // Render
  return (
    <div className="hidden lg:flex lg:flex-1 lg:justify-end">
    <a href="#" className="text-sm font-semibold leading-6 text-gray-900">
      {title} <span aria-hidden="true">&rarr;</span>
    </a>
    </div>
    
  );
}
