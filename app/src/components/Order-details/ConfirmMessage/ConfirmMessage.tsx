//ICON/CSS
import { AiOutlineExclamationCircle } from "react-icons/ai";
import classes from "./ConfirmMessage.module.css";
//ROUTER/REDUX
import { useNavigate } from "react-router-dom";
import { useAppDispatch } from "../../../redux/hooks/redux-hooks";
import { updateOrder } from "../../../redux/slices/formSlice";

const ConfirmMessage = (props: {
  setState: (state: boolean) => void;
  values;
  orderIdNumber: number;
}) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const ConfirmHandler = () => {
    console.log(props.values);
    let payload = {
      id: props.orderIdNumber,
      updatedValues: props.values,
    };
    dispatch(updateOrder(payload));
    navigate("/orders");
  };
  return (
    <div className={classes.back}>
      <div className={classes.modal}>
        <div className={classes.confirm}>
          <AiOutlineExclamationCircle size={24} style={{ color: "#b8781f" }} />
          <div className={classes.text}>
            <p>Confirmation</p>
            <p>Confirm order details changes?</p>
          </div>
        </div>
        <div className={classes.btn}>
          <button onClick={() => props.setState(false)}>Cancel</button>
          <button onClick={ConfirmHandler}>Confirm</button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmMessage;
