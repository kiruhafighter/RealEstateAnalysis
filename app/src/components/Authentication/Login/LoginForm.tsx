//REACT&&ROUTER&&REDUX&&FIREBASE
import { Link, useNavigate,useSearchParams } from "react-router-dom";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { useState } from "react";
import { useAppDispatch } from "../../../redux/hooks/redux-hooks";
import { setUser } from "../../../redux/slices/userSlice";
import { getAuth, signInWithEmailAndPassword } from "firebase/auth";
//CSS
import classes from "./AuthForm.module.css";
import * as Yup from "yup";

interface FormValues {
  email: string;
  password: string;
}

const LoginForm: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const isOrder = searchParams.get("mode") === "order";

  const [error, setError] = useState("");
  const [rememberMe, setRememberMe] = useState(false);

  const InitialValues: FormValues = {
    email: localStorage.getItem("email") ?? "",
    password: localStorage.getItem("password") ?? "",
  };

  const validationSchema = Yup.object({
    email: Yup.string().email("Invalid email address").required("Required"),
    password: Yup.string()
      .required("Required")
      .min(5, "Password is too short - should be 4 chars minimum"),
  });

  const Login = async (values: FormValues, { resetForm }) => {
    try {
      console.log(values);
      const auth = getAuth();
      const userCredential = await signInWithEmailAndPassword(
        auth,
        values.email,
        values.password
      );
      const { user } = userCredential;
      dispatch(
        setUser({
          email: user.email,
          token: user.refreshToken,
          id: user.uid,
        })
      );
      setError("");
      resetForm();
      if (rememberMe) {
        localStorage.setItem("email", values.email);
        localStorage.setItem("password", values.password);
      }
      if(isOrder){
        navigate("/shopping-cart/order-details?mode=form");
      }else{
        navigate("/");
      }
    } catch (error) {
      console.log(error.message);
      let errorMessage: string;
      switch (error.code) {
        case "auth/user-not-found":
          errorMessage =
            "The user is not found. Please go to the page - register.";
          break;
        case "auth/wrong-password":
          errorMessage = "The entered password is incorrect. Please check it.";
          break;
        default:
          errorMessage = "An error occurred. Please try again later.";
          break;
      }
      setError(errorMessage);
    }
  };

  return (
    <Formik
      initialValues={InitialValues}
      validationSchema={validationSchema}
      onSubmit={Login}
    >
      {({ errors, touched }) => (
        <Form className={classes.form}>
          <div className={classes.header}>
            <h1>Log in</h1>
            <p>Use a local account to log in.</p>
          </div>

          <p className={classes.input}>
            <label htmlFor="email">Email</label>
            <Field
              id="email"
              type="email"
              name="email"
              className={errors.email && touched.email ? classes.error : null}
              placeholder="Email"
            />
            <ErrorMessage
              name="email"
              component="span"
              className={classes.error_message}
            />
          </p>
          <p className={classes.input}>
            <label htmlFor="password">Password</label>
            <Field
              id="password"
              type="password"
              name="password"
              className={
                errors.password && touched.password ? classes.error : null
              }
              placeholder="Password"
              autoComplete="current-password"
            />
            <ErrorMessage
              name="password"
              component="span"
              className={classes.error_message}
            />
          </p>
          <p className={classes.check}>
            <input
              type="checkbox"
              checked={rememberMe}
              onChange={(e) => setRememberMe(e.target.checked)}
            />
            Remember me?
          </p>

          <div className={classes.actions}>
            <button type="submit">Log in</button>
            {error && <p className={classes.error_authmessage}>{error}</p>}
            <Link to="/login/reset-pass">Forgot your password?</Link>
            <Link to="/signup">Register as new user</Link>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default LoginForm;
