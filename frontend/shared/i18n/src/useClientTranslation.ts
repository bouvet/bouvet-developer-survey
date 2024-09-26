"use client";

import { createInstance, type KeyPrefix, type Namespace } from "i18next";
import { useEffect } from "react";
import {
  initReactI18next,
  useTranslation,
  type UseTranslationOptions,
} from "react-i18next";
import { getOptions, languages } from "./settings";

// Initialize i18next for the client components.
createInstance()
  .use(initReactI18next)
  .init({
    ...getOptions(),
    lng:
      typeof window !== "undefined"
        ? navigator.language.split("-")[0] || undefined
        : undefined,
    preload: typeof window !== "undefined" ? languages : [],
  });

/**
 * Wrapper around react-i18next `useTranslation` hook to be used on the client components.
 *
 * @typeParam N - The type of the translation namespace.
 * @typeParam TKPrefix - The type of the translation key prefix.
 *
 * @param ns - The translation namespace or a readonly version of it.
 * @param options - Options for the translation hook.
 * @returns The result of the `useTranslation` hook.
 */
export function useClientTranslation<
  N extends Namespace,
  TKPrefix extends KeyPrefix<N> = undefined
>(ns?: N | Readonly<N>, options?: UseTranslationOptions<TKPrefix>) {
  // @ts-expect-error -- Suppressing error as the problem is due to overriding types
  return useTranslation(ns, options);
}

/**
 * Hook that updates the i18n of the client translation.
 * ! This hook must be used only once, for example on a LngLayout as it has the URL parameter.
 *
 * @param language - The language to change.
 */
export function useUpdateI18nClientTranslation(language: string) {
  const { i18n } = useClientTranslation();
  const lng = languages.find((locale) => language === locale);
  useEffect(() => {
    if (lng) {
      i18n.changeLanguage(lng);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps -- We want only to run the effect on mount
  }, []);
}
