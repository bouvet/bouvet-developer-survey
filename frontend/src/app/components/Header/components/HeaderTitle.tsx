import Logo from "@/app/components/Header/Logo";
import { useParams } from "next/navigation";

export default function HeaderTitle() {
  // TODO: Error handling for non-existing params when we get real data from backend
  const { year } = useParams<{ year?: string }>();

  return (
    <div className="flex lg:flex-1 pr-5">
      <a href="/" className="-m-1.5 p-1.5">
        <Logo year={year} />
      </a>
    </div>
  );
}
