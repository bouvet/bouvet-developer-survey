export const fetcher = async ({
  url,
  accessToken,
  allowFetch = true,
}: {
  url: string | null;
  allowFetch?: boolean;
  accessToken?: string;
}) => {
  if (url && accessToken && allowFetch) {
    try {
      const res = await fetch(url, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });

      if (!res.ok) {
        throw new Error(`API Error: ${res.statusText}`);
      }

      return await res.json();
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.error(new Date(), "Error in fetcher:", error.message);
        return { error: error.message };
      }
      return { error: "Failed to fetch data" };
    }
  }
};
