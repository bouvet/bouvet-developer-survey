import { PropsWithChildren } from "react";
import SessionWrapper from "@/app/api/auth/[...nextauth]/SessionWrapper";

export default function Layout({ children }: PropsWithChildren) {
  return <SessionWrapper>{children}</SessionWrapper>;
}
