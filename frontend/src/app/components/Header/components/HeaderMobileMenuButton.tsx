import { Bars3Icon } from "@heroicons/react/24/outline";

export default function HeaderMobileMenuButton({
  onClick,
}: {
  onClick: (bool: boolean) => void;
}) {
  // Render
  return (
    <div className="flex justify-end pr-6 w-full lg:hidden ">
      <button
        type="button"
        onClick={() => onClick(true)}
        className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
      >
        <Bars3Icon aria-hidden="true" className="h-10 w-10" />
      </button>
    </div>
  );
}
