//COMPONENT/CSS/IMG
import LoginForm from "../components/Authentication/Login/LoginForm";
import classes from "./Authentication.module.css";
import imgAuth from "../assets/login.jpg";
import { useAppSelector } from "../redux/hooks/redux-hooks";
import LogOutForm from "../components/Authentication/LogOut/LogOutForm";

function LoginPage() {
  const { isLogin } = useAppSelector((state) => state.user);
  return (
    <div className={classes.box}>
      <div className={classes.imgDiv}>
        <img src={imgAuth} alt="login pic" />
      </div>
      {isLogin ? <LogOutForm /> :  <LoginForm />}
     
    </div>
  );
}
export default LoginPage;
