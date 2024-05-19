//REACT
import { Fragment, useEffect } from "react";
//CONTEXT
import OrderItems from "../components/Order/OrderItems";
import { useAppSelector } from "../redux/hooks/redux-hooks"; 
import EmptyOrder from "../components/Empty/EmptyOrder/EmptyOrder";
//CSS
import classes from "./ShoppingCart.module.css";

const Orders: React.FC = () => {
  const { orderInfo } = useAppSelector((state) => state.order);

  useEffect(() => {
    const json = JSON.stringify(orderInfo);
    localStorage.setItem("order", json);
  }, [orderInfo]);

  const formValues = (
    <ul className={classes.cart_item}>
      {orderInfo.map((value) => (
        <OrderItems key={value.orderNum} {...value} />
      ))}
    </ul>
  );
  return (
    <Fragment>
      {!orderInfo.length ? (
        <EmptyOrder />
      ) : (
        <div className={classes.cart}>
          <h1 className={classes.cart_title}>Your Products</h1>
          {formValues}
        </div>
      )}
    </Fragment>
  );
};

export default Orders;
