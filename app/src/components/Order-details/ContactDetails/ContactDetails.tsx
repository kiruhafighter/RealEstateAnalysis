//FORMIK
import { FormikProps, Field, ErrorMessage } from "formik";
//CSS
import classes from "../NewForm/Form.module.css";

//TYPE
type ContactDetailsProps = {
  formik: FormikProps<{
    firstName: string;
    lastName: string;
  }>;
};

const ContactDetails: React.FC<ContactDetailsProps> = ({ formik }) => {
  return (
    <div className={classes.contact_details}>
      <label className={classes.label}>Contact Details</label>
      <div className={classes.name_inputs}>
        <div>
          <Field
            id="firstName"
            name="firstName"
            placeholder="First Name"
            {...formik.getFieldProps("firstName")}
            className={
              formik.errors.firstName && formik.touched.firstName
                ? classes.error_name
                : classes.name
            }
          />
          <ErrorMessage
            name="firstName"
            component="span"
            className={classes.error_message}
          />
        </div>
        <div>
          <Field
            id="lastName"
            name="lastName"
            placeholder="Last Name"
            {...formik.getFieldProps("lastName")}
            className={
              formik.errors.lastName && formik.touched.lastName
                ? classes.error_name
                : classes.name
            }
          />
          <ErrorMessage
            name="lastName"
            component="span"
            className={classes.error_message}
          />
        </div>
      </div>
    </div>
  );
};

export default ContactDetails;
