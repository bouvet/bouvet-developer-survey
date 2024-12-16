"use client";
import { Dialog, DialogPanel } from "@headlessui/react";
import { XMarkIcon } from "@heroicons/react/24/outline";
import { menuItems } from "./menuItems";
import { useState } from 'react';

export default function MobileMenu({
  mobileMenuOpen,
  onClick,
}: {
  mobileMenuOpen: boolean;
  onClick: (bool: unknown) => void;
}) {
  const [activeTab, setActiveTab] = useState('intro');

  return (
    <Dialog
      open={mobileMenuOpen}
      onClose={() => onClick(false)}
      className="lg:hidden"
    >
      <div className="fixed inset-0 z-10 bg-black/30" aria-hidden="true" />
      <DialogPanel className="fixed inset-x-0 top-0 z-20 w-full bg-white dark:bg-slate-800">
        <div className="px-6 py-4 flex items-center justify-between border-b border-gray-200">
          <button
            type="button"
            onClick={() => onClick(false)}
            className="rounded-md p-2 text-gray-700 hover:bg-gray-100"
          >
            <span className="sr-only">Close menu</span>
            <XMarkIcon aria-hidden="true" className="h-6 w-6" />
          </button>
        </div>
        <nav className="px-4 py-2">
          <div className="space-y-3">
            {menuItems.map((item) => (
              <a
                key={item.id}
                href={`#${item.id}`}
                onClick={() => {
                  setActiveTab(item.id);
                  onClick(false);
                }}
                className={`
                  block px-3 py-1 text-sm font-bold
                  hover:underline decoration-2 underline-offset-
                  ${activeTab === item.id 
                    ? 'text-[#11133C] underline' 
                    : 'text-gray-600'
                  }
                  dark:${activeTab === item.id 
                    ? 'text-[#11133C] underline' 
                    : 'text-white'
                  }
                `}
              >
                {item.label}
              </a>
            ))}
          </div>
        </nav>
      </DialogPanel>
    </Dialog>
  );
}