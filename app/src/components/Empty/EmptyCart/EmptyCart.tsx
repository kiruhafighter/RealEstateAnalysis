//IMG/ICON/CSS
import EmptyCartPic from "../../../assets/EmptyCart.png";
import { FaRegSmileWink } from "react-icons/fa";
import classes from "../Empty.module.css";

const EmptyCart: React.FC = () => {
  return (
    <div className={classes.cart}>
      <div className={classes.img}>
        <img src={EmptyCartPic} alt="empty cart picture" />
      </div>

      <h2>Your Shopping Cart is empty and sad.</h2>
      <p>
        Please fill It and make It happy
        <FaRegSmileWink />
      </p>
    </div>
  );
};

export default EmptyCart;
