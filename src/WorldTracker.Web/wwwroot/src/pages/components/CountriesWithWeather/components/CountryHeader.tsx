export function CountryHeader({
  name,
  code,
  flag,
}: {
  name: string;
  code: string;
  flag: { url: string; altText: string };
}) {
  return (
    <div className="flex items-center justify-between">
      <h3
        className="text-2xl font-semibold max-w-[85%] truncate cursor-help"
        title={`${name}, ${code}`}
      >
        {name}, {code}
      </h3>
      <img
        src={flag.url}
        alt={flag.altText || `Bandeira de ${name}`}
        className="h-[35px] object-cover rounded-sm border border-gray-300"
      />
    </div>
  );
}
