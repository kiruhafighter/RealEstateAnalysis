//FORMIK
import { FormikProps, Field, ErrorMessage } from "formik";
import CountrySelect from "../CountrySelect/CountrySelect";
//CSS
import classes from "../NewForm/Form.module.css";
//TYPE
type AddressProps = {
  formik: FormikProps<{
    billingCountry: string;
    billingAddress: string;
    billingPhoneNumber: string;
    checkAdress: boolean;
    deliveryCountry: string;
    deliveryAddress: string;
    deliveryPhoneNumber: string;
  }>;
};

const Address: React.FC<AddressProps> = ({ formik }) => {
  const handleCheckboxChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    setFieldValue: Function
  ) => {
    const { checked } = e.target;
    formik.values.checkAdress = !formik.values.checkAdress;
    if (checked) {
      setFieldValue("deliveryCountry", formik.values.billingCountry);
      setFieldValue("deliveryAddress", formik.values.billingAddress);
      setFieldValue("deliveryPhoneNumber", formik.values.billingPhoneNumber);
    } else {
      setFieldValue("deliveryCountry", "");
      setFieldValue("deliveryAddress", "");
      setFieldValue("deliveryPhoneNumber", "");
    }
  };
  return (
    <>
      <div className={classes.address}>
        <label className={classes.label}>Billing address</label>
        <CountrySelect name="billingCountry" formik={formik} />
        <Field
          id="billingAddress"
          name="billingAddress"
          placeholder="Address"
          className={
            formik.errors.billingAddress && formik.touched.billingAddress
              ? classes.error
              : classes.adress_inputs
          }
        />
        <ErrorMessage
          name="billingAddress"
          component="span"
          className={classes.error_message}
        />
        <Field
          id="billingPhoneNumber"
          type="phone"
          name="billingPhoneNumber"
          placeholder="Phone Number"
          className={
            formik.errors.billingPhoneNumber &&
            formik.touched.billingPhoneNumber
              ? classes.error
              : classes.adress_inputs
          }
        />
        <ErrorMessage
          name="billingPhoneNumber"
          component="span"
          className={classes.error_message}
        />
        <label className={classes.check}>
          <Field
            name="checkAdress"
            checked={formik.values.checkAdress}
            type="checkbox"
            {...formik.getFieldProps("checkAdress")}
            className={classes.check_input}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleCheckboxChange(e, formik.setFieldValue)
            }
          />
          Use adress for delivery
        </label>
      </div>

      {!formik.values.checkAdress && (
        <div className={classes.address}>
          <label className={classes.label}>Delivery address</label>
          <CountrySelect name="deliveryCountry" formik={formik} />
          <Field
            id="deliveryAddress"
            name="deliveryAddress"
            placeholder="Address"
            className={
              formik.errors.deliveryAddress && formik.touched.deliveryAddress
                ? classes.error
                : classes.adress_inputs
            }
          />
          <ErrorMessage
            name="deliveryAddress"
            component="span"
            className={classes.error_message}
          />
          <Field
            id="deliveryPhoneNumber"
            name="deliveryPhoneNumber"
            placeholder="Phone Number"
            className={
              formik.errors.deliveryPhoneNumber &&
              formik.touched.deliveryPhoneNumber
                ? classes.error
                : classes.adress_inputs
            }
          />
          <ErrorMessage
            name="deliveryPhoneNumber"
            component="span"
            className={classes.error_message}
          />
        </div>
      )}
    </>
  );
};

export default Address;
