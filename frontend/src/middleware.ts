import { getToken } from "next-auth/jwt";
import type { NextRequest } from "next/server";
import { NextResponse } from "next/server"; // API Paths to be restricted.

// API Paths to be restricted.
const protectedRoutes = ["/survey", "/results/2024", "/results/2025", "/"];

export default async function middleware(request: NextRequest) {
  const res = NextResponse.next();
  const pathname = request.nextUrl.pathname;
  if (protectedRoutes.some((path) => pathname === path)) {
    const token = await getToken({
      req: request,
    });

    if (!token) {
      const signInUrl = new URL("/signin", request.nextUrl.origin);
      signInUrl.searchParams.set("callbackUrl", request.url);
      return NextResponse.redirect(signInUrl);
    }
  }
  return res;
}
