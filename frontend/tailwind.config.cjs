/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: "class",
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Custom Lavender palette - Primary color
        lavender: {
          50: '#F8F5FD',
          100: '#F3EBFB',
          200: '#E8DFE6',
          300: '#D9C8E6',
          400: '#C9B4E6',
          500: '#B897E8',
          600: '#A78BCC',
          700: '#8E68B2',
          800: '#6D4E8C',
          900: '#5A3D7A',
          950: '#42284D',
        },
        // Custom Seafoam palette - Secondary color
        seafoam: {
          50: '#F0FDFB',
          100: '#E0F5F2',
          200: '#CCEDE9',
          300: '#B3E2DD',
          400: '#8ECEC5',
          500: '#7ECCC5',
          600: '#6DB5AC',
          700: '#57938A',
          800: '#46756D',
          900: '#386259',
          950: '#1F3B35',
        },
      },
    },
  },
  plugins: [],
};
