import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";

import Register from "@/pages/Register";
import Homepage from "../pages/Homepage";
import Login from "../pages/Login";

const Routes = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<Homepage />} />
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
    </Route>
  )
);

export default Routes;
