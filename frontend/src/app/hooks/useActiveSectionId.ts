"use client";
import { useEffect, useMemo, useReducer, useRef } from "react";
import { menuItems } from "../components/Header/components/menuItems";

export const useActiveSectionId = () => {
  useRerenderOnScroll();
  const elements = useMemo(() => {
    if (typeof window === "undefined") return [];
    const ids = menuItems.map((item) => item.id);
    return ids
      .map((id) => window.document.getElementById(id))
      .filter((element) => element !== null);
  }, []);
  return useOnScreen(elements);
};

function useOnScreen(elements: HTMLElement[]) {
  const intersectingArrayRef = useRef<{ id: string; visible: boolean }[]>([]);

  const observer = useMemo(() => {
    if (typeof IntersectionObserver === "undefined") return null;
    return new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          const item = intersectingArrayRef.current.find(
            (item) => item.id === entry.target.id
          );
          if (item) {
            item.visible = entry.isIntersecting;
          } else {
            intersectingArrayRef.current.push({
              id: entry.target.id,
              visible: entry.isIntersecting,
            });
          }
        });
      },
      { threshold: 0.125 }
    );
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [elements]);

  useEffect(() => {
    elements.forEach((element) => observer?.observe(element));
    return () => observer?.disconnect();
  }, [observer, elements]);

  return intersectingArrayRef.current.findLast((val) => val.visible)?.id;
}

const useRerenderOnScroll = () => {
  const forceUpdate = useForceUpdate();
  useEffect(() => {
    window.addEventListener("scroll", () => {
      forceUpdate();
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
};

function useForceUpdate(): () => void {
  // This is the recommended escape hatch you can use, as described here:
  // https://reactjs.org/docs/hooks-faq.html#is-there-something-like-forceupdate
  // They do suggest avoiding this, so only use this sparingly.
  const [, forceUpdate] = useReducer((dummyVar: number) => dummyVar + 1, 0);
  return forceUpdate;
}
