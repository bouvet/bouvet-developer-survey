import { TransitionRootProps } from "@headlessui/react";
import {
  HtmlHTMLAttributes,
  ReactElement,
  ReactNode,
  RefAttributes,
  SyntheticEvent,
} from "react";

export interface PortalProps {
  /**
   * The content of the component.
   */
  children: ReactElement & RefAttributes<ReactElement>;
  /**
   * A HTML element.
   * The `container` will have the portal children appended to it.
   *
   * By default, it uses the body of the top-level document object.
   *
   * @default document.body
   */
  container?: HTMLElement;
  /**
   * If `true` The `children` will be inside the DOM hierarchy of the parent component.
   * @default false
   */
  disablePortal?: boolean;
}

export interface BackdropProps
  extends Omit<HtmlHTMLAttributes<HTMLDivElement>, "color">,
    ColorProps,
    LayoutProps,
    BackgroundProps,
    PositionProps {
  /**
   * The element to show in top of the backdrop.
   */
  children?: ReactNode;
  /**
   * The className of the component.
   */
  className?: string;
  /**
   * The css object ot override the styles.
   */
  css?: string;
  /**
   * If `true`, the backdrop is open.
   */
  open: boolean;
  /**
   * If true, the backdrop is invisible.
   * @default false
   */
  invisible?: boolean;
  /**
   * Whether the backdrop can be closed with esc key and outside click.
   * @default false
   */
  closable?: boolean;
  /**
   * Always keep the children in the DOM.
   * @default false
   */
  keepMounted?: boolean;
  /**
   * The duration for the transition, in milliseconds.
   *
   * You may specify a single timeout for all transitions, or individually with an object.
   *
   * @default undefined
   */
  transitionDuration?:
    | number
    | {
        appear: number;
        enter: number;
        exit: number;
      };
  /**
   * Properties object for the transition component.
   * @default {}
   */
  transitionProps?: TransitionRootProps;
  /**
   * Callback fired when the backdrop is closable and click on the backdrop or the esc key.
   * @default undefined
   */
  onClose?: (event: SyntheticEvent | Event) => void;
}

export interface DefaultLoaderProps
  extends Omit<PortalProps, "children">,
    Omit<BackdropProps, "open" | "invisible" | "closable"> {
  /**
   * The loader component to use. By default will use `<StarProgress />`
   * @default undefined
   */
  loader?: ReactNode;
  /**
   * The size of the default loader.
   * @default undefined
   */
  sizeLoader?: number;
  /**
   * The title of the screen loader.
   * @default undefined
   */
  title?: string;
  /**
   * Show/hide the backdrop layer.
   * @default false
   */
  backdropInvisible?: boolean;
}

export interface RtkQueryFetcherStatusProps {
  /**
   * The children component.
   */
  children: ReactNode;
  /**
   * The queries to wait for and check errors.
   * If one of the queries is in loading status a loader screen will be displayed.
   * If one of the queries has an error an error screen will be displayed.
   */
  queries: any[];
  /**
   * `true` to show progress on the loader screen.
   */
  showProgress?: boolean;
  /**
   * The text to be displayed if the query is in loading status.
   * The text on the x position/index represents the query on the same position/index.
   *
   * Only used if a `showFakeProgress` is true.
   */
  queriesProgressText?: string[];
  /**
   * Custom loader to use.
   */
  loader?: ReactNode;
  /**
   * `true` to force the loading status no matter the status of the queries.
   */
  forceLoading?: boolean;
  /**
   * Custom error to use.
   */
  error?: ReactNode;
  /**
   * The queries to check errors and show a notification.
   * It will only check for errors and in any case prevent the consecutive renders.
   */
  errorNotificationQueries?: any[];
  /**
   * The custom text to be used for `errorNotificationQueries` that has errors.
   * The text on the x position/index represents the query on the same position/index.
   */
  queriesErrorText?: string[];
}
