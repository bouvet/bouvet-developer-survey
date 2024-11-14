"use client";

import { useState } from 'react';

export default function HeaderTabs() {
  const [activeTab, setActiveTab] = useState('intro');

  const menuItems = [
    { id: 'intro', label: 'Intro' },
    { id: 'about', label: 'Om undersøkelsen' },
    { id: 'languages_and_frameworks', label: 'Språk' },
    { id: 'web_frameworks', label: 'Rammeverk' },
    { id: 'ai', label: 'AI & Søk' },
    { id: 'databases', label: 'Databaser' },
    { id: 'compiler_and_test', label: 'Kompile & Test' },
    { id: 'security', label: 'Sikkerhet' },
    { id: 'other_tools', label: 'Andre verktøy' }
  ];

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