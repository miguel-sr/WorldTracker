import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";

import Homepage from "../pages/Homepage";
import Login from "../pages/Login";

const Routes = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<Homepage />} />
      <Route path="login" element={<Login />} />
    </Route>
  )
);

export default Routes;
