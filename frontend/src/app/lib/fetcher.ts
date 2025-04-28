export const fetcher = async (url: string | null, accessToken?: string) => {
  if (url && accessToken) {
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
        console.error("Error in fetcher:", error.message);
        return { error: error.message };
      }
      return { error: "Failed to fetch data" };
    }
  }
};
