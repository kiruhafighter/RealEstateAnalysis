//ICON
import { BsArrowUpSquareFill } from "react-icons/bs";
//CSS
import classes from "./Footer.module.css";

const scrollUp = () => {
  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
};

const Footer: React.FC = () => {
  return (
    <div className={classes.footer}>
      <p>
        &copy;Copyright{" "}
        <a
          rel="noreferrer"
          className={classes.ITP}
        >
          Yeryfa Yevheniia
        </a>
      </p>
      <BsArrowUpSquareFill
        onClick={scrollUp}
        className={classes.btn}
        size={28}
      />
    </div>
  );
};

export default Footer;
