//FORMIK/CSS
import { FormikProps, Field, ErrorMessage } from "formik";
import classes from "../NewForm/Form.module.css";

type PaymentDateProps = {
  formik: FormikProps<{
    paymentMethod: string;
    deliveryDate: Date | string;
  }>;
};

const PaymentDate: React.FC<PaymentDateProps> = ({ formik }) => {
  return (
    <>
      <label className={classes.label}>Payment Type</label>
      <div className={classes.payment}>
        <label>
          <Field type="radio" name="paymentMethod" value="online" />
          Online
        </label>
        <label>
          <Field type="radio" name="paymentMethod" value="cash" />
          Cash
        </label>
      </div>
      <div>
        <label className={classes.label}>Delivery date</label>
        <Field
          type="date"
          name="deliveryDate"
          placeholder="Delivery Date"
          className={
            formik.errors.deliveryDate && formik.touched.deliveryDate
              ? classes.error
              : classes.adress_inputs
          }
        />
        <ErrorMessage
          name="deliveryDate"
          component="span"
          className={classes.error_message}
        />
      </div>
    </>
  );
};

export default PaymentDate;
