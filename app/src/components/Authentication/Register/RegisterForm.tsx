//REACT&&ROUTER&&REDUX&&FIREBASE
import { Link, useNavigate } from "react-router-dom";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { useState } from "react";
import * as Yup from "yup";
import { getAuth, createUserWithEmailAndPassword } from "firebase/auth";
//CSS
import classes from "../Login/AuthForm.module.css";

interface FormValues {
  email: string;
  password: string;
  confirmPassword: string;
}

const RegisterForm: React.FC = () => {
  const InitialValues: FormValues = {
    email: "",
    password: "",
    confirmPassword: "",
  };

  const validationSchema = Yup.object({
    email: Yup.string().email("Invalid email address").required("Required"),
    password: Yup.string()
      .required("Required")
      .min(5, "Password is too short - should be 5 chars minimum"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password"), null], "Passwords must match")
      .required("Required"),
  });
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const Register = (values, { resetForm }: { resetForm: () => void }) => {
    const auth = getAuth();
    createUserWithEmailAndPassword(auth, values.email, values.password)
      .then(() => {
        setError("");
        resetForm();
        navigate("/login");
      })
      .catch((error) => {
        let errorMessage: string;
        switch (error.code) {
          case "auth/email-already-in-use":
            errorMessage =
              "The user is already in use. Please go to the page - login.";
            break;
          case "auth/missing-email":
            errorMessage = "The email is missing. Please fill it.";
            break;
          default:
            errorMessage = "An error occurred. Please try again later.";
            break;
        }
        setError(errorMessage);
      });
  };

  const handleSubmit = (values, { resetForm }: { resetForm: () => void }) => {
    Register(values, { resetForm });
  };

  return (
    <Formik
      initialValues={InitialValues}
      validationSchema={validationSchema}
      onSubmit={handleSubmit}
    >
      {({ errors, touched, isValid, dirty }) => (
        <Form className={classes.form}>
          <div className={classes.header}>
            <h1>Register</h1>
            <p>Create a new account</p>
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
            />
            <ErrorMessage
              name="password"
              component="span"
              className={classes.error_message}
            />
          </p>
          <p className={classes.input}>
            <label htmlFor="confirm-password">Confirm Password</label>
            <Field
              id="confirmPassword"
              type="password"
              name="confirmPassword"
              className={
                errors.confirmPassword && touched.confirmPassword
                  ? classes.error
                  : null
              }
              placeholder="Confirm Password"
            />
            <ErrorMessage
              name="confirmPassword"
              component="span"
              className={classes.error_message}
            />
          </p>

          <div className={classes.actions}>
            <button type="submit" disabled={!(dirty && isValid)}>
              Register
            </button>
            {error && <p className={classes.error_authmessage}>{error}</p>}
            <Link to="/login">Have you already had an acount? Then Login!</Link>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default RegisterForm;
