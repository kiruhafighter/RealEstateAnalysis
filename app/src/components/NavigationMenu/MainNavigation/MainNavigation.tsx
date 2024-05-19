//REACT-ROUTER/REDUX
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../../../redux/hooks/redux-hooks";
//COMPONENTS
import HeaderCartButton from "../Cart/HeaderCartButton";
import HeaderOrderButton from "../Order/HeaderOrderButton";
import HeaderLoginButton from "../Login/HeaderLoginButton";
//CSS
import classes from "../MainNavigation/MainNavigation.module.css";
//ICONS
import { GrHomeRounded } from "react-icons/gr";
import { BsXDiamond } from "react-icons/bs";

const MainNavigation: React.FC = () => {
  const { isLogin, email } = useAppSelector((state) => state.user);

  const menuItems = (
    <ul className={classes.fRight}>
      <li>
        <NavLink
          to="/"
          className={({ isActive }) =>
            isActive ? classes.active : classes.not_active
          }
        >
          <GrHomeRounded size={22} />
          <p className={classes.nav_text}>Home</p>
        </NavLink>
      </li>
      <li>
        <HeaderCartButton />
      </li>
      {isLogin && (
        <li>
          <HeaderOrderButton />
        </li>
      )}
      <li>
        <HeaderLoginButton isLogin={isLogin} email={email} />
      </li>
    </ul>
  );

  return (
    <header>
      <nav>
        <NavLink to="/" className={classes.logo}>
          <label>
            <BsXDiamond />
            <span className={classes.nav_title}>Shelf Library</span>
          </label>
        </NavLink>
        {menuItems}
      </nav>
    </header>
  );
};

export default MainNavigation;
