export const fetcher = async (url: string) => {
  try {
    const res = await fetch(url);
    if (!res.ok) {
      throw new Error('API Error: ' + res.statusText);
    }
    return await res.json();
  } catch (error) {
    // Handle the error gracefully without logging to the console
    return { error: 'Failed to fetch data' };
  }
};