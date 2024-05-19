//REACT
import { Fragment } from "react";
//COMPONENTS
import BookList from "../components/Books/BookList/BookList";
import Carousel from "../components/Books/CarouselComponent/Carousel";
//CSS
import classes from "./Home.module.css";

const HomePage: React.FC = () => {
  return (
    <Fragment>
      <div className={classes.main_container}>
        <Carousel />
        <BookList />
      </div>
    </Fragment>
  );
};

export default HomePage;
