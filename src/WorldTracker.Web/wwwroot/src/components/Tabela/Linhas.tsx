export default function Linhas({
  colunas,
  linhas,
}: {
  colunas: string[];
  linhas: Record<string, any>[];
}) {
  return (
    <tbody>
      {linhas.map((linha, index) => (
        <tr key={index}>
          {colunas.map((coluna, index) => (
            <td key={index} className="py-3 px-4 border-b text-zinc-600">
              {linha[coluna]}
            </td>
          ))}
        </tr>
      ))}
    </tbody>
  );
}
