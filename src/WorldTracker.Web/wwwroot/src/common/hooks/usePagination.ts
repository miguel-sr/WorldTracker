import { useState } from "react";
import useRequestHandler from "./useRequestHandler";

export function usePagination<T>(
  fetchPage: (
    paginationToken: string | null,
    filter: string | undefined
  ) => Promise<{
    items: T[];
    paginationToken: string | null;
  }>
) {
  const { showLoading } = useRequestHandler();

  const [items, setItems] = useState<T[]>([]);
  const [currentToken, setCurrentToken] = useState<string | null>(null);
  const [previousTokens, setPreviousTokens] = useState<string[]>([]);
  const [nextToken, setNextToken] = useState<string | null>(null);
  const [filter, setFilter] = useState<string | undefined>(undefined);
  const [loading, setLoading] = useState(false);

  async function loadPage(token: string | null, newFilter?: string) {
    setLoading(true);

    showLoading(async () => {
      if (newFilter !== undefined && newFilter !== filter) {
        loadWithNewFilter(newFilter);
      } else {
        loadWithCurrentFilter(token);
      }
    });
  }

  async function loadWithNewFilter(newFilter: string) {
    setFilter(newFilter);
    setPreviousTokens([]);
    setCurrentToken(null);

    const data = await fetchPage(null, newFilter);

    setItems(data.items);
    setNextToken(data.paginationToken ?? null);
  }

  async function loadWithCurrentFilter(token: string | null) {
    const data = await fetchPage(token, filter);

    setItems(data.items);
    setNextToken(data.paginationToken ?? null);
    setCurrentToken(token);
  }

  function goNext() {
    if (!nextToken) return;

    setPreviousTokens((prev) => [...prev, currentToken ?? ""]);

    loadPage(nextToken);
  }

  function goPrev() {
    setPreviousTokens((prev) => {
      if (prev.length === 0) {
        loadPage(null);
        return [];
      }

      const newTokens = [...prev];
      const lastToken = newTokens.pop() ?? null;

      loadPage(lastToken === "" ? null : lastToken);

      return newTokens;
    });
  }

  function setFilterAndReset(newFilter: string) {
    loadPage(null, newFilter);
  }

  return {
    items,
    currentToken,
    nextToken,
    previousTokens,
    loadPage,
    goNext,
    goPrev,
    filter,
    setFilter: setFilterAndReset,
    loading,
  };
}
