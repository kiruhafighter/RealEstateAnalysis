//FORMIK&&SELECT&&CSS
import { ErrorMessage, FormikProps } from "formik";
import Select from "react-select";
import classes from "../NewForm/Form.module.css";
//TYPE
type CountrySelectProps = {
  name: string;
  formik: FormikProps<{
    billingCountry: string;
    deliveryCountry: string;
  }>;
};
//OPTIONS
const options = [
  { value: "ukraine", label: "Ukraine" },
  { value: "romania", label: "Romania" },
  { value: "moldova", label: "Moldova" },
  { value: "germany", label: "Germany" },
];

const CountrySelect: React.FC<CountrySelectProps> = ({ name, formik }) => {
  const selectedCountryOption = options.find(
    (option) => option.value === formik.values[name]
  );

  const handleSelectChange = (selectedOption: any) => {
    formik.setFieldValue(name, selectedOption.value);
  };

  const ErrorCountry =
    (formik.errors[name] && formik.touched[name]) ||
    (formik.touched[name] && !selectedCountryOption);

  return (
    <div>
      <Select
        name={name}
        options={options}
        value={selectedCountryOption}
        onChange={handleSelectChange}
        onBlur={formik.handleBlur(name)}
        placeholder="Select a country"
        styles={{
          control: (provided, state) => ({
            ...provided,
            backgroundColor: ErrorCountry
              ? "rgba(236, 99, 99, 0.466)"
              : "white",
            border: ErrorCountry
              ? "1px solid red"
              : state.isFocused
              ? "1px solid black"
              : "1px solid #e9e9e9",
            borderRadius: "none",
            width: "100%",
            height: "40px",
            marginTop: "16px",
            boxShadow: state.isFocused ? "0 0 0 1px black" : provided.boxShadow,
            "&:hover": {
              border: ErrorCountry
                ? "1px solid red"
                : state.isFocused
                ? "1px solid black"
                : "1px solid #e9e9e9",
            },
          }),
          option: (provided, state) => ({
            ...provided,
            backgroundColor: state.isSelected
              ? "#989c99"
              : state.isFocused
              ? "#e1e3e1"
              : provided.backgroundColor,
          }),
        }}
      />
      <ErrorMessage
        name={name}
        component="span"
        className={classes.error_message}
      />
    </div>
  );
};

export default CountrySelect;
