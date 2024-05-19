//REACT/ROUTER/REDUX
import { useEffect, useState, useRef } from "react";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../../../redux/hooks/redux-hooks";
//ICONS & COMPONENTS
import { CiShoppingCart } from "react-icons/ci";
//CSS
import classes from "../MainNavigation/MainNavigation.module.css";

const HeaderCartButton: React.FC = () => {
  const { totalAmount, items } = useAppSelector((state) => state.cart);
  const isMounted = useRef(false);

  useEffect(() => {
    if (isMounted.current) {
      const json = JSON.stringify(items);
      localStorage.setItem("cart", json);
    }
    isMounted.current = true;
  }, [items]);

  const [btnIsHighlighted, setBtnIsHighlighted] = useState(false);
  const btnClasses = `${classes.shopping_btn} ${
    btnIsHighlighted ? classes.bump : ""
  }`;
  useEffect(() => {
    if (items.length === 0) {
      return;
    }
    setBtnIsHighlighted(true);
    const timer = setTimeout(() => {
      setBtnIsHighlighted(false);
    }, 300);
    return () => {
      clearTimeout(timer);
    };
  }, [items]);
  return (
    <button className={btnClasses}>
      <NavLink
        to="/shopping-cart"
        className={({ isActive }) =>
          isActive ? classes.active : classes.not_active
        }
      >
        <CiShoppingCart size={28} />
        <p className={classes.nav_text}>Shopping Cart</p>
      </NavLink>
      {items.length ? (
        <span className={classes.badge}>{totalAmount}</span>
      ) : (
        <></>
      )}
    </button>
  );
};

export default HeaderCartButton;
