//REACT&&ROUTER&&REDUX&&FIREBASE
import { Link, useNavigate } from "react-router-dom";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { useState } from "react";
import "firebase/auth";
import { getAuth, sendPasswordResetEmail } from "firebase/auth";
//CSS
import classes from "../Login/AuthForm.module.css";
import * as Yup from "yup";

interface FormValues {
  email: string;
}

const ResetPassForm: React.FC = () => {
  const navigate = useNavigate();

  const [error, setError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const InitialValues: FormValues = {
    email: "",
  };

  const validationSchema = Yup.object({
    email: Yup.string().email("Invalid email address").required("Required"),
  });

  const ResetPass = async (values: FormValues) => {
    const auth = getAuth();
    sendPasswordResetEmail(auth, values.email)
      .then(() => {
        setSuccessMessage(
          "Password reset email sent successfully! Please check your email!"
        );
        console.log("Password reset email sent successfully");
        setError("");
        const timer = setTimeout(() => {
          navigate("/login");
        }, 5000);
        return () => clearTimeout(timer);
      })
      .catch((error) => {
        console.log(error.message);
        let errorMessage: string;
        switch (error.code) {
          case "auth/user-not-found":
            errorMessage =
              "The user is not found. Please go to the page - register.";
            break;
          default:
            errorMessage = "An error occurred. Please try again later.";
            break;
        }
        setError(errorMessage);
      });
  };

  return (
    <Formik
      initialValues={InitialValues}
      validationSchema={validationSchema}
      onSubmit={ResetPass}
    >
      {({ errors, touched }) => (
        <Form className={classes.form}>
          <div className={classes.header}>
            <h1>Forgot your password?</h1>
            <p>Use your email to reset the password.</p>
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

          <div className={classes.actions}>
            {successMessage && (
              <p className={classes.success}>{successMessage}</p>
            )}
            <button type="submit">Reset Password</button>
            {error ? <p className={classes.error_authmessage}>{error}</p> : ""}
            <Link to="/login">Return to the login page</Link>
            <Link to="/signup">Register as new user</Link>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default ResetPassForm;
