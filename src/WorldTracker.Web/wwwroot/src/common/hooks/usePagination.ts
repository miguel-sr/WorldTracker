import { useState } from "react";

export function usePagination<T>(
  fetchPage: (paginationToken: string | null) => Promise<{
    items: T[];
    paginationToken: string | null;
  }>
) {
  const [items, setItems] = useState<T[]>([]);
  const [currentToken, setCurrentToken] = useState<string | null>(null);
  const [previousTokens, setPreviousTokens] = useState<string[]>([]);
  const [nextToken, setNextToken] = useState<string | null>(null);

  const loadPage = async (token: string | null) => {
    const data = await fetchPage(token);
    setItems(data.items);
    setNextToken(data.paginationToken ?? null);
  };

  const goNext = () => {
    if (!nextToken) return;
    setPreviousTokens((prev) => [...prev, currentToken || ""]);
    setCurrentToken(nextToken);
  };

  const goPrev = () => {
    if (previousTokens.length > 0) {
      setPreviousTokens((prev) => {
        const newTokens = [...prev];
        const lastToken = newTokens.pop() || null;
        setCurrentToken(lastToken === "" ? null : lastToken);
        return newTokens;
      });
    } else {
      setCurrentToken(null);
      setPreviousTokens([]);
    }
  };

  return {
    items,
    currentToken,
    nextToken,
    previousTokens,
    loadPage,
    goNext,
    goPrev,
  };
}
