"use client";

import { DefaultLoaderProps } from "../types/types";

// Prop types

// Component
export default function DefaultLoader({ children, title }: DefaultLoaderProps) {
  return <div title={title}>{children}</div>;
}
