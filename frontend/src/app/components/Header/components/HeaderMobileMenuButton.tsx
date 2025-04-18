import { Bars3Icon } from "@heroicons/react/24/outline";

export default function HeaderMobileMenuButton({
  onClick,
  isLoading,
}: {
  onClick: (bool: boolean) => void;
  isLoading: boolean;
}) {
  return (
    <div className="flex justify-end pr-6 w-full lg:hidden ">
      {!isLoading ? (
        <button
          type="button"
          onClick={() => onClick(true)}
          className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-[var(--foreground)]"
        >
          <Bars3Icon aria-hidden="true" className="h-10 w-10" />
        </button>
      ) : (
        <Bars3Icon aria-hidden="true" className="h-10 w-10 animate-pulse" />
      )}
    </div>
  );
}
