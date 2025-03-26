const Logo = ({ year }: { year?: string }) => {
  return (
    <svg
      width="200"
      height="64"
      viewBox="0 0 200 64"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
    >
      <style>
        {`.text {font: 500 14px 'Inter', Ubuntu, Sans-Serif; fill: var(--foreground);}`}
      </style>
      <text x="0" y="50" className="text">
        Developer Survey {year}
      </text>
      <path
        d="M192.525 29.6088L200 42.5433H185.048L192.525 29.6088Z"
        fill="#79FE9D"
        className="dark:invert"
      />
      <path
        d="M182.121 29.6088L189.596 42.5433H174.641L182.121 29.6088Z"
        fill="#79FE9D"
        className="dark:invert"
      />
      <path
        d="M146.605 32.5734L174.735 32.8827C174.512 30.6088 173.382 28.59 171.106 27.0706C151.821 14.1767 161.012 2.23251 146.43 1.6842L146.605 32.5734Z"
        fill="#1D43C6"
        className="dark:invert"
      />
      <path
        d="M176.72 16.7596H196.986C196.986 22.383 192.449 26.9421 186.853 26.9421C181.256 26.9421 176.72 22.383 176.72 16.7596Z"
        fill="#F9A86F"
        stroke="#F9A86F"
        strokeWidth="1.5"
        strokeMiterlimit="10"
        className="dark:invert"
      />
      <path
        d="M146.605 32.5734L174.735 32.8827C174.512 30.6088 173.382 28.59 171.106 27.0706C151.821 14.1767 161.012 2.23251 146.43 1.6842L146.605 32.5734Z"
        fill="#1D43C6"
        className="dark:invert"
      />
      <path
        d="M92.2228 10.9249L85.7042 27.5368L79.1346 10.9249H74.0067L83.1374 32.393H87.796L96.9324 10.9249H92.2228ZM66.4081 10.9249V22.2596C66.4081 26.3298 64.4067 28.6933 60.9128 28.6933C57.4188 28.6933 55.4117 26.3298 55.4117 22.2596V10.9249H50.8436V22.4449C50.8436 28.8337 54.7107 32.8533 60.9071 32.8533C67.1035 32.8533 70.9707 28.8281 70.9707 22.4449V10.9249H66.4025H66.4081ZM36.6302 10.414C34.9228 10.414 33.2323 10.7958 31.7002 11.5368C27.9009 13.3782 25.5038 17.2295 25.5433 21.4288C25.5433 26.9361 30.7164 33.3193 35.1433 33.3193C36.2627 33.3081 37.2973 32.7354 37.891 31.7923C39.7115 29.0639 41.5715 27.4919 44.3192 26.2849C46.886 25.1733 47.5814 24.0618 47.5814 21.2435C47.5814 15.1354 42.7814 10.414 36.6302 10.414ZM41.4754 24.8028C39.6097 25.9593 38.6373 26.9305 37.8853 28.4575C37.5744 29.0695 36.9412 29.4456 36.2514 29.4288C33.2663 29.4288 29.8684 25.5382 29.8684 22.1193C29.8684 18.2793 32.7122 15.4105 36.5793 15.4105C40.2486 15.4218 43.2111 18.386 43.1941 22.0295C43.1941 22.0575 43.1941 22.0912 43.1941 22.1193C43.1941 23.3207 42.7758 24.0168 41.4698 24.8028H41.4754ZM11.9349 10.414C9.3738 10.386 6.89749 11.3516 5.03743 13.0975L4.9922 0.00561416H0.565369V32.393H5.13354V30.2203C6.99926 31.8877 9.42469 32.8084 11.9349 32.8084C18.0861 32.8084 22.7448 27.9523 22.7448 21.614C22.7448 15.2758 18.0861 10.4196 11.9349 10.4196V10.414ZM11.5618 28.6428C7.83035 28.6428 5.03743 25.6337 5.03743 21.6084C5.03743 17.5832 7.83035 14.574 11.5618 14.574C15.2932 14.574 18.0861 17.5832 18.0861 21.6084C18.0861 25.6337 15.2932 28.6428 11.5618 28.6428ZM135.451 10.9193H128.836L128.785 0H124.358V10.9193H121.317V14.8491H124.358V32.3874H128.881L128.836 14.8491H135.451V10.9193Z"
        fill="var(--foreground)"
      />
      <path
        d="M110.773 14.5909C110.309 14.4505 109.834 14.3607 109.353 14.3214C109.834 14.3495 110.315 14.4393 110.773 14.5909Z"
        fill="var(--foreground)"
      />
      <path
        d="M119.445 23.0625H102.739C103.078 25.1453 103.909 26.88 105.803 27.9354C106.804 28.4688 107.923 28.7439 109.06 28.727C111.027 28.7551 112.887 28.3621 114.476 27.1214C114.956 26.7116 115.414 26.2737 115.844 25.8133L118.897 28.7719C118.286 29.5018 117.58 30.1474 116.8 30.6863C114.278 32.4379 111.462 33.0779 108.438 32.9095C105.984 32.8253 103.638 31.8877 101.812 30.2596C99.7423 28.4126 98.6512 26.0547 98.3459 23.3207C98.0293 20.9684 98.3911 18.5712 99.3975 16.4154C100.997 13.1032 103.683 11.2449 107.273 10.6947C109.495 10.3074 111.779 10.6218 113.809 11.593C116.653 12.9853 118.303 15.3488 119.095 18.3579C119.491 19.8905 119.609 21.4849 119.451 23.0568L119.445 23.0625ZM102.829 19.4246H114.787C114.402 16.6961 112.124 14.6133 109.354 14.4618C105.877 14.2653 103.423 16.1291 102.829 19.4246Z"
        fill="var(--foreground)"
      />
    </svg>
  );
};

export default Logo;
