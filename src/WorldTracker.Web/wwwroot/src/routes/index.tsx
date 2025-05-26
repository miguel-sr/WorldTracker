import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import { ProtectedRoute } from "./ProtectedRoute";

import Homepage from "../pages/Homepage";
import Login from "../pages/Login";

const Routes = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<Homepage />} />
      <Route element={<ProtectedRoute />}></Route>
      <Route path="login" element={<Login />} />
    </Route>
  )
);

export default Routes;
