//REACT-ROUTER-REDUX
import { useParams, useLocation } from "react-router-dom";
import { Fragment } from "react";
import { useAppSelector } from "../redux/hooks/redux-hooks";
import { useAppDispatch } from "../redux/hooks/redux-hooks";
import { addItem } from "../redux/slices/cartSlice";
//COMPONENT
import ButtonToCart from "../components/Books/ButtonToCart/ButtonToCart";
//CSS
import classes from "./BookDetail.module.css";

const ViewBook: React.FC = () => {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const bookId = searchParams.get("id");
 
  const { items } = useAppSelector((state) => state.books);
  
  const book = items.find((book) => book.id === bookId);
  const { title, price, author, description, id, image} = book;
  console.log(book);
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
    <Fragment>
      <div className={classes.container}>
        <div className={classes.box}>
          <div className={classes.prodImage}>
            <img src={image}/>
          </div>
          <div className={classes.info}>
            <p className={classes.price}>${price}</p>
            <h1>{title}</h1>
            <p className={classes.author}>
              by <span className={classes.author_span}>{author}</span>
            </p>
            <p className={classes.dscrptn}>{description}</p>
            <ButtonToCart onAddToCart={onClickAdd} />
          </div>
        </div>
      </div>
    </Fragment>
  );
};

export default ViewBook;
