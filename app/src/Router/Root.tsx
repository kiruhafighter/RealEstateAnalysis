//ROOT
import classes from "./Root.module.css";
//COMPONENTS
import { Outlet } from "react-router-dom";
import MainNavigation from "../components/NavigationMenu/MainNavigation/MainNavigation";
import Footer from "../components/Footer/Footer";

const RootLayout: React.FC = () => {
  return (
    <div className={classes.container}>
      <div className={classes.header}>
        <MainNavigation />
      </div>
      <div className={classes.content}>
        <Outlet/>
      </div>
      <div className={classes.footer}>
        <Footer />
      </div>
    </div>
  );
}

export default RootLayout;
