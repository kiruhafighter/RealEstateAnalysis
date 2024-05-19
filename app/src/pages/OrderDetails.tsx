//ROUTER
import { useSearchParams } from "react-router-dom";
//IMG
import orderImg from "../assets/order.jpg";
//COMPONENTS
import NewForm from "../components/Order-details/NewForm/NewForm";
import EditForm from "../components/Order-details/EditForm/EditForm";
//CSS
import classes from "./OrderDetails.module.css";

const OrderDetails: React.FC = () => {
  const [searchParams] = useSearchParams();
  const isNewlyFormInfo = searchParams.get("mode") === "form";
  return (
    <>
      <div className={classes.box}>
        <div className={classes.img_box}>
          <img src={orderImg} alt="books" className={classes.img_ordr} />
          <div className={classes.rectangle}></div>
        </div>
        <div className={classes.form_box}>
          <h1>Order Details</h1>
          {isNewlyFormInfo ? (
            <div>
              <NewForm />
            </div>
          ) : (
            <div>
              <EditForm />
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default OrderDetails;
