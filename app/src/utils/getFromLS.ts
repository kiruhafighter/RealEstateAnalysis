import { calcTotalAmount, calcTotalPrice } from "./calcTotal";

export const getCartFromLS = () => {
  const data = localStorage.getItem('cart');
  const items = data ? JSON.parse(data) : [];
  const totalPrice = calcTotalPrice(items);
  const totalAmount = calcTotalAmount(items);
    return {
      items,
      totalPrice,
      totalAmount,
    };
} ;

export const getOrderFromLS = () => {
  const data = localStorage.getItem('order');
  const orderInfo = data ? JSON.parse(data) : [];
    return {
      orderInfo,
    };
} ;

export const getFormFromLS = () => {
  const data = localStorage.getItem('form');
  const values = data ? JSON.parse(data) : [];
      return {
        values,
      };
};

export const getUserFromLS = () => {
  const email = localStorage.getItem('current_email');
  const json = localStorage.getItem('is_login');
  const isLogin = json ? JSON.parse(json) : false;
      return {
        email,
        isLogin,
      };
}


