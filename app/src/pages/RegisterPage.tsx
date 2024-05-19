//COMPONENT/CSS/IMG
import RegisterForm from "../components/Authentication/Register/RegisterForm";
import classes from './Authentication.module.css';
import imgAuth from '../assets/login.jpg';
import { useAppSelector } from "../redux/hooks/redux-hooks";
import LogOutForm from "../components/Authentication/LogOut/LogOutForm";

function RegisterPage() {
  const { isLogin } = useAppSelector((state) => state.user);
  return <div className={classes.box}>  
<div className={classes.imgDiv}>
  <img src={imgAuth} alt="login pic"/>
</div >
{isLogin ? <LogOutForm /> : <RegisterForm />}
  </div>;
}

export default RegisterPage;

