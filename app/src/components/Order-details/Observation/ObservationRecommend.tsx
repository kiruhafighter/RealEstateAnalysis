//FORMIK/CSS
import { FormikProps, Field } from "formik";
import classes from "../NewForm/Form.module.css";

type ObservationRecommendProps = {
  formik: FormikProps<{
    observations: string;
    checkRecommend: boolean;
  }>;
};

const ObservationRecommend: React.FC<ObservationRecommendProps> = ({
  formik,
}) => {
  return (
    <>
      <div className={classes.text_div}>
        <label className={classes.label}>Observations</label>
        <Field
          id="observations"
          as="textarea"
          name="observations"
          value={formik.values.observations}
          placeholder="Observations"
          className={classes.text_textarea}
        />
      </div>

      <div className={classes.text_div}>
        <label className={classes.label}>Would You Recommend Us?</label>
        <p className={classes.check}>
          <Field
            name="checkRecommend"
            checked={formik.values.checkRecommend}
            type="checkbox"
            {...formik.getFieldProps("checkRecommend")}
            className={classes.check_input}
          />
          Would you recommend us?
        </p>
      </div>
    </>
  );
};

export default ObservationRecommend;
