export default function CountryWithWeatherSkeleton() {
  return (
    <section className="w-full max-w-[600px] h-[207px] rounded-xl shadow-md p-6 bg-gray-100 flex flex-col gap-6 animate-pulse">
      <div className="flex items-center gap-4">
        <div className="w-10 h-6 bg-gray-300 rounded" />
        <div className="h-6 w-32 bg-gray-300 rounded" />
      </div>

      <div className="flex flex-row md:justify-between gap-6">
        <div className="w-full md:w-1/2 space-y-3">
          <div className="h-4 w-24 bg-gray-300 rounded" />
          <div className="h-4 w-32 bg-gray-300 rounded" />
          <div className="h-4 w-20 bg-gray-300 rounded" />
          <div className="h-4 w-24 bg-gray-300 rounded" />
        </div>

        <div className="w-full md:w-1/2 md:pl-5 space-y-3">
          <div className="h-4 w-36 bg-gray-300 rounded" />
          <div className="h-4 w-24 bg-gray-300 rounded" />
          <div className="h-4 w-28 bg-gray-300 rounded" />
          <div className="h-4 w-24 bg-gray-300 rounded" />
        </div>
      </div>
    </section>
  );
}
