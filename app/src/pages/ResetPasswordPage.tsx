import ResetPassForm from "../components/Authentication/ResetPass/ResetPass";
import classes from './Authentication.module.css';
import imgAuth from '../assets/login.jpg';

function ResetPasswordPage() {
  return <div className={classes.box}>
<div className={classes.imgDiv}>
  <img src={imgAuth} alt="login pic"/>
</div >
<ResetPassForm />
  </div>;
}
export default ResetPasswordPage;