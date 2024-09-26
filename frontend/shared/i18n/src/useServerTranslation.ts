import { createInstance, type KeyPrefix, type Namespace } from "i18next";
import { headers } from "next/headers";
import { initReactI18next } from "react-i18next/initReactI18next";
import { fallbackLng, getOptions } from "./settings";

/**
 * Initializes an i18n instance for translations with the specified language and namespace.
 *
 * @typeParam N - The type of the translation namespace.
 *
 * @param lng - The target language for translation.
 * @param ns - The translation namespace or a readonly version of it.
 * @returns The initialized i18n instance.
 */
async function initI18next<N extends Namespace | null>(
  lng: string,
  ns?: N | Readonly<N>
) {
  const i18nInstance = createInstance();
  await i18nInstance.use(initReactI18next).init(getOptions(lng, ns as never));
  return i18nInstance;
}

/**
 * Retrieves a translation function and i18n instance for server-side translations.
 *
 * @typeParam N - The type of the translation namespace.
 * @typeParam TKPrefix - The type of the translation key prefix.
 *
 * @param lng - The target language for translation.
 * @param ns - The translation namespace or a readonly version of it.
 * @param keyPrefix - The key prefix for translations.
 * @returns An object containing the translation function and i18n instance.
 */
export async function getServerTranslation<
  N extends Namespace,
  TKPrefix extends KeyPrefix<N> = undefined,
>(lng?: string, ns?: N | Readonly<N>, keyPrefix?: TKPrefix) {
  const language = lng || headers().get("X-Language") || fallbackLng;
  const i18nextInstance = await initI18next(language, ns);
  return {
    // @ts-expect-error -- Suppressing error as the problem is due to overriding types.
    t: i18nextInstance.getFixedT(lng, ns, keyPrefix),
    i18n: i18nextInstance,
  };
}
