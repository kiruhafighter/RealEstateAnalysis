//CSS
import classes from "../CarouselComponent/Carousel.module.css";

const CarouselItem = (props) => {
  return (
    <div className={classes.content}>
      <div className={classes.text}>
        <h1>
          Buy textbooks <br /> for the best price{" "}
        </h1>
        <p>
          From applied literature to educational resources, we have a lot of{" "}
          <br />
          textbooks to offer you. We sell only the best books.
        </p>
      </div>
      <div className={classes.image}>
        <img src={props.props} alt="carousel" />
      </div>
    </div>
  );
};

export default CarouselItem;
