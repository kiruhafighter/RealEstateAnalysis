//REACT-REDUX
import { useLocation } from "react-router-dom";
import React from "react";
import { Link } from "react-router-dom";
import { Formik, Form } from "formik";
import { useAppSelector } from "../../../redux/hooks/redux-hooks";
//COMPONENTS
import ContactDetails from "../ContactDetails/ContactDetails";
import Address from "../Address/Adress";
import PaymentDate from "../Payment/PaymentDate";
import ObservationRecommend from "../Observation/ObservationRecommend";
import { scrollUp } from "../../Books/BookComponent/BookComponent";
//INTERFACE&&ERRORS
import { FormValues } from "../Stuff/StuffForForm";
import { validate } from "../Stuff/StuffForForm";
import classes from "../NewForm/Form.module.css";
import ConfirmMessage from "../ConfirmMessage/ConfirmMessage";

const EditForm: React.FC = () => {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const orderId = searchParams.get("id");
  const orderIdNumber = parseInt(orderId, 10);
  const oldValues = useAppSelector((state) => state.form.values);
  const orders = oldValues[orderIdNumber - 1];

  const [showModal, setShowModal] = React.useState(false);

  const initialValues: FormValues = {
    firstName: orders.firstName,
    lastName: orders.lastName,
    billingCountry: orders.billingCountry,
    billingAddress: orders.billingAddress,
    billingPhoneNumber: orders.billingPhoneNumber,
    checkAdress: orders.checkAdress,
    deliveryCountry: orders.deliveryCountry,
    deliveryAddress: orders.deliveryAddress,
    deliveryPhoneNumber: orders.billingPhoneNumber,
    paymentMethod: orders.paymentMethod,
    deliveryDate: orders.deliveryDate,
    observations: orders.observations,
    checkRecommend: orders.checkRecommend,
  };
  const [updatedValues, setUpdatedValues] = React.useState(initialValues);

  const HandleSubmit = (values, { resetForm }: { resetForm: () => void }) => {
    setUpdatedValues(values);
    setShowModal(true);
    scrollUp();
    resetForm();
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
            <Link to="/orders">
              <button type="reset" className={classes.cancel}>
                Cancel{" "}
              </button>
            </Link>
            <button type="submit" className={classes.submit}>
              Update Order
            </button>
          </div>

          {showModal ? (
            <ConfirmMessage
              setState={setShowModal}
              values={updatedValues}
              orderIdNumber={orderIdNumber}
            />
          ) : (
            ""
          )}
        </Form>
      )}
    </Formik>
  );
};

export default EditForm;
