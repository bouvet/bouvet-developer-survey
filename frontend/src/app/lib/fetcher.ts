export const fetcher = async (url: string) => {
    const res = await fetch(url)
    if (!res.ok) {
      throw new Error('API Error: ' + res.statusText)
    }
    return res.json()
  }