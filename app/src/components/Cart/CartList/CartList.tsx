//CONTEXT
import { CartItem } from '../../../redux/slices/types';
import CartItems from '../CartItem/CartItems';
//CSS
import classes from '../../../pages/ShoppingCart.module.css';


const ShoppingCart: React.FC<{items: CartItem[]}> = ({items}) => {
    return (
        <ul className={classes.cart_item}>
        {items.map((item) => (
          <CartItems {...item} />
        ))}
      </ul>
    );
};

export default ShoppingCart;