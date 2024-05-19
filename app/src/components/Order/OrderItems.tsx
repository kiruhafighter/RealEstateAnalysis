//REACT-ROUTER
import { Link } from "react-router-dom";
//CSS
import classes from "./OrderItem.module.css";
import orderPic from "../../assets/orderPic.jpg";
import { BsPencil } from "react-icons/bs";

type OrderProps = {
  totalAmount: number;
  totalPrice: number;
  orderNum: number;
  isCompleted: boolean;
};

const OrderItems: React.FC<OrderProps> = ({
  totalAmount,
  totalPrice,
  orderNum,
  isCompleted,
}) => {
  const id = orderNum;
  const editOrder = (
    <Link to={`/shopping-cart/order-details?mode=edit&id=${id}`} className={classes.trash}>
     
        <BsPencil size={16} /> 
      <p className={classes.trash_text}> Edit Order Details</p>
    </Link>
  );

  return (
    <li className={classes.info_folder}>
      <div className={classes.image}>
        <img src={orderPic} className={classes.image} />
      </div>
      <div className={classes.info}>
        <div>
          <h1>Order #{orderNum.toString().padStart(4, "0")}</h1>
          <p className={classes.info_text}>
            Items: <b>{totalAmount}</b>
          </p>
          <p className={classes.down}>
            Delivery Status: <b>{isCompleted ? "Completed" : "In Progress"}</b>
          </p>
        </div>

        <div className={classes.right}>
          <p className={classes.price}>${totalPrice}</p>
          {isCompleted ? "" : editOrder}
        </div>
      </div>
    </li>
  );
};

export default OrderItems;
