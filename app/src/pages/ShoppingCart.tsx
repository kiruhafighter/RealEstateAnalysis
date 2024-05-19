//REACT
import { Fragment} from 'react';
import { Link } from 'react-router-dom';
//CONTEXT
import CartList from '../components/Cart/CartList/CartList'; 
import EmptyCart from '../components/Empty/EmptyCart/EmptyCart';
import { useAppSelector } from '../redux/hooks/redux-hooks';
//CSS
import classes from './ShoppingCart.module.css';


const ShoppingCart: React.FC = () => {
  const {items , totalPrice} = useAppSelector((state) => state.cart);
  const { isLogin } = useAppSelector((state) => state.user);
  const hasItems = items.length > 0;
    return (
      <Fragment>
        <div className={classes.box}>
         {!items.length ? <EmptyCart/> : 
        <div className={classes.cart}>
          <h1 className={classes.cart_title}>Your Products</h1>
          <CartList items={items}/>
          <div className={classes.total}>
            <span className={classes.total_text}>Total:</span>
            <span className={classes.total_price}>${totalPrice}</span>
          </div>
          <div className={classes.actions}>
            <Link to="/">
              <button className={classes.btn_back}>Continue Shopping</button>
            </Link>
            <Link to={`${isLogin ? "order-details?mode=form" : "/login?mode=order"}`}>
              {hasItems && (
                <button className={classes.button_order}>Place Order</button>
              )}
            </Link>
          </div> 
        </div>
        }
        </div>
      </Fragment>
    );
};

export default ShoppingCart;