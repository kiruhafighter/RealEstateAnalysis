//IMG/ICON/CSS
import EmptyOrderPic from "../../../assets/EmptyOrder.png";
import { FaRegSmileWink } from "react-icons/fa";
import classes from "../Empty.module.css";

const EmptyOrder: React.FC = () => {
  return (
    <div className={classes.cart}>
      <div className={classes.img_or}>
        <img src={EmptyOrderPic} alt="empty order pic((" />
      </div>

      <h2>Your Order List is empty.</h2>
      <p>
        Please fill It and make Us happy
        <FaRegSmileWink />
      </p>
    </div>
  );
};

export default EmptyOrder;
