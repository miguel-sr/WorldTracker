import { closeSnackbar, enqueueSnackbar } from "notistack";
import { IoIosCheckmark, IoIosClose } from "react-icons/io";

const Notistack = {
  showNotification(message: string) {
    enqueueSnackbar(message);
  },

  showSuccessMessage(message: string, onClose?: () => void) {
    enqueueSnackbar(message, { variant: "success", onClose });
  },

  showConfirmationMessage(
    message: string,
    onConfirm: () => void,
    onClose: () => void
  ) {
    enqueueSnackbar(message, {
      variant: "info",
      persist: true,
      onClose,
      action: (key) => (
        <>
          <button
            onClick={() => {
              onConfirm();
              closeSnackbar(key);
            }}
          >
            <IoIosCheckmark size={36} />
          </button>
          <button onClick={() => closeSnackbar(key)}>
            <IoIosClose size={36} />
          </button>
        </>
      ),
    });
  },

  showErrorMessage(message: string, onClose?: () => void) {
    enqueueSnackbar(message, { variant: "error", onClose });
  },
};

export default Notistack;
