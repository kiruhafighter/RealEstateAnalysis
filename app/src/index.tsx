//REACT
import ReactDOM from "react-dom/client";
//REDUX
import { store } from "./redux/store";
import { Provider } from "react-redux";
//CSS
import "./index.css";
//MAIN COMPONENT
import App from "./App";
import './firebase';

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <Provider store={store}>
    <App />
  </Provider>
);
