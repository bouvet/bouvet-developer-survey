/* eslint-disable @typescript-eslint/require-await -- Server actions must be async functions */
"use server";


/**
 * Changes the language of a cookie to the specified language. test
 *
 * @param language - The language code to set for the cookie.
 */
export async function changeCookieLanguage() {
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
