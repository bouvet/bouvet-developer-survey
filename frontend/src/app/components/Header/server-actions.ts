/* eslint-disable @typescript-eslint/require-await -- Server actions must be async functions */
"use server";

import { cookies } from "next/headers";

const supportedLanguages = [
  {
    I18nLngKey: "iso-8859-1",
    locale: "no",
  },
  {
    I18nLngKey: "iso-8859-1",
    locale: "en",
  },
];

/**
 * Changes the application theme.
 *
 * @param theme - the theme to be set.
 */
export async function changeTheme(theme: any) {
  cookies().set("theme", theme, {
    httpOnly: true,
    secure: process.env.NODE_ENV === "production",
    expires: Date.now() + 365 * 24 * 60 * 60 * 1000, // 1 year from now
    sameSite: "strict",
  });
}

/**
 * Changes the language of a cookie to the specified language.
 *
 * @param language - The language code to set for the cookie.
 */
export async function changeCookieLanguage(language: string) {
  // const lng = supportedLanguages.find((locale: string) => language === locale);
  // // if (lng) {
  // //   cookies().set(I18nLngKey, lng, {
  // //     httpOnly: true,
  // //     secure: process.env.NODE_ENV === "production",
  // //     expires: Date.now() + 365 * 24 * 60 * 60 * 1000, // 1 year from now
  // //     sameSite: "strict",
  // //   });
  // // }
}
