//REACT
import { useState, useEffect } from "react";
//CSS
import classes from "./Carousel.module.css";
//ICONS
import {
  BsChevronCompactLeft,
  BsChevronCompactRight,
  BsDash,
} from "react-icons/bs";
//IMAGE
import img1 from "../../../assets/1.webp";
import img2 from "../../../assets/2.webp";
import img3 from "../../../assets/3.webp";
//COMPONENTS
import CarouselItem from "../CarouselItem/CarouselItem";

const BooksIntro: React.FC = () => {
  const [currentPhotoIndex, setCurrentPhotoIndex] = useState(0);
  const images = [img1, img2, img3];
  const goToPreviousPhoto = () => {
    if (currentPhotoIndex === 0) {
      setCurrentPhotoIndex(images.length - 1);
    } else {
      setCurrentPhotoIndex(currentPhotoIndex - 1);
    }
  };

  const goToNextPhoto = () => {
    if (currentPhotoIndex === images.length - 1) {
      setCurrentPhotoIndex(0);
    } else {
      setCurrentPhotoIndex(currentPhotoIndex + 1);
    }
  };
  const dashes = images.map((_, index) => (
    <BsDash
      key={index}
      onClick={() => setCurrentPhotoIndex(index)}
      style={{
        fontSize: currentPhotoIndex === index ? "48px" : "32px",
        color: currentPhotoIndex === index ? "gray" : "lightgray",
      }}
    />
  ));

  useEffect(() => {
    const interval = setInterval(() => {
      goToNextPhoto();
    }, 10000);
    return () => clearInterval(interval);
  }, [currentPhotoIndex]);

  return (
    <div className={classes.main}>
      <div className={classes.box}>
        <button onClick={goToPreviousPhoto} className={classes.buttons}>
          <BsChevronCompactLeft size={50} />
        </button>
        <CarouselItem props={images[currentPhotoIndex]} />
        <button onClick={goToNextPhoto} className={classes.buttons}>
          <BsChevronCompactRight size={50} />
        </button>
      </div>
      <div className={classes.dashes}>{dashes}</div>
    </div>
  );
};

export default BooksIntro;
