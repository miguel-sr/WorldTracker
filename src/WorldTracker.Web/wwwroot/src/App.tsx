import "./styles/global.css";

import Routes from "./routes";
import { RouterProvider } from "react-router-dom";

import Loading from "./components/Loading";
import { SnackbarProvider, closeSnackbar } from "notistack";
import { IoIosClose } from "react-icons/io";
import useAppContext from "./common/context/AppContext";

function App() {
  const { loadingState } = useAppContext();

  return (
    <SnackbarProvider
      preventDuplicate
      action={(key) => (
        <button onClick={() => closeSnackbar(key)}>
          <IoIosClose size={36} />
        </button>
      )}
    >
      <RouterProvider router={Routes} />
      {loadingState && <Loading />}
    </SnackbarProvider>
  );
}

export default App;
