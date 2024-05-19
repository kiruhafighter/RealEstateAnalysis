//ROUTER-DOM/REACT/REDUX
import { Link } from "react-router-dom";
import { useAppDispatch } from "../../../redux/hooks/redux-hooks";
import { addItem } from "../../../redux/slices/cartSlice";
//DATA
import ButtonToCart from "../ButtonToCart/ButtonToCart";
//CSS
import classes from "./BookComponent.module.css";

export type BookProps = {
  key: string;
  id: string;
  title: string;
  price: number;
  image: string;
  author: string;
};

export const scrollUp = () => {
  window.scrollTo({
    top: 0,
    behavior: "auto",
  });
};

const BookComponent: React.FC<BookProps> = ({
  key,
  id,
  title,
  price,
  image,
  author,
}) => {
  const dispatch = useAppDispatch();
  const onClickAdd = () => {
    const item = {
      id: id,
      title: title,
      price: price,
      image: image,
      author: author,
    };
    dispatch(addItem(item));
  };
  return (
    <li key={key} className={classes.box}>
      <Link to={`/book?id=${id}`} onClick={scrollUp}>
        <div className={classes.image}>
          <img src={image} />
        </div>
        <div className={classes.header}>
          <p className={classes.price}>${price}</p>
          <h1>{title}</h1>
          <p>{author}</p>
        </div>
      </Link>
      <ButtonToCart onAddToCart={onClickAdd} />
    </li>
  );
};

export default BookComponent;
