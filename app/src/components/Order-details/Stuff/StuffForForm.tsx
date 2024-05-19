export interface FormValues {
  firstName: string;
  lastName: string;
  billingCountry: string;
  billingAddress: string;
  billingPhoneNumber: string;
  checkAdress: boolean;
  deliveryCountry: string;
  deliveryAddress: string;
  deliveryPhoneNumber: string;
  paymentMethod: string;
  deliveryDate: Date | string;
  observations: string;
  checkRecommend: boolean;
}

export const validate = (values: FormValues) => {
  const errors: Partial<FormValues> = {};
  const trimmedFirstName = values.firstName.trim();
  const trimmedLastName = values.lastName.trim();
  if (!trimmedFirstName) {
    errors.firstName = "Please, enter your first name";
  } else if (!/^[a-zA-Z\s]+$/.test(trimmedFirstName)) {
    errors.firstName = "Please enter only letters";
  }
  if (!trimmedLastName) {
    errors.lastName = "Please, enter your last name";
  }else if (!/^[a-zA-Z\s]+$/.test(trimmedLastName)) {
    errors.lastName = "Please enter only letters";
  }
  if (!values.billingCountry) {
    errors.billingCountry = "Please, choose your country";
  }
  if (!values.billingAddress) {
    errors.billingAddress = "Please, enter your address";
  }
  if (!values.billingPhoneNumber) {
    errors.billingPhoneNumber = "Please, enter your phone number";
  }else if (!/^\d+$/.test(values.billingPhoneNumber)) {
    errors.billingPhoneNumber = "Please enter only numbers for your phone number";}
  if (!values.deliveryCountry && !values.checkAdress) {
    errors.deliveryCountry = "Please, choose your country";
  }
  if (!values.deliveryAddress && !values.checkAdress) {
    errors.deliveryAddress = "Please, enter your address";
  }
  if (!values.deliveryPhoneNumber && !values.checkAdress) {
    errors.deliveryPhoneNumber = "Please, enter your phone number";
  }else if (!/^\d+$/.test(values.deliveryPhoneNumber)) {
    errors.deliveryPhoneNumber = "Please enter only numbers for your phone number";}
  if (!values.deliveryDate) {
    errors.deliveryDate = "Please, enter your delivery date";
  }else {
  const selectedDate = new Date(values.deliveryDate);
  const currentDate = new Date();

  if (
    selectedDate < currentDate &&
    selectedDate.toDateString() !== currentDate.toDateString()
  ) {
    errors.deliveryDate = "Please select a date in the future";
  }}
  return errors;
};
