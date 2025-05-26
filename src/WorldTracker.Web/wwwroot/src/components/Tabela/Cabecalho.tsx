export default function Cabecalho({ colunas }: { colunas: string[] }) {
  return (
    <thead>
      <tr className="bg-zinc-200 text-left text-zinc-700">
        {colunas.map((coluna, index) => (
          <th key={index} className="py-2 px-4 border-b">
            {coluna}
          </th>
        ))}
      </tr>
    </thead>
  );
}
