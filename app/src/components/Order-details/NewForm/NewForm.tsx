//REACT-REDUX
import React from "react";
import { useNavigate, Link } from "react-router-dom";
import { Formik, Form } from "formik";
import { useAppDispatch, useAppSelector } from "../../../redux/hooks/redux-hooks";
import { cleanItems } from "../../../redux/slices/cartSlice";
import { submitForm } from "../../../redux/slices/formSlice";
import { createOrder } from "../../../redux/slices/orderSlice";
//COMPONENTS
import ContactDetails from "../ContactDetails/ContactDetails";
import Address from "../Address/Adress";
import PaymentDate from "../Payment/PaymentDate";
import ObservationRecommend from "../Observation/ObservationRecommend";
//INTERFACE&&ERRORS
import { FormValues } from "../Stuff/StuffForForm";
import { validate } from "../Stuff/StuffForForm";
import classes from "./Form.module.css";
import { getOrderFromLS } from "../../../utils/getFromLS";

const initialValues: FormValues = {
  firstName: "",
  lastName: "",
  billingCountry: "",
  billingAddress: "",
  billingPhoneNumber: "",
  checkAdress: false,
  deliveryCountry: "",
  deliveryAddress: "",
  deliveryPhoneNumber: "",
  paymentMethod: "online",
  deliveryDate: "",
  observations: "",
  checkRecommend: false,
};

const { orderInfo } = getOrderFromLS();

const NewForm: React.FC = () => {
  const dispatch = useAppDispatch();
  const { totalAmount, totalPrice } = useAppSelector((state) => state.cart);
  const { email } = useAppSelector((state) => state.user);
  let navigate = useNavigate();

  const SubmitOrder = (values) => {
    dispatch(submitForm(values));
  };

  const CreateOrder = (totalAmount: number, totalPrice: number) => {
    let total = {
      totalAmount,
      totalPrice,
    };
    dispatch(createOrder(total));
  };

  const CleanItems = () => {
    dispatch(cleanItems());
  };

  const HandleSubmit = async(values, { resetForm }: { resetForm: () => void }) => {
    SubmitOrder(values);
    CreateOrder(totalAmount, totalPrice);
    CleanItems();
    const url = `https://itp-labrary-auth-default-rtdb.firebaseio.com/UserData.json`;
    console.log(url);
    const res = await fetch(
      url,
      {
        method: "POST",
        headers: {
          "Content-Type": "aplication/json",
        },
        body: JSON.stringify({...values, email, totalAmount, totalPrice })
      }
    );
    navigate("/orders");
    resetForm();
    values = initialValues;
  };

  return (
    <Formik
      initialValues={initialValues}
      validate={validate}
      onSubmit={HandleSubmit}
    >
      {(formik) => (
        <Form>
          <ContactDetails formik={formik} />
          <Address formik={formik} />
          <PaymentDate formik={formik} />
          <ObservationRecommend formik={formik} />

          <div className={classes.buttons}>
            <Link to="/shopping-cart">
              <button type="reset" className={classes.cancel}>
                Cancel order
              </button>
            </Link>
            <button type="submit" className={classes.submit}>
              Place order
            </button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default NewForm;
