"use client";
import { menuItems } from "./menuItems";
import { useState } from 'react';

export default function HeaderTabs() {
  const [activeTab, setActiveTab] = useState('intro');

  return (
    <div className="pl-10 hidden lg:flex justify-center items-center space-x-6 font-bold text-sm">
      {menuItems.map((item) => (
        <a
        key={item.id}
        href={`#${item.id}`}
        onClick={() => setActiveTab(item.id)}
        className={`
          hover:underline 
          decoration-2
          underline-offset-4
          ${activeTab === item.id ? 'text-[#11133C] underline' : 'text-gray-600'}
        `}
      >
        {item.label}
      </a>
      ))}
    </div>
  );
}