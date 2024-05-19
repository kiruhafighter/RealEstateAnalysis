//LOADING
import ContentLoader from "react-content-loader";
//CSS
import classes from "../BookComponent/BookComponent.module.css";

const Skeleton: React.FC = (props) => (
  <ContentLoader
    speed={2}
    width={232}
    height={500}
    viewBox="0 0 232 500"
    backgroundColor="#f3f3f3"
    foregroundColor="#ecebeb"
    {...props}
    className={classes.box}
  >
    <rect x="0" y="6" rx="0" ry="0" width="232" height="329" />
    <rect x="0" y="345" rx="0" ry="0" width="232" height="50" />
    <rect x="0" y="433" rx="0" ry="0" width="232" height="40" />
  </ContentLoader>
);

export default Skeleton;
