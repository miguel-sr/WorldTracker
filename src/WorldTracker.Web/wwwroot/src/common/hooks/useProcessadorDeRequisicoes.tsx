import { AxiosError } from "axios";
import useAppContext from "../context/AppContext";
import Notistack from "@/lib/Notistack";

function useRequestHandler() {
  const { setLoadingState } = useAppContext();

  function showLoading(action: () => Promise<void>) {
    const promise = executeAndGetActionPromise(action);

    promise
      .catch((error: AxiosError) => {
        const message = `Could not complete the request to route [${error.config?.url}].`;
        Notistack.showNotification(message);

        if (
          typeof error.response?.data === "string" &&
          error.response?.data !== ""
        )
          Notistack.showErrorMessage(String(error.response?.data));
      })
      .finally(() => {
        setLoadingState(false);
      });
  }

  function executeAndGetActionPromise(action: () => Promise<void>) {
    try {
      setLoadingState(true);
      const result = action();

      const THEN_METHOD_NAME = "then";
      const TYPE_NAME = "function";

      if (typeof result[THEN_METHOD_NAME] !== TYPE_NAME) {
        return Promise.resolve(result);
      }

      return result;
    } catch (error) {
      return Promise.reject(error);
    }
  }

  return { showLoading };
}

export default useRequestHandler;
