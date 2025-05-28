import { useEffect, useState } from "react";

interface CountryFilterProps {
  filter?: string;
  setFilter: (value: string) => void;
}

export default function CountryFilter({
  filter,
  setFilter,
}: CountryFilterProps) {
  const [internalFilter, setInternalFilter] = useState(filter || "");

  useEffect(() => {
    setInternalFilter(filter || "");
  }, [filter]);

  function onSubmit(e: React.FormEvent) {
    e.preventDefault();
    setFilter(internalFilter);
  }

  return (
    <section className="mb-10 mx-auto p-6 bg-sky-blue">
      <h2 className="text-3xl font-semibold mb-4 text-center text-white">
        Busque por seu país
      </h2>
      <form
        className="max-w-md mx-auto flex items-center gap-2 border border-gray-300 rounded-lg bg-white shadow-sm focus-within:ring-2 focus-within:ring-sky-500 px-4 py-2 transition-shadow"
        role="search"
        onSubmit={onSubmit}
      >
        <input
          type="text"
          name="city"
          value={internalFilter}
          onChange={(e) => setInternalFilter(e.target.value)}
          placeholder="Digite o nome do país"
          className="flex-grow min-w-0 text-gray-700 placeholder-gray-400 bg-transparent focus:outline-none"
        />
        <button
          type="submit"
          className="bg-sky-600 hover:bg-sky-700 text-white font-medium rounded-lg px-4 py-2 transition-colors"
          aria-label="Buscar clima"
        >
          Buscar
        </button>
      </form>
    </section>
  );
}
