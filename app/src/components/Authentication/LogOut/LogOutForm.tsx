//REACT&&ROUTER
import { Form, useNavigate } from "react-router-dom";
//ICONS
import { ImPointDown } from "react-icons/im";
import { RiChatSmile2Line } from "react-icons/ri";
//CSS
import classes from "../Login/AuthForm.module.css";
//REDUX
import { useAppDispatch, useAppSelector } from "../../../redux/hooks/redux-hooks";
import { removeUser } from "../../../redux/slices/userSlice";

const LogOutForm = () => {
  const dispatch = useAppDispatch();
  const { email } = useAppSelector((state) => state.user);
  const navigate = useNavigate();
  const handleLogOut = () => {
    dispatch(removeUser());
    navigate("/");
  };
  return (
    <Form className={classes.form}>
      <div className={classes.header}>
        <h1>
          Welcome,{" "}
          <span className={classes.email}>
            {email} <RiChatSmile2Line size={24} />
          </span>
        </h1>

        <p>You are currently logged-in.</p>
        <p>
          If you want to log out - push the button below.
          <ImPointDown />
        </p>
      </div>
      <div className={classes.actions}>
        <button onClick={handleLogOut}>Log Out</button>
      </div>
    </Form>
  );
};

export default LogOutForm;                                                        