import { CartItem } from "../redux/slices/types";

export const calcTotalPrice = (items:CartItem[]) => {
  return items.reduce((sum, obj) => {
    return obj.price * obj.count + sum;
  }, 0);
};

export const calcTotalAmount = (items:CartItem[]) => {
  return items.reduce((sum, item) => sum + item.count,
  0
);
};