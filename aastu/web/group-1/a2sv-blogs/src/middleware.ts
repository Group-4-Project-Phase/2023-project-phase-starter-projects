import { NextResponse } from "next/server";
import { NextRequest } from "next/server";

export function middleware(request: NextRequest) {
  if (!request.headers.authorization) {
    return NextResponse.next();
  }
  return NextResponse.redirect(new URL("/auth/login", request.url));
}

export const config = {
  matcher: ["/profile/:path*", "/blogs/create"],
};