//REDUX
import { useAppDispatch } from "../../../redux/hooks/redux-hooks";
import { removeItem, addItem, deleteItem } from "../../../redux/slices/cartSlice";
//CSS
import classes from "./CartItems.module.css";
//ICONS
import { CiTrash } from "react-icons/ci";
import { AiOutlineMinusCircle, AiOutlinePlusCircle } from "react-icons/ai";

type CartProps = {
  id: string;
  title: string;
  price: number;
  image: string;
  author: string;
  count: number;
};

const CartItems: React.FC<CartProps> = ({
  id,
  title,
  image,
  author,
  count,
  price,
}) => {
  const dispatch = useAppDispatch();
  const cartItemRemoveHandler = () => {
    dispatch(removeItem(id));
  };

  const cartItemAddHandler = () => {
    const item = {
      id: id,
      title: title,
      price: price,
      image: image,
      author: author,
    };
    dispatch(addItem(item));
  };

  const cartItemDeleteHandler = () => {
    dispatch(deleteItem(id));
  }
  return (
    <li className={classes.info_folder}>
      <div className={classes.image}>
        <img src={image} alt="book" className={classes.image} />
      </div>

      <div className={classes.info}>
        <div className={classes.description}>
          <h1>{title}</h1>
          <p className={classes.info_text}>
            by <span className={classes.info_author}>{author}</span>
          </p>
        </div>
          <div className={classes.amount}>
            <button onClick={cartItemRemoveHandler} disabled={count === 1}><AiOutlineMinusCircle size={32}/></button>
            <span>{count}</span>
            <button onClick={cartItemAddHandler}><AiOutlinePlusCircle size={32}/></button>
          </div>
          <div>
            <p className={classes.price}>${price * count}</p>
            <button className={classes.trash} onClick={cartItemDeleteHandler}>
              <CiTrash size={20} />
              <span className={classes.trash_text}>Remove</span>
            </button>
          </div>
      </div>
    </li>
  );
};

export default CartItems;
