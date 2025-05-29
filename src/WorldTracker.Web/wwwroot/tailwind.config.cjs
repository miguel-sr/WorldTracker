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
        "primary-blue": "#1B98E0",
        "dark-gray": "#374151",
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
