//REACT/REDUX
import { Fragment, useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../redux/hooks/redux-hooks";
import { fetchBooks } from "../../../redux/slices/booksSlice";
import { Status } from "../../../redux/slices/types";
//COMPONENT
import BookComponent from "../BookComponent/BookComponent";
import Skeleton from "../Skeleton/Skeleton";
import ErrorPage from "../../../pages/Error";
//CSS
import classes from "./BookList.module.css";

const BookList: React.FC = () => {
  const dispatch = useAppDispatch();
  const { status, items } = useAppSelector((state) => state.books);

  const getBooks = async () => {
    dispatch(fetchBooks());
  };

  useEffect(() => {
    getBooks();
  }, []);

  const BestBooks = items
    .filter((props) => props.category === "best book")
    .map((props) => <BookComponent key={props.id} {...props} />);

  const RecentlyAdded = items
    .filter((props) => props.category === "recently added")
    .map((props) => <BookComponent key={props.id} {...props} />);
  const skeletons = [...new Array(6)].map((_, index) => (
    <Skeleton key={index} />
  ));

  return (
    <Fragment>
      {status === Status.ERROR ? (
        <ErrorPage />
      ) : (
        <div className={classes.box}>
          <p className={classes.title}>Best books of the month</p>
          <ul className={classes.container}>
            {status === Status.LOADING ? skeletons : BestBooks}
          </ul>
          <p className={classes.title}>Recently added</p>
          <ul className={classes.container}>
            {status === Status.LOADING ? skeletons : RecentlyAdded}
          </ul>
        </div>
      )}
    </Fragment>
  );
};
export default BookList;
