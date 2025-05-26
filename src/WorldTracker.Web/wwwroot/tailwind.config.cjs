import plugin from "tailwindcss/plugin";

/** @type {import('tailwindcss').Config} */
module.exports = {
  important: true,
  content: ["./src/**/*.tsx", "./index.html"],
  plugins: [
    plugin(function ({ addBase, theme }) {
      addBase({
        h1: {
          fontSize: theme("fontSize.4xl"),
        },
        h2: {
          fontSize: theme("fontSize.3xl"),
        },
        h3: {
          fontSize: theme("fontSize.2xl"),
        },
        h4: {
          fontSize: theme("fontSize.xl"),
        },
        h5: {
          fontSize: theme("fontSize.lg"),
        },
        h6: {
          fontSize: theme("fontSize.md"),
        },
      });
    }),
  ],
  theme: {
    extend: {
      colors: {
        "primary-blue": "#006494", // Strong blue for header/footer or call to action
        "sky-blue": "#1B98E0", // Lighter blue for accents or highlights
        aqua: "#22D3EE", // Fresh cyan, good for buttons/links
        "deep-navy": "#003554", // Very dark blue for background or text
        "light-gray": "#F5F7FA", // Neutral for borders or secondary text E5E7EB
        "dark-gray": "#374151", // Darker gray for headings or content
        "soft-red": "#F87171", // Alerts or errors
        "warm-yellow": "#FBBF24", // Highlights or warnings
        "cool-green": "#34D399",
      },
      fontFamily: {
        agency: "Agency",
        poppins: "Poppins",
      },
      minHeight: {
        "full-screen": "calc(100vh - 220px)",
      },
      height: {
        "full-screen": "calc(100vh - 220px)",
      },
      dropShadow: {
        black: "0 5px 5px rgba(0, 0, 0, 0.2)",
      },
    },
  },
};
