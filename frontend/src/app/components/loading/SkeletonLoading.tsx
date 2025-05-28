const SkeletonLoading = () => {
  const randomTextLength = () => Math.floor(Math.random() * (100 - 60) + 60);

  return (
    <section
      className="flex gap-4 flex-col lg:justify-items-stretch lg:flex-row lg:gap-6 survey-section animate-pulse"
      role="status"
    >
      <span className="sr-only">Laster...</span>
      <div
        className="bg-white dark:bg-slate-800 p-8 flex-[1]"
        aria-hidden="true"
      >
        <div className="sticky top-32 flex flex-col gap-4">
          <div className="flex place-content-between">
            <div className="w-48 h-9 bg-gray-400 dark:bg-gray-50 rounded-full"></div>
            <div className="md:hidden h-5 w-5 bg-gray-400 dark:bg-gray-50 rounded-full"></div>
          </div>
          <div className="hidden md:flex flex-col gap-4">
            {Array(6)
              .fill(0)
              .map((_, i) => (
                <div
                  key={i}
                  className="h-3.5 bg-gray-400 dark:bg-gray-50 rounded-full"
                  style={{ width: `${randomTextLength()}%` }}
                ></div>
              ))}
            <div className="w-full bg-[var(--secondary-cool-blue)] dark:bg-slate-600 p-2 flex items-center gap-3">
              <div className="h-6 w-6 bg-gray-400 dark:bg-gray-50 rounded-full"></div>
              <div className="flex flex-col w-full gap-2">
                {Array(4)
                  .fill(0)
                  .map((_, i) => (
                    <div
                      key={i}
                      className="h-3 bg-gray-400 dark:bg-gray-50 rounded-full"
                      style={{ width: `${randomTextLength()}%` }}
                    ></div>
                  ))}
              </div>
            </div>
          </div>
        </div>
      </div>
      <div
        className="p-6 flex flex-col flex-[2] bg-[var(--primary-colossus)] gap-10"
        aria-hidden="true"
      >
        <div className="flex gap-10 justify-items-center">
          <div className="w-1/2 flex justify-end">
            <div className="h-9 w-40 bg-gray-400 dark:bg-gray-50"></div>
          </div>
          <div className="w-1/2">
            <div className="h-9 w-48 bg-gray-400 dark:bg-gray-50"></div>
          </div>
        </div>
        <div className="flex flex-col gap-3">
          {Array(7)
            .fill(0)
            .map((_, i) => (
              <div key={i} className="flex gap-5">
                <div className="w-1/3 flex justify-end">
                  <div
                    className="h-3.5 bg-gray-400 dark:bg-gray-50 rounded-full"
                    style={{ width: `${randomTextLength()}%` }}
                  ></div>
                </div>
                <div className="w-2/3">
                  <div
                    className="h-5 bg-[var(--contrast-boris-orange)] dark:bg-[#61DAFB]"
                    style={{ width: `${randomTextLength()}%` }}
                  ></div>
                </div>
              </div>
            ))}
        </div>
        <div className="mt-auto ml-auto">
          <div className="h-5 w-40 bg-gray-400 dark:bg-gray-50 rounded-full"></div>
        </div>
      </div>
    </section>
  );
};
export default SkeletonLoading;
