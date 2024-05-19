//REACT-ROUTER
import { RouterProvider } from 'react-router-dom';
import router from './Router/router';

const App = () => {
  return (
      <RouterProvider router={router} />
  );
}

export default App;
