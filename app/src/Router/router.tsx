//REACT-ROUTER
import { createBrowserRouter} from 'react-router-dom';
//PAGES
import HomePage from '../pages/Home';
import ShoppingCart from '../pages/ShoppingCart';
import RootLayout from './Root';
import ErrorPage from '../pages/Error';
import Orders from '../pages/Orders';
import BookDetail from '../pages/BookDetail';
import OrderDetails from '../pages/OrderDetails';
import RegisterPage from '../pages/RegisterPage';
import LoginPage from '../pages/LoginPage';
import ResetPasswordPage from '../pages/ResetPasswordPage';


 const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      { path: "/", element: <HomePage /> },
      {
        path: "/book",
        element: <BookDetail />,
      },
      { path: "shopping-cart", element: <ShoppingCart /> },
      { path: "/shopping-cart/order-details", element: <OrderDetails /> },
      { path: "orders", element: <Orders /> },
      { path: "login", element: <LoginPage />},
      { path: "/login/reset-pass", element: <ResetPasswordPage />},
      { path: "signup", element: <RegisterPage />},
      { path: "*", element: <ErrorPage />},
    ],
  },
]);

export default router;