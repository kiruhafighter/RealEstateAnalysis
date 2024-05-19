//ICON
import { CiShoppingCart } from "react-icons/ci";
//CSS
import classes from "./ButtonToCart.module.css";

type ButtonProps = {
  onAddToCart: (i: number) => void;
};

const ButtonToCart: React.FC<ButtonProps> = ({ onAddToCart }) => {
  const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const enteredAmountNumber = 1;
    onAddToCart(enteredAmountNumber);
  };
  return (
    <form onSubmit={submitHandler}>
      <button className={classes.add_btn}>
        <CiShoppingCart size={16} />
        <span>Add to Cart</span>
      </button>
    </form>
  );
};

export default ButtonToCart;
