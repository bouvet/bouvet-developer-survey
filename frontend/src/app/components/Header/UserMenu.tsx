import {
  Button,
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
} from "@headlessui/react";
import {
  DocumentArrowDownIcon,
  UserCircleIcon,
  UserIcon,
} from "@heroicons/react/24/solid";
import { ArrowRightStartOnRectangleIcon } from "@heroicons/react/24/outline";
import DarkModeToggle from "./DarkModeToggle";
import { signOut, useSession } from "next-auth/react";

const UserMenu = () => {
  const { data: session } = useSession();
  const exportAsPdf = () => {
    console.log("exportAsPdf");
  };

  return (
    <Menu>
      <MenuButton className="ml-auto mr-8" aria-haspopup="true">
        <UserCircleIcon className="w-10 h-10" aria-hidden="true" />
      </MenuButton>
      <MenuItems
        as="menu"
        modal={false}
        className="z-50 bg-white dark:bg-slate-800 flex flex-col gap-4 py-4 w-52 rounded-lg shadow-lg"
        anchor="bottom"
      >
        <MenuItem as="li">
          <div className="flex px-4 gap-3 text-gray-500 select-none">
            <UserIcon className="size-6" aria-hidden="true" />
            <span>{session?.user?.name}</span>
          </div>
        </MenuItem>
        <MenuItem as="li">
          <DarkModeToggle />
        </MenuItem>
        <MenuItem as="li">
          <Button onClick={exportAsPdf} className="flex px-4 gap-3 hover:bg-">
            <DocumentArrowDownIcon className="size-6" aria-hidden="true" />
            <span>Last ned som pdf</span>
          </Button>
        </MenuItem>
        <MenuItem as="li">
          <Button
            onClick={async () =>
              await signOut({ redirect: true, callbackUrl: "/signin" })
            }
            className="flex px-4 gap-3 hover:bg-"
          >
            <ArrowRightStartOnRectangleIcon
              className="size-6"
              aria-hidden="true"
            />
            <span>Sign out</span>
          </Button>
        </MenuItem>
      </MenuItems>
    </Menu>
  );
};

export default UserMenu;
