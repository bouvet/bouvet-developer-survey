import type { InitOptions } from "i18next";
import translationEn from "../locales/en/translation.json";
import translationNo from "../locales/no/translation.json";

// Define resources
const resources = {
  en: {
    translation: translationEn,
  },
  no: {
    translation: translationNo,
  },
} as const;

// Define namespaces for typescript type safety.
export type Languages = keyof typeof resources;
export type I18nNamespaces = {
  [k in keyof (typeof resources)["en"]]: (typeof resources)["en"][k];
};
declare module "i18next" {
  interface CustomTypeOptions {
    returnNull: false;
    defaultNS: "translation";
    resources: I18nNamespaces;
  }
}

// Values
export const fallbackLng: Languages = "en";
export const languages: Languages[] = [fallbackLng, "no"];
export const defaultNS: keyof I18nNamespaces = "translation";
export const cookieI18NextLngKey = "i18nextLng";

/**
 * Method to get the options for the i18next instance.
 *
 * @param lng - the language.
 * @param ns - the namespace or namespaces.
 *
 * @returns the i18next options object.
 */
export function getOptions(
  lng: string = fallbackLng,
  ns: keyof I18nNamespaces | (keyof I18nNamespaces)[] = "translation"
): InitOptions {
  return {
    lng,
    supportedLngs: languages,
    fallbackLng: languages,
    ns,
    fallbackNS: defaultNS,
    defaultNS,
    resources,
  };
}
