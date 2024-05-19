//REACT/ROUTER/REDUX
import { useEffect, useState, useRef } from "react";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../../../redux/hooks/redux-hooks";
//ICONS & COMPONENTS
import { TbTruckDelivery } from "react-icons/tb";
//CSS
import classes from "../MainNavigation/MainNavigation.module.css";

const HeaderOrderButton: React.FC = () => {
  const values = useAppSelector((state) => state.form.values);
  const isMounted = useRef(false);

  useEffect(() => {
    if (isMounted) {
      const json = JSON.stringify(values);
      localStorage.setItem("form", json);
    }
    isMounted.current = true;
  }, [values]);

  const [btnIsHighlighted, setBtnIsHighlighted] = useState(false);
  const btnClasses = `${classes.shopping_btn} ${
    btnIsHighlighted ? classes.bump : ""
  }`;
  useEffect(() => {
    if (values.length === 0) {
      return;
    }
    setBtnIsHighlighted(true);
    const timer = setTimeout(() => {
      setBtnIsHighlighted(false);
    }, 300);
    return () => {
      clearTimeout(timer);
    };
  }, [values.length]);
  return (
    <button className={btnClasses}>
      <NavLink
        to="/orders"
        className={({ isActive }) =>
          isActive ? classes.active : classes.not_active
        }
      >
        <TbTruckDelivery size={28} />
        <p className={classes.nav_text}>Orders</p>
      </NavLink>

      {values.length ? (
        <span className={classes.badge}>{values.length}</span>
      ) : (
        <></>
      )}
    </button>
  );
};

export default HeaderOrderButton;
