//ICON
import { TfiFaceSad } from 'react-icons/tfi';
import classes from "./Error.module.css";

const ErrorPage: React.FC = () => {
  return (
    <div className={classes.error}>
        <TfiFaceSad size={260}/>
        <h2>An error occurred...Could not find this page!</h2>
        <p>Please go to Home page and choose a new direction</p>
      </div>
  );
};

export default ErrorPage;
