//REACT-ROUTER/REDUX
import { Fragment, useState, useEffect, useRef } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { useAppDispatch } from "../../../redux/hooks/redux-hooks";
import { removeUser } from "../../../redux/slices/userSlice";
//CSS
import classes from "../MainNavigation/MainNavigation.module.css";
//ICONS
import { CgProfile } from "react-icons/cg";
import { TfiHandPointLeft } from "react-icons/tfi";

interface HeaderLoginButtonProps {
  isLogin: boolean;
  email: string;
}

const HeaderLoginButton: React.FC<HeaderLoginButtonProps> = ({
  isLogin,
  email,
}) => {
  const isMounted = useRef(false);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [showLogOut, setShowLogOut] = useState(false);
  const logoutHandler = () => {
    dispatch(removeUser());
    navigate("/");
    setShowLogOut(false);
  };
  const handleMouseEnter = () => {
    setShowLogOut(true);
  };

  const handleMouseLeave = () => {
    setShowLogOut(false);
  };

  useEffect(() => {
    if (isMounted.current) {
      const json = JSON.stringify(isLogin);
      localStorage.setItem("current_email", email);
      localStorage.setItem("is_login", json);
    }
    isMounted.current = true;
  }, [isLogin, email]);
  return (
    <Fragment>
      {isLogin ? (
        <span
          className={showLogOut ? classes.log : ""}
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeave}
        >
          <div className={classes.not_active}>
            <CgProfile size={28} />
            <p className={classes.nav_text}>{isLogin ? email : "LOGIN"}</p>
          </div>
          {showLogOut && (
            <div className={classes.logout}>
              <p>Are you sure you want to log out?</p>
              <div className={classes.btn_logout}>
                <button onClick={logoutHandler}>Log Out</button>
                <TfiHandPointLeft size={24} />
              </div>
            </div>
          )}
        </span>
      ) : (
        <NavLink
          to="/signup"
          className={({ isActive }) =>
            isActive ? classes.active : classes.not_active
          }
        >
          <CgProfile size={28} />
          <p className={classes.nav_text}>LOGIN</p>
        </NavLink>
      )}
    </Fragment>
  );
};

export default HeaderLoginButton;
