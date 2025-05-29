import { useCallback } from "react";
import { AxiosError } from "axios";
import useAppContext from "../context/AppContext";
import Notistack from "@/lib/Notistack";

interface ApiErrorResponse {
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
  requestId: string;
  traceId: string;
}

function useRequestHandler() {
  const { setLoadingState } = useAppContext();

  const showLoading = useCallback(
    (action: () => Promise<void>) => {
      const promise = executeAndGetActionPromise(action);

      promise
        .catch((error: AxiosError) => {
          const data = error.response?.data as ApiErrorResponse | undefined;

          if (data?.detail) {
            Notistack.showErrorMessage(data.detail);
          } else {
            Notistack.showNotification(
              `Could not complete the request to route [${error.config?.url}].`
            );
          }
        })
        .finally(() => {
          setLoadingState(false);
        });
    },
    [setLoadingState]
  );

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
